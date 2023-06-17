using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSchedule : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public DoctorSchedule(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        [HttpGet("doctor_schedule/{doctorId}")]
        public ActionResult<IEnumerable<DoctorsSchedule>> GetDoctorSchedule(int doctorId)
        {
            try
            {
                var doctorSchedule = _db.DoctorSchedules
                    .Where(ds => ds.DoctorId == doctorId)
                    .OrderBy(ds => ds.Date)
                    .ToList();

                return Ok(doctorSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("doctor_post_schedule")]
        [HttpPost]
        public async Task<IActionResult> Getnewandsave(int doctorId)
        {
            try
            {
                if(_db.DoctorSchedules.Any(d=>d.DoctorId==doctorId))
                {
                    return Ok("Schedule already exist");
                }
                var doctorsSchedule = _db.AdminSchedules.ToList();

                List<DoctorsSchedule> doseScheduleList = new List<DoctorsSchedule>();

                foreach (var ds in doctorsSchedule)
                {
                    var doseScheduleEntry = new DoctorsSchedule
                    {
                        Date = ds.Date,
                        DoseId = ds.DoseId,
                        DoctorId = doctorId
                    };

                    doseScheduleList.Add(doseScheduleEntry);
                }
                
                _db.DoctorSchedules.AddRange(doseScheduleList);
                await _db.SaveChangesAsync();

                return Ok(doseScheduleList.OrderBy(x => x.Date));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("single_updateDate")]

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] DoctorsSchedule ds)
        {
            try{
                var dbDoc = await _db.DoctorSchedules.Where(x=>x.DoseId==ds.DoseId).FirstOrDefaultAsync();
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
        [Route("doctor_bulk_updateDate/{date}")]
        [HttpPatch]
         public async Task<IActionResult> PatchAsync(DateTime date,[FromBody] JsonPatchDocument<DoctorsSchedule> patchDocument)
        {
            try{
                var dbDocS = _db.DoctorSchedules.Where(d=>d.Date.Date==date.Date).ToList(); 
                if (dbDocS == null)
                {
                    return NotFound();
                }
                dbDocS.ForEach(d => patchDocument.ApplyTo(d));
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }
    

        [HttpPatch]
        [Route("/update_date_for_Vaccations")]
        public async Task<IActionResult> UpadateDoseDates(int doctorId, DateTime fromDate,DateTime toDate)
        {
            try
            {
                var dosesToUpdate= _db.DoctorSchedules
                    .Where(ds=>ds.Date >= fromDate && ds.Date <=toDate && ds.DoctorId==doctorId).ToList();

                foreach(var dose in dosesToUpdate)
                {
                    var gap=(toDate - fromDate).TotalDays;
                    dose.Date=dose.Date.AddDays(gap);
                }
                await _db.SaveChangesAsync();
                return Ok("Dose Dates Updated for vacations");
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

  


    }
}