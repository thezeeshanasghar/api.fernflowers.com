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

       
       [Route("doctor_post_schedule/child")]
        [HttpPost]
        public async Task<IActionResult> GetAndSaveWithChildId(int doctorId, int childId)
        {
            try
            {
                if (_db.PatientSchedules.Any(c => c.childId == childId ))
                {
                    return Ok("Schedule already exists");
                }

                var dsSchedule = _db.DoctorSchedules.Where(d => d.DoctorId == doctorId).ToList();

                List<PattientsSchedule> patientScheduleList = new List<PattientsSchedule>();

                foreach (var ds in dsSchedule)
                {
                    var doseScheduleEntry = new PattientsSchedule
                    {
                        Date = ds.Date,
                        DoseId = ds.DoseId,
                        DoctorId = doctorId,
                        childId = childId  // Add the child ID to each schedule entry
                    };

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


 

     
  


    }
}