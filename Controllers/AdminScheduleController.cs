using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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

        [HttpGet]
        [Route("admin_post_doseSchedule")]
        public async Task<IActionResult> Get()
        {
            try
            {
                if (_db.AdminSchedules.Any())
                {
                    return Ok("Schedule already exist");
                }
                
                List<DoseDTO> doseDTOList = new List<DoseDTO>();
                List<AdminSchedule> doseScheduleList = new List<AdminSchedule>();
                var doses = await _db.Doses.OrderBy(x => x.MinAge).ToListAsync();
                DateTime? doseDate = null;
                long lastVaccineId = -1;
                foreach (var dos in doses)
                {
                    var dosDTo = new DoseDTO
                    {
                        Id = dos.Id,
                        Name = dos.Name,
                        VaccineId = dos.VaccineId
                    };
                    var doseSchedule = new AdminSchedule
                    {
                        DoseId = dos.Id
                    };
                    if (doseDate == null || (dosDTo.VaccineId != lastVaccineId))
                    {
                        doseDate = DateTime.Now;
                    }
                    else
                    {
                        // var dateOfLastDoseOfSameVaccine = doseDTOList.LastOrDefault(d=> d.VaccineId == dosDTo.VaccineId)?.DoseDate;
                        // if(dateOfLastDoseOfSameVaccine!=null){
                        //    doseDate = dateOfLastDoseOfSameVaccine.Value.AddDays(dos.MinGap);
                        // }
                    }
                    // dosDTo.DoseDate = doseDate;
                    doseDTOList.Add(dosDTo);
                    doseSchedule.Date = doseDate.Value;
                    doseScheduleList.Add(doseSchedule);
                    lastVaccineId = dosDTo.VaccineId;
                }
                _db.AdminSchedules.AddRange(doseScheduleList);
                _db.SaveChanges();
                return Ok(doseDTOList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("Admin_single_updateDate")]
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] AdminSchedule ds)
        {
            try
            {
                var dbDoc = await _db.AdminSchedules.Where(x => x.DoseId == ds.DoseId).FirstOrDefaultAsync();
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

        [Route("Admin_bulk_updateDate/{date}")]
        [HttpPatch]
        public async Task<IActionResult> PatchAsync(DateTime date, [FromBody] JsonPatchDocument<AdminSchedule> patchDocument)
        {
            try
            {
                var dbDoc = _db.AdminSchedules.Where(d => d.Date.Date == date.Date).ToList();
                if (dbDoc == null)
                {
                    return NotFound();
                }
                dbDoc.ForEach(d => patchDocument.ApplyTo(d));
                await _db.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("new")]
        public async Task<IActionResult> GetNew()
        {
            try
            {
                var doses = await _db.Doses.OrderBy(x => x.MinAge).ToListAsync();
                Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();
                
                var today = DateOnly.FromDateTime(DateTime.Now);
                foreach (var dos in doses)
                {
                    var newDate = today.AddDays(dos.MinAge);
                    var dto = _mapper.Map<DoseDTO>(dos);
                    if (dict.ContainsKey(newDate))
                        dict[newDate].Add(dto);
                    else
                        dict.Add(newDate, new List<DoseDTO>() { dto });
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
