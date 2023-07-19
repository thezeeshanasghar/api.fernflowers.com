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
        public async Task<IActionResult> GetNew(long ChildId, long DoctorId)
        {
            try
            {
                var child = await _db.Childs.FindAsync(ChildId);

                if (child == null)
                {
                    // Child with the given ID not found
                    return NotFound();
                }
                Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();

                if (!_db.PatientSchedules.Any(d => d.ChildId == ChildId && d.DoctorId == DoctorId))
                    {
                    var doctorSchedules = _db.DoctorSchedules.Where(d => d.DoctorId == DoctorId).OrderBy(d => d.Date).ToList();

                    DateOnly childDOB =DateOnly.FromDateTime(child.DOB);// Example date of birth for the child

                    DateOnly oldDate = default; // Variable to store the previous date
                    DateOnly updateDate = default; // Variable to store the updated date

                    foreach (var schedule in doctorSchedules)
                    {
                        var pdate = schedule.Date; // Store the current schedule's date in pdate

                        if (oldDate == default)
                        {
                            oldDate = pdate;
                            updateDate = childDOB; // Replace the first date with the child's date of birth
                        }
                        else
                        {
                            if (pdate != oldDate)
                            {
                                var gap = (int)(pdate.DayNumber-oldDate.DayNumber); // Calculate the gap between oldDate and pdate
                                updateDate = updateDate.AddDays(gap); // Add the gap to the updateDate
                            }
                        }

                        var newDate = updateDate;

                        var dose = await _db.Doses.FindAsync(schedule.DoseId);
                        var dto = new PatientDoseScheduleDTO
                        {
                            ScheduleId = 0, // Set to 0 as it will be generated when saved
                            DoseName = dose.Name,
                            IsSkip = false,
                            IsDone = false
                        };

                        if (dict.ContainsKey(newDate))
                            dict[newDate].Add(dto);
                        else
                            dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });

                        // Save the DoctorSchedule record.
                        var patientSchedule = new PatientSchedule
                        {
                            Date = newDate,
                            DoseId = schedule.DoseId,
                            DoctorId = DoctorId,
                            ChildId = ChildId,
                            IsDone = false
                        };
                        _db.PatientSchedules.Add(patientSchedule);
                        await _db.SaveChangesAsync();

                        dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                        oldDate = pdate; // Update the oldDate for the next iteration
                    }

                }
                else
                    {
                        // If the DoctorSchedules table already exists, get the data from it.
                        var patientSchedules = await _db.PatientSchedules.Where(d => d.DoctorId == DoctorId && d.ChildId == ChildId).ToListAsync();

                        foreach (var patientSchedule in patientSchedules)
                        {
                            var newDate = patientSchedule.Date;
                            var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone
                            };

                            if (dict.ContainsKey(newDate))
                                dict[newDate].Add(dto);
                            else
                                dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
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
        public async Task<IActionResult> Update(long Id,[FromBody] PatientSchedule ps)
        {
            try
            {
                var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.Id == ps.Id);
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

        [Route("single_updateDone")]
        [HttpPatch]
        public async Task<IActionResult> UpdateDone(long Id,[FromBody] PatientSchedule ps)
        {
            try
            {
                var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.Id==ps.Id);
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
        public async Task<IActionResult> UpdateSkip(long Id,[FromBody] PatientSchedule ps)
        {
            try
            {
               var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.Id==ps.Id);
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

        [Route("patient_bulk_updateDone")]
        [HttpPatch()]
        public async Task<IActionResult> UpdateIsDone(long childId,string date, bool isDone)
        {
            try
            {
                var parsedFromDate = System.DateOnly.Parse(date);
                var dbPS = await _db.PatientSchedules
                    .Where(d => d.ChildId == childId && d.Date.Equals(parsedFromDate))
                    .ToListAsync();

                if (dbPS == null || dbPS.Count == 0)
                {
                    return NotFound();
                }

                foreach (var patientSchedule in dbPS)
                {
                    patientSchedule.IsDone = isDone;
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("patient_bulk_update_IsSkip")]
        [HttpPatch()]
        public async Task<IActionResult> UpdateIsSkip(long childId,string date, bool IsSkip)
        {
            try
            {
                var parsedFromDate = System.DateOnly.Parse(date);
                var dbPS = await _db.PatientSchedules
                    .Where(d => d.ChildId == childId && d.Date.Equals(parsedFromDate))
                    .ToListAsync();

                if (dbPS == null || dbPS.Count == 0)
                {
                    return NotFound();
                }

                foreach (var patientSchedule in dbPS)
                {
                    patientSchedule.IsSkip = IsSkip;
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("patient_bulk_update_Date")]
        [HttpPatch]
        public async Task<IActionResult> UpdateBulkDate(long ChildId,long DoctorId,string oldDate, string newDate)
        {
            try
            {
                var parsedOldDate = System.DateOnly.Parse(oldDate);
                var parsedNewDate = System.DateOnly.Parse(newDate);

                var db = await _db.PatientSchedules
                    .Where(d =>d.ChildId==ChildId && d.DoctorId==DoctorId && d.Date.Equals(parsedOldDate))
                    .ToListAsync();

                if (db == null || db.Count == 0)
                {
                    return NotFound();
                }

                foreach (var Schedule in db)
                {
                    Schedule.Date = parsedNewDate;
            
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
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
