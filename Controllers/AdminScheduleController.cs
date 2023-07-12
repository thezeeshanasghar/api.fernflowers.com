using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Globalization;
namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminScheduleController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;

        public AdminScheduleController(VaccineDBContext vaccineDBContext, IMapper mapper)
        {
            _db = vaccineDBContext;
            _mapper = mapper;
        }

      



        [Route("Admin_single_updateDate")]
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] AdminSchedule ds)
        {
            try
            {
                var dbDoc = await _db.AdminSchedules.Where(x=>x.DoseId==ds.DoseId).FirstOrDefaultAsync();
                if (dbDoc == null)
                {
                    return NotFound();
                }

                dbDoc.Date = ds.Date;
                _db.Entry(dbDoc).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [Route("Admin_bulk_updateDate/{date}")]
        // [HttpPatch]
        // public async Task<IActionResult> PatchAsync(DateTime date, [FromBody] JsonPatchDocument<AdminSchedule> patchDocument)
        // {
        //     try
        //     {
        //         // var dbDoc = _db.AdminSchedules.Where(d => d.Date ==DateOnly.FromDateTime(date.Date)).ToList();
        //         var dbDoc = _db.AdminSchedules.Where(d => d.Date ==DateOnly.FromDateTime(date.Date)).ToList();
        //         if (dbDoc == null)
        //         {
        //             return NotFound();
        //         }
        //         foreach (var doc in dbDoc)
        //         {
        //             var patchedDoc = new AdminSchedule();
        //             patchDocument.ApplyTo(patchedDoc);

        //             // Update the Date property with the patched date
        //             doc.Date = patchedDoc.Date;
        //         }
        //         await _db.SaveChangesAsync();
        //         return NoContent();

        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }


        [Route("Admin_bulk_updateDate/{date}")]
        [HttpPatch]
        public async Task<IActionResult> PatchAsync(DateTime date, [FromBody] JsonPatchDocument<AdminSchedule> patchDocument)
        {
            try
            {
                var dbDoc = _db.AdminSchedules.Where(d => d.Date == DateOnly.FromDateTime(date)).ToList();
                if (dbDoc == null)
                {
                    return NotFound();
                }
                // dbDoc.ForEach(d => patchDocument.ApplyTo(d));
                foreach (var doc in dbDoc)
                {
                    patchDocument.ApplyTo(doc);
                    _db.Entry(dbDoc).State = EntityState.Modified;
                }
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }








    



    [HttpGet]
    [Route("admin_post_doseSchedule")]
    public async Task<IActionResult> GetNew()
    {
        try
        {
            Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();

            if (!_db.AdminSchedules.Any())
            {
                var doses = await _db.Doses.OrderBy(x => x.MinAge).ToListAsync();

                var today = DateOnly.FromDateTime(DateTime.Now);
                foreach (var dos in doses)
                {
                    var newDate = today.AddDays(dos.MinAge);
                    var dto = _mapper.Map<DoseDTO>(dos);
                    if (dict.ContainsKey(newDate))
                        dict[newDate].Add(dto);
                    else
                        dict.Add(newDate, new List<DoseDTO>() { dto });

                    // Save AdminSchedule, {date, dose_id} to update
                    var adminSchedule = new AdminSchedule
                    {
                        Date = newDate,
                        DoseId = dos.Id
                    };
                    _db.AdminSchedules.Add(adminSchedule);
                }

                await _db.SaveChangesAsync();
            }
            else
            {
                var adminSchedules = await _db.AdminSchedules.ToListAsync();

                foreach (var adminSchedule in adminSchedules)
                {
                    var newDate = (adminSchedule.Date);
                    var dose = await _db.Doses.FindAsync(adminSchedule.DoseId);
                    var dto = _mapper.Map<DoseDTO>(dose);

                    if (dict.ContainsKey(newDate))
                        dict[newDate].Add(dto);
                    else
                        dict.Add(newDate, new List<DoseDTO>() { dto });
                }
            }

            return Ok(dict);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    }
}
