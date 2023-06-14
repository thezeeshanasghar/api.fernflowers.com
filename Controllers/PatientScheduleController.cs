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
    public class PatientSchedule : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public PatientSchedule(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

       
    //    [Route("doctor_post_schedule/child")]
    //     [HttpPost]
    //     public async Task<IActionResult> GetAndSaveWithChildId(int doctorId, int childId)
    //     {
    //         try
    //         {
    //             if (_db.PatientSchedules.Any(c => c.childId == childId ))
    //             {
    //                 return Ok("Schedule already exists");
    //             }

    //             var dsSchedule = _db.DoctorSchedules.Where(d => d.DoctorId == doctorId).ToList();

    //             List<PattientsSchedule> patientScheduleList = new List<PattientsSchedule>();

    //             foreach (var ds in dsSchedule)
    //             {
    //                 var doseScheduleEntry = new PattientsSchedule
    //                 {
    //                     Date = ds.Date,
    //                     DoseId = ds.DoseId,
    //                     DoctorId = doctorId,
    //                     childId = childId , // Add the child ID to each schedule entry
    //                     isSkip=false,
    //                     isDone=false
    //                 };

    //                 patientScheduleList.Add(doseScheduleEntry);
    //             }

    //             _db.PatientSchedules.AddRange(patientScheduleList);
    //             await _db.SaveChangesAsync();

    //             return Ok(patientScheduleList.OrderBy(x => x.Date));
    //         }
    //         catch (Exception ex)
    //         {
    //             return StatusCode(500, ex.Message);
    //         }
    //     }
    [Route("doctor_post_schedule")]
[HttpPost]
public async Task<IActionResult> Getnewandsave(int doctorId, int childId)
{
    try
    {
        if (_db.PatientSchedules.Any(d => d.childId == childId))
        {
            return Ok("Schedule already exists");
        }

        var doctorsSchedule = await _db.DoctorSchedules.ToListAsync();

        List<PattientsSchedule> patientScheduleList = new List<PattientsSchedule>();

        foreach (var ds in doctorsSchedule)
        {
            var doseScheduleEntry = new PattientsSchedule
            {
                DoseId = ds.DoseId,
                DoctorId = doctorId,
                childId = childId
            };

            var child = _db.Childs.FirstOrDefault(c => c.Id == childId);
doseScheduleEntry.Date = child != null
    ? child.DOB.AddDays(ds.Date.Day - 1) // Adjust the date based on the child's date of birth
    : DateTime.MinValue;

            patientScheduleList.Add(doseScheduleEntry);
        }

        _db.PatientSchedules.AddRange(patientScheduleList);
        await _db.SaveChangesAsync();

        return Ok(patientScheduleList.OrderBy(x => x.Date));
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}


        [HttpGet("patient_schedule/{childId}")]
        public ActionResult<IEnumerable<PattientsSchedule>> GetPatientSchedule(int childId)
        {
            try
            {
                var pSchedule = _db.PatientSchedules
                    .Where(ps => ps.childId == childId)
                    .OrderBy(ds => ds.Date)
                    .ToList();

                return Ok(pSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
         [Route("single_updateDate")]

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] PattientsSchedule ps)
        {
            try{
                var dbps = await _db.PatientSchedules.Where(x=>x.DoseId==ps.DoseId).FirstOrDefaultAsync();
                if (dbps == null)
                {
                    return NotFound();
                }
             
                dbps.Date = ps.Date;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }
            [Route("patient_bulk_updateDone")]
            [HttpPatch]
            public async Task<IActionResult> PatchAsync(bool isDone,[FromBody] JsonPatchDocument<PattientsSchedule> patchDocument)
            {
                try{
                    var dbPS = _db.PatientSchedules.Where(d=>d.isDone==isDone).ToList(); 
                    if (dbPS == null)
                    {
                        return NotFound();
                    }
                    dbPS.ForEach(d => patchDocument.ApplyTo(d));
                    await _db.SaveChangesAsync();
                    return NoContent();
                    
                }
                catch(Exception ex){
                    return StatusCode(500,ex.Message); 
                }
            }


        [Route("single_updateDone")]

        [HttpPatch]
        public async Task<IActionResult> UpdateDone([FromBody] PattientsSchedule ps)
        {
            try{
                var dbps = await _db.PatientSchedules.Where(x=>x.DoseId==ps.DoseId).FirstOrDefaultAsync();
                if (dbps == null)
                {
                    return NotFound();
                }
             
                dbps.isDone = ps.isDone;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }
        [Route("single_update_Skip")]

        [HttpPatch]
        public async Task<IActionResult> UpdateSkip([FromBody] PattientsSchedule ps)
        {
            try{
                var dbps = await _db.PatientSchedules.Where(x=>x.DoseId==ps.DoseId).FirstOrDefaultAsync();
                if (dbps == null)
                {
                    return NotFound();
                }
             
                dbps.isSkip = ps.isSkip;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }


        [Route("patient_bulk_updateDate/{date}")]
        [HttpPatch]
         public async Task<IActionResult> PatchAsync(DateTime date,[FromBody] JsonPatchDocument<PattientsSchedule> patchDocument)
        {
            try{
                var dbPS = _db.PatientSchedules.Where(d=>d.Date.Date==date.Date).ToList(); 
                if (dbPS == null)
                {
                    return NotFound();
                }
                dbPS.ForEach(d => patchDocument.ApplyTo(d));
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }
    

    [Route("patient_bulk_update_isSkip")]
        [HttpPatch]
         public async Task<IActionResult> PatchisSKip(DateTime date,[FromBody] JsonPatchDocument<PattientsSchedule> patchDocument)
        {
            try{
                var dbPS = _db.PatientSchedules.Where(d=>d.Date.Date==date.Date).ToList(); 
                if (dbPS == null)
                {
                    return NotFound();
                }
                dbPS.ForEach(d => patchDocument.ApplyTo(d));
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }
        // [HttpGet("today")]
        // public ActionResult<IEnumerable<PattientsSchedule>> GetPatientsWithTodayDate()
        // {
        //     try
        //     {
        //         DateTime today = DateTime.Today;
        //         var patients = _db.PatientSchedules
        //             .Where(p => p.Date.Date == today)
        //             .GroupBy(p => p.childId)
        //             .Select(g => g.First())
        //             .ToList();

        //         if (patients == null || patients.Count == 0)
        //             return Ok("No patient is visiting");

        //         return Ok(patients);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
        [HttpGet("today_alert")]
public ActionResult<IEnumerable<Child>> GetPatientsWithTodayDate()
{
    try
    {
        DateTime today = DateTime.Today;
        var childIds = _db.PatientSchedules
            .Where(p => p.Date.Date == today)
            .Select(p => p.childId)
            .Distinct()
            .ToList();

        var children = _db.Childs
            .Where(c => childIds.Contains(c.Id))
            .ToList();

        if (children == null || children.Count == 0)
            return Ok("no one visiting today");

        return Ok(children);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}



            
        


     }
 }