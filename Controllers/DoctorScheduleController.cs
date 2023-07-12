using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
using AutoMapper;


namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;


        public DoctorScheduleController(VaccineDBContext vaccineDBContext, IMapper mapper)
        {
            _db = vaccineDBContext;
              _mapper = mapper;
        }

        // [HttpGet("doctor_schedule/{doctorId}")]
        // public ActionResult<IEnumerable<DoctorSchedule>> GetDoctorSchedule(int doctorId)
        // {
        //     try
        //     {
        //         var doctorSchedule = _db.DoctorSchedules
        //             .Where(ds => ds.DoctorId == doctorId)
        //             .OrderBy(ds => ds.Date)
        //             .ToList();

        //         return Ok(doctorSchedule);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
        // [Route("doctor_post_schedule")]
        // [HttpPost]
        // public async Task<IActionResult> Getnewandsave(int doctorId)
        // {
        //     try
        //     {
        //         if(_db.DoctorSchedules.Any(d=>d.DoctorId==doctorId))
        //         {
        //             return Ok("Schedule already exist");
        //         }
        //         var doctorsSchedule = _db.AdminSchedules.ToList();

        //         List<DoctorSchedule> doseScheduleList = new List<DoctorSchedule>();

        //         foreach (var ds in doctorsSchedule)
        //         {
        //             var doseScheduleEntry = new DoctorSchedule
        //             {
        //                 // Date = ds.Date,
        //                 DoseId = ds.DoseId,
        //                 DoctorId = doctorId
        //             };

        //             doseScheduleList.Add(doseScheduleEntry);
        //         }
                
        //         _db.DoctorSchedules.AddRange(doseScheduleList);
        //         await _db.SaveChangesAsync();

        //         return Ok(doseScheduleList.OrderBy(x => x.Date));
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
        [Route("single_updateDate")]
        [HttpPatch]
        public async Task<IActionResult> Update(long doseId,long doctorId,[FromBody] DoctorSchedule ds)
        {
            try{
                
                var dbDoc = await _db.DoctorSchedules.FirstOrDefaultAsync(d => d.DoctorId == ds.DoctorId && d.DoseId==ds.DoseId); 
                if (dbDoc == null)
                {
                    return NotFound();
                }
             
                dbDoc.Date = ds.Date;
                _db.Entry(dbDoc).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }
        // [Route("doctor_bulk_updateDate/{date}")]
        // [HttpPatch]
        //  public async Task<IActionResult> PatchAsync(DateTime date,int doctorId,[FromBody] JsonPatchDocument<DoctorSchedule> patchDocument)
        // {
        //     try{
        //         var dbDocS = _db.DoctorSchedules.Where(d=>d.Date.Date==date.Date && d.DoctorId==doctorId).ToList(); 
        //         if (dbDocS == null)
        //         {
        //             return NotFound();
        //         }
        //         dbDocS.ForEach(d => patchDocument.ApplyTo(d));
        //         await _db.SaveChangesAsync();
        //         return NoContent();
                
        //     }
        //     catch(Exception ex){
        //         return StatusCode(500,ex.Message); 
        //     }
        // }

        // [HttpPatch]
        // [Route("/update_date_for_Vaccations")]
        // public async Task<IActionResult> UpadateDoseDates(int doctorId, DateTime fromDate,DateTime toDate)
        // {
        //     try
        //     {
        //         var dosesToUpdate= _db.DoctorSchedules
        //             .Where(ds=>ds.Date >= fromDate && ds.Date <=toDate && ds.DoctorId==doctorId).ToList();

        //         foreach(var dose in dosesToUpdate)
        //         {
        //             var gap=(toDate - fromDate).TotalDays;
        //             dose.Date=dose.Date.AddDays(gap);
        //         }
        //         await _db.SaveChangesAsync();
        //         return Ok("Dose Dates Updated for vacations");
        //     }
        //     catch(Exception ex)
        //     {
        //         return StatusCode(500,ex.Message);
        //     }
        // }



[HttpGet]
[Route("new")]
public async Task<IActionResult> GetNew(int doctorId)
{
    try
    {
        Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();

        // Check if the DoctorSchedules table already exists.
        if (!_db.DoctorSchedules.Any(d=>d.DoctorId==doctorId))
        {
            // If not, get the data from the AdminSchedules table and save it in the DoctorSchedules table.
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

                // Save the DoctorSchedule record.
                var doctorSchedule = new DoctorSchedule
                {
                    Date = newDate,
                    DoseId = adminSchedule.DoseId,
                    DoctorId = doctorId
                };
                _db.DoctorSchedules.Add(doctorSchedule);
            }

            await _db.SaveChangesAsync();
        }
        else
        {
            // If the DoctorSchedules table already exists, get the data from it.
            var doctorSchedules = await _db.DoctorSchedules.Where(d => d.DoctorId == doctorId).ToListAsync();

            foreach (var doctorSchedule in doctorSchedules)
            {
                var newDate = (doctorSchedule.Date);
                var dose = await _db.Doses.FindAsync(doctorSchedule.DoseId);
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