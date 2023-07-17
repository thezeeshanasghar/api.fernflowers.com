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
    public class PatientScheduleController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;

        public PatientScheduleController(VaccineDBContext vaccineDBContext,IMapper mapper)
        {
            _db = vaccineDBContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Patient_DoseSchedule")]
        public async Task<IActionResult> GetNew(long ChildId,long DoctorId)
        {
            try
            {
                Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();

                // Check if the PatientSchedules table already exists.
                if (!_db.PatientSchedules.Any(d=>d.ChildId==ChildId && d.DoctorId==DoctorId))
                {
                    // If not, get the data from the doctorSchedules table and save it in the patientSchedules table.
                    var doctorSchedules =  _db.DoctorSchedules.Where(d => d.DoctorId == DoctorId).ToList();

                    foreach (var Schedule in doctorSchedules)
                    {
                        var newDate = (Schedule.Date);
                        var dose = await _db.Doses.FindAsync(Schedule.DoseId);
                        var dto = _mapper.Map<DoseDTO>(dose);

                        if (dict.ContainsKey(newDate))
                            dict[newDate].Add(dto);
                        else
                            dict.Add(newDate, new List<DoseDTO>() { dto });

                        // Save the DoctorSchedule record.
                        var patientSchedule = new PatientSchedule
                        {
                            Date = newDate,
                            DoseId = Schedule.DoseId,
                            DoctorId = DoctorId,
                            ChildId=ChildId
                        };
                        _db.PatientSchedules.Add(patientSchedule);
                    }

                    await _db.SaveChangesAsync();
                }
                else
                {
                    // If the DoctorSchedules table already exists, get the data from it.
                    var patientSchedules = await _db.PatientSchedules.Where(d => d.DoctorId == DoctorId && d.ChildId==ChildId).ToListAsync();

                    foreach (var patientSchedule in patientSchedules)
                    {
                        var newDate = (patientSchedule.Date);
                        var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);
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
    
        [Route("single_updateDate")]
        [HttpPatch]
        public async Task<IActionResult> Update(long DoseId,long DoctorId,long ChildId,[FromBody] PatientSchedule ps)
        {
            try
            {
                var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.DoctorId == ps.DoctorId && d.DoseId==ps.DoseId && d.ChildId==ps.ChildId);
                if (dbps == null)
                {
                    return NotFound();
                }

                dbps.Date = ps.Date;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [Route("patient_bulk_updateDone/{childId}/{date}")]
        // [HttpPatch]
        // public async Task<IActionResult> PatchAsync(int childId, DateTime date, bool isDone, [FromBody] JsonPatchDocument<PatientSchedule> patchDocument)
        // {
        //     try
        //     {
        //         var dbPS = _db.PatientSchedules.Where(d => d.isDone == isDone && d.childId == childId && d.Date == date).ToList();
        //         if (dbPS == null)
        //         {
        //             return NotFound();
        //         }
        //         dbPS.ForEach(d => patchDocument.ApplyTo(d));
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        [Route("single_updateDone")]
        [HttpPatch]
        public async Task<IActionResult> UpdateDone(long DoseId,long DoctorId,long ChildId,[FromBody] PatientSchedule ps)
        {
            try
            {
                var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.DoctorId == ps.DoctorId && d.DoseId==ps.DoseId && d.ChildId==ps.ChildId);
                if (dbps == null)
                {
                    return NotFound();
                }

                dbps.IsDone = ps.IsDone;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("single_update_Skip")]
        [HttpPatch]
        public async Task<IActionResult> UpdateSkip(long DoseId,long DoctorId,long ChildId,[FromBody] PatientSchedule ps)
        {
            try
            {
               var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.DoctorId == ps.DoctorId && d.DoseId==ps.DoseId && d.ChildId==ps.ChildId);
                if (dbps == null)
                {
                    return NotFound();
                }

                dbps.IsSkip = ps.IsSkip;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [Route("patient_bulk_updateDate/{date}")]
        // [HttpPatch]
        // public async Task<IActionResult> PatchAsync(
        //     int childId,
        //     DateTime date,
        //     [FromBody] JsonPatchDocument<PatientSchedule> patchDocument
        // )
        // {
        //     try
        //     {
        //         var dbPS = _db.PatientSchedules.Where(d => d.childId == childId && d.Date.Date == date.Date).ToList();
        //         if (dbPS == null)
        //         {
        //             return NotFound();
        //         }
        //         dbPS.ForEach(d => patchDocument.ApplyTo(d));
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        // [Route("patient_bulk_update_isSkip")]
        // [HttpPatch]
        // public async Task<IActionResult> PatchisSKip(
        //     int childId,
        //     DateTime date,
        //     [FromBody] JsonPatchDocument<PatientSchedule> patchDocument
        // )
        // {
        //     try
        //     {
        //         var dbPS = _db.PatientSchedules.Where(d=>d.Date.Date==date.Date && d.childId == childId).ToList();
        //         if (dbPS == null)
        //             return NotFound();

        //         dbPS.ForEach(d => patchDocument.ApplyTo(d));
        //         await _db.SaveChangesAsync();
        //         return NoContent();
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
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                var childIds = _db.PatientSchedules
                    .Where(p => p.Date == today)
                    .Select(p => p.ChildId)
                    .Distinct()
                    .ToList();

                var children = _db.Childs.Where(c => childIds.Contains(c.Id)).ToList();

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
