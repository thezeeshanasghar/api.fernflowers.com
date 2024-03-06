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
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Cors;
using System.Linq;
using System.Xml.XPath;
using System.Runtime.Intrinsics.X86;

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

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateIsDone(long id,bool isDone)
    {
        var patientSchedule = await _db.PatientSchedules.FindAsync(id);
        if (patientSchedule == null)
        {
            return NotFound();
        }

        patientSchedule.IsDone = isDone;

        // Reset other properties if IsDone is false
        if (!isDone)
        {
            // Reset other properties here
            patientSchedule.GivenDate = null;
            patientSchedule.BrandId = null;
            patientSchedule.Height = null;
            patientSchedule.Weight = null;
            patientSchedule.OFC = null;
            // You can reset other properties as needed
        }

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PatientScheduleExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool PatientScheduleExists(long id)
    {
        throw new NotImplementedException();
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
                    return NotFound();
                }

                Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();

                if (!child.IsSpecial)
                {
                    if (!_db.PatientSchedules.Any(d => d.ChildId == ChildId && d.DoctorId == DoctorId))
                    {
                        var doctorSchedules = _db.DoctorSchedules.Where(d => d.DoctorId == DoctorId).OrderBy(d => d.Date).ToList();

                        DateOnly childDOB = child.DOB; // Example date of birth for the child

                        DateOnly oldDate = default; // Variable to store the previous date
                        DateOnly updateDate = default; // Variable to store the updated date
                        var childgender = child.Gender;
                        foreach (var schedule in doctorSchedules)
                        {
                            var dose3 = await _db.Doses.FindAsync(schedule.DoseId);
                            long vaccineId = dose3.VaccineId;

                            // Query the Vaccine table based on the VaccineId
                            var vaccine = await _db.Vaccines.FindAsync(vaccineId);


                            var vaccineName = vaccine.Name;
                            // ... other vaccine properties

                            if (childgender == 0 && vaccineName == "HPV")
                            {
                                continue;
                            }




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
                                    var gap = (int)(pdate.DayNumber - oldDate.DayNumber); // Calculate the gap between oldDate and pdate
                                    updateDate = updateDate.AddDays(gap); // Add the gap to the updateDate
                                }
                            }

                            var newDate = updateDate;

                            // var dose = await _db.Doses
                            //     .Join(_db.Vaccines,
                            //         dose => dose.VaccineId,
                            //         vaccine => vaccine.Id,
                            //         (dose, vaccine) => new { Dose = dose, Vaccine = vaccine })
                            //     .Where(x => !x.Vaccine.IsSpecial && x.Dose.Id == schedule.DoseId)
                            //     .Select(x => x.Dose)
                            //     .FirstOrDefaultAsync();
                               var dose = await _db.Doses
                                .Where(d => !d.IsSpecial && d.Id == schedule.DoseId)
                                .FirstOrDefaultAsync(); 

                            if (dose == null)
                            {
                                // Dose not found, set the properties to null
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = 0, // Set to 0 as it will be generated when saved
                                    DoseName = null,
                                    IsSkip = false,
                                    IsDone = false,
                                    BrandName = null,


                                };

                                if (dict.ContainsKey(newDate))
                                    dict[newDate].Add(dto);
                                else
                                    dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });

                                // Save the DoctorSchedule record with DoseId set to null (optional)
                                var patientSchedule = new PatientSchedule
                                {
                                    Date = newDate,
                                    DoseId = null, // Set DoseId to null or remove this line if you prefer
                                    DoctorId = DoctorId,
                                    ChildId = ChildId,
                                    IsDone = false,
                                    BrandId = null,
                                    GivenDate = null,
                                    IsSpecial = true,
                                };
                                //_db.PatientSchedules.Add(patientSchedule);
                                //await _db.SaveChangesAsync();

                                dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                                oldDate = pdate; // Update the oldDate for the next iteration
                               
                            }
                            else
                            {
                                // Dose found, continue with the existing code to create the dto
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = 0, // Set to 0 as it will be generated when saved
                                    DoseName = dose.Name,
                                    IsSkip = false,
                                    IsDone = false,
                                    IsSpecial=schedule.SelectedDose,
                                    // BrandName = brand.Name (you can remove this line since we don't need it when dose is deleted)
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
                                    IsDone = false,
                                    BrandId = null,
                                    GivenDate = null,
                                    IsSpecial = schedule.SelectedDose,
                                };

                                _db.PatientSchedules.Add(patientSchedule);
                               
                               
                                await _db.SaveChangesAsync();


                                dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                                oldDate = pdate; // Update the oldDate for the next iteration
                            }
                            
                            
                        }
                    }

                    Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict2 = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();
                    var patientSchedules = await _db.PatientSchedules.Where(d => d.DoctorId == DoctorId && d.ChildId == ChildId).ToListAsync();

                    foreach (var patientSchedule in patientSchedules)
                    {
                        var newDate = patientSchedule.Date;
                        var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);
                        var brand = await _db.Brands.FindAsync(patientSchedule.BrandId);

                        if (dose == null || brand == null)
                        {
                            // Dose or Brand not found, set the properties to null
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose?.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone,
                                BrandName = brand?.Name,
                                IsSpecial=patientSchedule.IsSpecial,

                            };

                            if (dict2.ContainsKey(newDate))
                                dict2[newDate].Add(dto);
                            else
                                dict2.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                        }
                        else
                        {
                            // Dose and Brand found, continue with the existing code to create the dto
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone,
                                BrandName = brand.Name,
                                IsSpecial=patientSchedule.IsSpecial,
                            };

                            if (dict2.ContainsKey(newDate))
                                dict2[newDate].Add(dto);
                            else
                                dict2.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                        }
                    }

                    var sortedDict = dict2.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    return Ok(sortedDict);
                }
                else
                {
                    if (!_db.PatientSchedules.Any(d => d.ChildId == ChildId && d.DoctorId == DoctorId))
                    {
                        var doctorSchedules = _db.DoctorSchedules.Where(d => d.DoctorId == DoctorId).OrderBy(d => d.Date).ToList();

                        DateOnly childDOB = child.DOB; // Example date of birth for the child

                        DateOnly oldDate = default; // Variable to store the previous date
                        DateOnly updateDate = default; // Variable to store the updated date
                        var childgender = Gender.Boy;

                        foreach (var schedule in doctorSchedules)
                        {
                            var dose3 = await _db.Doses.FindAsync(schedule.DoseId);
                            long vaccineId = dose3.VaccineId;

                            // Query the Vaccine table based on the VaccineId
                            var vaccine = await _db.Vaccines.FindAsync(vaccineId);


                            var vaccineName = vaccine.Name;
                            // ... other vaccine properties

                            if (childgender == Gender.Boy && vaccineName == "HPV")
                            {
                                continue;
                            }

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
                                    var gap = (int)(pdate.DayNumber - oldDate.DayNumber); // Calculate the gap between oldDate and pdate
                                    updateDate = updateDate.AddDays(gap); // Add the gap to the updateDate
                                }
                            }

                            var newDate = updateDate;

                            var dose = await _db.Doses.FindAsync(schedule.DoseId);
                            


                            if (dose == null)
                            {
                                // Dose not found, set the properties to null
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = 0, // Set to 0 as it will be generated when saved
                                    DoseName = null,
                                    IsSkip = false,
                                    IsDone = false,
                                    BrandName = null
                                };

                                if (dict.ContainsKey(newDate))
                                    dict[newDate].Add(dto);
                                else
                                    dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });

                                // Save the DoctorSchedule record with DoseId set to null (optional)
                                var patientSchedule = new PatientSchedule
                                {
                                    Date = newDate,
                                    DoseId = null, // Set DoseId to null or remove this line if you prefer
                                    DoctorId = DoctorId,
                                    ChildId = ChildId,
                                    IsDone = false,
                                    BrandId = null,
                                    GivenDate = null,

                                };
                                //_db.PatientSchedules.Add(patientSchedule);
                                //await _db.SaveChangesAsync();

                                dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                                oldDate = pdate; // Update the oldDate for the next iteration
                            }
                            else
                            {
                                // Dose found, continue with the existing code to create the dto
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = 0, // Set to 0 as it will be generated when saved
                                    DoseName = dose.Name,
                                    IsSkip = false,
                                    IsDone = false,
                                    // BrandName = brand.Name (you can remove this line since we don't need it when dose is deleted)
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
                                    IsDone = false,
                                    BrandId = null,
                                    GivenDate = null,
                                    IsSpecial=schedule.SelectedDose,

                                };
                                

                                
                                _db.PatientSchedules.Add(patientSchedule);
                                await _db.SaveChangesAsync();

                                dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                                oldDate = pdate; // Update the oldDate for the next iteration
                            }
                            
                        }
                    }

                    Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict2 = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();
                    var patientSchedules = await _db.PatientSchedules.Where(d => d.DoctorId == DoctorId && d.ChildId == ChildId).ToListAsync();

                    foreach (var patientSchedule in patientSchedules)
                    {
                        var newDate = patientSchedule.Date;
                        var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);
                        var brand = await _db.Brands.FindAsync(patientSchedule.BrandId);

                        if (dose == null || brand == null)
                        {
                            // Dose or Brand not found, set the properties to null
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose?.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone,
                                BrandName = brand?.Name,
                                IsSpecial=patientSchedule.IsSpecial,
                            };

                            if (dict2.ContainsKey(newDate))
                                dict2[newDate].Add(dto);
                            else
                                dict2.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                        }
                        else
                        {
                            // Dose and Brand found, continue with the existing code to create the dto
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone,
                                BrandName = brand.Name,
                                IsSpecial=patientSchedule.IsSpecial,
                            };

                            if (dict2.ContainsKey(newDate))
                                dict2[newDate].Add(dto);
                            else
                                dict2.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                        }
                    }

                    var sortedDict = dict2.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    return Ok(sortedDict);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetMissingDoses")]
        public async Task<IActionResult> GetMissingDoses(long ChildId, long DoctorId)
        {
            try
            {
                var child = await _db.Childs.FindAsync(ChildId);

                if (child == null)
                {
                    return NotFound("Child not found");
                }

                List<DoctorScheduleDTO> missingDoses = new List<DoctorScheduleDTO>();

                
                    var doctorSchedules = await _db.DoctorSchedules
                        .Where(ds => ds.DoctorId == DoctorId)
                        .OrderBy(ds => ds.Date)
                        .ToListAsync();

                    var patientSchedules = await _db.PatientSchedules
                        .Where(ps => ps.ChildId == ChildId && ps.DoctorId == DoctorId)
                        .ToListAsync();

                    // Extract DoseIds from patient schedules
                    var patientDoseIds = patientSchedules.Select(ps => ps.DoseId).ToList();

                    // Find missing doses from doctor schedules
                    foreach (var doctorSchedule in doctorSchedules)
                    {
                        if (!patientDoseIds.Contains(doctorSchedule.DoseId))
                        {
                            missingDoses.Add(new DoctorScheduleDTO
                            {
                                Id = doctorSchedule.Id,
                                DoseId = doctorSchedule.DoseId,
                                Date = doctorSchedule.Date,
                                DoctorId=doctorSchedule.DoctorId,
                                SelectedDose=doctorSchedule.SelectedDose
                                // Add other properties from DoctorScheduleDTO as needed
                            });
                        }
                    }



                //return Ok(missingDoses.OrderBy(d => d.Date).ToList());

                if (missingDoses.Count != 0)
                {



                    Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();

                    if (!child.IsSpecial)
                    {

                        var newdoses = missingDoses.OrderBy(d => d.Date).ToList();

                        DateOnly childDOB = child.DOB; // Example date of birth for the child

                        DateOnly oldDate = default; // Variable to store the previous date
                        var childgender=child.Gender;
                        DateOnly updateDate = default; // Variable to store the updated date

                        foreach (var schedule in newdoses)
                        {
                            var dose3 = await _db.Doses.FindAsync(schedule.DoseId);
                            long vaccineId = dose3.VaccineId;

                            // Query the Vaccine table based on the VaccineId
                            var vaccine = await _db.Vaccines.FindAsync(vaccineId);


                            var vaccineName = vaccine.Name;
                            // ... other vaccine properties

                            if (childgender == 0 && vaccineName == "HPV")
                            {
                                continue;
                            }
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
                                    var gap = (int)(pdate.DayNumber - oldDate.DayNumber); // Calculate the gap between oldDate and pdate
                                    updateDate = updateDate.AddDays(gap); // Add the gap to the updateDate
                                }
                            }

                            var newDate = updateDate;

                            // var dose = await _db.Doses
                            //     .Join(_db.Vaccines,
                            //         dose => dose.VaccineId,
                            //         vaccine => vaccine.Id,
                            //         (dose, vaccine) => new { Dose = dose, Vaccine = vaccine })
                            //     .Where(x => !x.Vaccine.IsSpecial && x.Dose.Id == schedule.DoseId)
                            //     .Select(x => x.Dose)
                            //     .FirstOrDefaultAsync();
                            var dose = await _db.Doses
    .Where(d => !d.IsSpecial && d.Id == schedule.DoseId)
    .FirstOrDefaultAsync();

                            if (dose == null)
                            {
                                // Dose not found, set the properties to null
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = 0, // Set to 0 as it will be generated when saved
                                    DoseName = null,
                                    IsSkip = false,
                                    IsDone = false,
                                    BrandName = null
                                };

                                if (dict.ContainsKey(newDate))
                                    dict[newDate].Add(dto);
                                else
                                    dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });

                                var patientSchedule = new PatientSchedule
                                {
                                    Date = newDate,
                                    DoseId = schedule.DoseId, // Set DoseId to null or remove this line if you prefer
                                    DoctorId = DoctorId,
                                    ChildId = ChildId,
                                    IsDone = false,
                                    BrandId = null,
                                    GivenDate = null,
                                    IsSpecial=schedule.SelectedDose,
                                };
                                _db.PatientSchedules.Add(patientSchedule);
                                await _db.SaveChangesAsync();

                                dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                                oldDate = pdate; // Update the oldDate for the next iteration
                            }
                            else
                            {
                                // Dose found, continue with the existing code to create the dto
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = 0, // Set to 0 as it will be generated when saved
                                    DoseName = dose.Name,
                                    IsSkip = false,
                                    IsDone = false,
                                    // BrandName = brand.Name (you can remove this line since we don't need it when dose is deleted)
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
                                    IsDone = false,
                                    BrandId = null,
                                    GivenDate = null,
                                    IsSpecial=schedule.SelectedDose,
                                };
                                _db.PatientSchedules.Add(patientSchedule);
                                await _db.SaveChangesAsync();

                                dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                                oldDate = pdate; // Update the oldDate for the next iteration
                            }
                        }


                        Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict3 = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();
                        var patientSchedules2 = await _db.PatientSchedules.Where(d => d.DoctorId == DoctorId && d.ChildId == ChildId).ToListAsync();

                        foreach (var patientSchedule in patientSchedules2)
                        {
                            var newDate = patientSchedule.Date;
                            var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);
                            var brand = await _db.Brands.FindAsync(patientSchedule.BrandId);

                            if (dose == null || brand == null)
                            {
                                // Dose or Brand not found, set the properties to null
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = patientSchedule.Id,
                                    DoseName = dose?.Name,
                                    IsSkip = patientSchedule.IsSkip,
                                    IsDone = patientSchedule.IsDone,
                                    BrandName = brand?.Name,
                                    IsSpecial=patientSchedule.IsSpecial
                                };

                                if (dict3.ContainsKey(newDate))
                                    dict3[newDate].Add(dto);
                                else
                                    dict3.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                            }
                            else
                            {
                                // Dose and Brand found, continue with the existing code to create the dto
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = patientSchedule.Id,
                                    DoseName = dose.Name,
                                    IsSkip = patientSchedule.IsSkip,
                                    IsDone = patientSchedule.IsDone,
                                    BrandName = brand.Name,
                                    IsSpecial = patientSchedule.IsSpecial
                                };

                                if (dict3.ContainsKey(newDate))
                                    dict3[newDate].Add(dto);
                                else
                                    dict3.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                            }
                        }

                        var sortedDict2 = dict3.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                        return Ok( newdoses);
                    }
                    else
                    {

                        var doctorSchedules2 = missingDoses.OrderBy(d => d.Date).ToList();
                        var doctorSchedules5 = await _db.DoctorSchedules
                        .Where(ds => ds.DoctorId == DoctorId)
                        .OrderBy(ds => ds.Id)
                        .ToListAsync();

                        var smallestIdDoctorSchedule = doctorSchedules5.First();
                        var smallestDate=smallestIdDoctorSchedule.Date;

                        DateOnly childDOB = child.DOB; // Example date of birth for the child
                        var childgender=child.Gender;

                        DateOnly oldDate = default; // Variable to store the previous date
                        DateOnly updateDate = default; // Variable to store the updated date

                        foreach (var schedule in doctorSchedules2)
                        {
                            var dose3 = await _db.Doses.FindAsync(schedule.DoseId);
                            long vaccineId = dose3.VaccineId;

                            // Query the Vaccine table based on the VaccineId
                            var vaccine = await _db.Vaccines.FindAsync(vaccineId);


                            var vaccineName = vaccine.Name;
                            // ... other vaccine properties

                            if (childgender == 0 && vaccineName == "HPV")
                            {
                                continue;
                            }
                            var pdate = schedule.Date; // Store the current schedule's date in pdate

                          
                            
                            if (pdate != oldDate)
                            {
                                var gap = (int)(pdate.DayNumber - smallestDate.DayNumber); // Calculate the gap between oldDate and pdate
                                updateDate = childDOB.AddDays(gap); // Add the gap to the updateDate
                            }
                            

                            var newDate = updateDate;

                            var dose = await _db.Doses.FindAsync(schedule.DoseId);


                            if (dose == null)
                            {
                                // Dose not found, set the properties to null
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = 0, // Set to 0 as it will be generated when saved
                                    DoseName = null,
                                    IsSkip = false,
                                    IsDone = false,
                                    BrandName = null
                                };

                                if (dict.ContainsKey(newDate))
                                    dict[newDate].Add(dto);
                                else
                                    dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });

                                // Save the DoctorSchedule record with DoseId set to null (optional)
                                var patientSchedule = new PatientSchedule
                                {
                                    Date = newDate,
                                    DoseId = null, // Set DoseId to null or remove this line if you prefer
                                    DoctorId = DoctorId,
                                    ChildId = ChildId,
                                    IsDone = false,
                                    BrandId = null,
                                    GivenDate = null,
                                    IsSpecial = false,
                                };
                                _db.PatientSchedules.Add(patientSchedule);
                                await _db.SaveChangesAsync();

                                dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                                oldDate = pdate; // Update the oldDate for the next iteration
                            }
                            else
                            {
                                // Dose found, continue with the existing code to create the dto
                                var dto = new PatientDoseScheduleDTO
                                {
                                    ScheduleId = 0, // Set to 0 as it will be generated when saved
                                    DoseName = dose.Name,
                                    IsSkip = false,
                                    IsDone = false,
                                    // BrandName = brand.Name (you can remove this line since we don't need it when dose is deleted)
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
                                    IsDone = false,
                                    BrandId = null,
                                    GivenDate = null,
                                    IsSpecial = false,
                                };
                                _db.PatientSchedules.Add(patientSchedule);
                                await _db.SaveChangesAsync();

                                dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                                oldDate = pdate; // Update the oldDate for the next iteration
                            }
                        }
                    }

                    Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict4 = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();
                    var patientSchedules3 = await _db.PatientSchedules.Where(d => d.DoctorId == DoctorId && d.ChildId == ChildId).ToListAsync();

                    foreach (var patientSchedule in patientSchedules3)
                    {
                        var newDate = patientSchedule.Date;
                        var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);
                        var brand = await _db.Brands.FindAsync(patientSchedule.BrandId);

                        if (dose == null || brand == null)
                        {
                            // Dose or Brand not found, set the properties to null
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose?.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone,
                                BrandName = brand?.Name,
                                IsSpecial = patientSchedule.IsSpecial
                            };

                            if (dict4.ContainsKey(newDate))
                                dict4[newDate].Add(dto);
                            else
                                dict4.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                        }
                        else
                        {
                            // Dose and Brand found, continue with the existing code to create the dto
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone,
                                BrandName = brand.Name,
                                IsSpecial = patientSchedule.IsSpecial
                            };

                            if (dict4.ContainsKey(newDate))
                                dict4[newDate].Add(dto);
                            else
                                dict4.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                        }
                    }

                    var sortedDict = dict4.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    return Ok("added2");
                }
                else
                {
                    return Ok("No new doses");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





        
        [Route("single_updateDate")]
        [HttpPatch]
        public async Task<IActionResult> Update(long Id,[FromBody] PatientScheduleDTO ps)
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
        public async Task<IActionResult> UpdateDone(long Id,[FromBody] PatientScheduleDTO ps)
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
                dbps.BrandId = ps.BrandId; 
                dbps.GivenDate= ps.GivenDate;
                dbps.IsDone = ps.IsDone;

                dbps.Height = ps.Height;
                dbps.Weight = ps.Weight;
                dbps.OFC = ps.OFC;
                _db.Entry(dbps).State = EntityState.Modified;
                //await _db.SaveChangesAsync();

                if (dbps.DoseId!=0)
                {
                    var dose = await _db.Doses
                        .FirstOrDefaultAsync(d => d.Id == dbps.DoseId);
                    string doseName = dose.Name;
                    var child = await _db.Childs.FindAsync(dbps.ChildId);
                    DateOnly dob = child.DOB;
                    DateOnly newchilddob = dob.AddYears(2);

                    if (doseName == "MenACWY # 1" && dbps.GivenDate> newchilddob)
                    {
                        var dose2 = await _db.Doses.FirstOrDefaultAsync(d => d.Name == "MenACWY # 2");

                        if (dose2 != null)
                        {
                            var patientScheduleWithDose2 = await _db.PatientSchedules.FirstOrDefaultAsync(ps => ps.DoseId == dose2.Id);

                            if (patientScheduleWithDose2 != null)
                            {
                                // Set IsSkip to true
                                patientScheduleWithDose2.IsSkip = true;

                                // Optionally, you can set other properties related to the "MenACWY # 2" dose
                                // Update other properties as needed

                                // Save changes to the database
                                await _db.SaveChangesAsync();
                            }
                        }

                    }


                        if (dose != null)
                    {
                        var vaccine = await _db.Vaccines
                            .FirstOrDefaultAsync(v => v.Id == dose.VaccineId);

                        string additionalVaccineName = vaccine?.Name;

                        if (vaccine.Infinite==true || additionalVaccineName == "Flu")
                        {
                            DateOnly newDate;

                            bool containsYear = dose.MinAgeText.ToLower().Contains("year");
                            bool containsMonth = dose.MinAgeText.ToLower().Contains("months");

                            if (containsYear && containsMonth)
                            {
                                // If both "year" and "month" are present, handle accordingly
                                string numericYearPart = new string(dose.MinAgeText
                                .TakeWhile(char.IsDigit)
                                .ToArray());
                                string numericMonthPart = new string(dose.MinAgeText
                                    .SkipWhile(c => char.IsDigit(c))
                                    .Where(c => char.IsDigit(c) || char.IsWhiteSpace(c))
                                    .ToArray());

                                if (int.TryParse(numericYearPart, out int yearValue) &&
                                    int.TryParse(numericMonthPart, out int monthValue))
                                {
                                    // Add both years and months to the startingDate without changing the day
                                    newDate = dbps.Date.AddYears(yearValue).AddMonths(monthValue);

                                }
                            }
                            else if (containsYear)
                            {
                                // If only "year" is present, add years to the startingDate without changing day and month
                                string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                                if (int.TryParse(numericPart, out int minAgeValue))
                                {
                                    newDate = dbps.Date.AddYears(minAgeValue);

                                }
                            }
                            else if (containsMonth)
                            {
                                // If only "month" is present, add months to the startingDate without changing day
                                string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                                if (int.TryParse(numericPart, out int minAgeValue))
                                {
                                    newDate = dbps.Date.AddMonths(minAgeValue);

                                }
                            }
                            else
                            {
                                // If neither "year" nor "month" is present, add MinAge days to the startingDate
                                newDate = dbps.Date.AddDays(dose.MinAge);

                            }

                            // Add a new PatientSchedule entry with multiplied date
                            var newSchedule = new PatientSchedule
                            {
                                DoseId = dbps.DoseId,
                                DoctorId = dbps.DoctorId,
                                ChildId = dbps.ChildId,
                                IsDone = false,
                                BrandId = null,
                                Date = newDate,
                                GivenDate=null,
                                IsSkip = false,
                                IsSpecial = true,
                                IsSpecial2 = false
                            };

                            _db.PatientSchedules.Add(newSchedule);
                        }
                    }
                }
                await _db.SaveChangesAsync();
                


                    return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetBrandForPatientSchedule")]
        public async Task<IActionResult> GetBrandForPatientSchedule(long Id)
        {
            try
            {
                // Find the patient schedule by its ID
                var patientSchedule = await _db.PatientSchedules.FindAsync(Id);

                if (patientSchedule == null)
                {
                    return NotFound("Patient schedule not found");
                }

                // Find the dose by its ID
                var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);

                if (dose == null)
                {
                    return NotFound("Dose not found");
                }

                // Get all brands that are associated with the given doseId
                var brandsForDose = await _db.Brands
                    .Where(brand => brand.VaccineId == dose.VaccineId)
                    .ToListAsync();

                return Ok(brandsForDose);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("single_update_Skip")]
        [HttpPatch]
        public async Task<IActionResult> UpdateSkip(long Id,[FromBody] PatientScheduleDTO ps)
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
                if (dbps.DoseId != 0)
                {
                    var dose = await _db.Doses
                        .FirstOrDefaultAsync(d => d.Id == dbps.DoseId);

                    if (dose != null)
                    {
                        var vaccine = await _db.Vaccines
                            .FirstOrDefaultAsync(v => v.Id == dose.VaccineId);

                        string additionalVaccineName = vaccine?.Name;
                        var doseIdToDelete = ps.DoseId;
                        var currentRowId = ps.Id;
                        

                        if (vaccine.Infinite == true || additionalVaccineName == "Flu")
                        {
                            if (ps.IsSkip == false)
                            {

                                // Find rows with the same DoseId and greater Id
                                var rowsToDelete = await _db.PatientSchedules
                                    .Where(d => d.DoseId == doseIdToDelete && d.Id > currentRowId)
                                    .ToListAsync();

                                // Delete the found rows
                                foreach (var row in rowsToDelete)
                                {
                                    _db.PatientSchedules.Remove(row);
                                }

                            }
                            else
                            {
                                DateOnly newDate;

                                bool containsYear = dose.MinAgeText.ToLower().Contains("year");
                                bool containsMonth = dose.MinAgeText.ToLower().Contains("months");

                                if (containsYear && containsMonth)
                                {
                                    // If both "year" and "month" are present, handle accordingly
                                    string numericYearPart = new string(dose.MinAgeText
                                    .TakeWhile(char.IsDigit)
                                    .ToArray());
                                    string numericMonthPart = new string(dose.MinAgeText
                                        .SkipWhile(c => char.IsDigit(c))
                                        .Where(c => char.IsDigit(c) || char.IsWhiteSpace(c))
                                        .ToArray());

                                    if (int.TryParse(numericYearPart, out int yearValue) &&
                                        int.TryParse(numericMonthPart, out int monthValue))
                                    {
                                        // Add both years and months to the startingDate without changing the day
                                        newDate = dbps.Date.AddYears(yearValue).AddMonths(monthValue);

                                    }
                                }
                                else if (containsYear)
                                {
                                    // If only "year" is present, add years to the startingDate without changing day and month
                                    string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                                    if (int.TryParse(numericPart, out int minAgeValue))
                                    {
                                        newDate = dbps.Date.AddYears(minAgeValue);

                                    }
                                }
                                else if (containsMonth)
                                {
                                    // If only "month" is present, add months to the startingDate without changing day
                                    string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                                    if (int.TryParse(numericPart, out int minAgeValue))
                                    {
                                        newDate = dbps.Date.AddMonths(minAgeValue);

                                    }
                                }
                                else
                                {
                                    // If neither "year" nor "month" is present, add MinAge days to the startingDate
                                    newDate = dbps.Date.AddDays(dose.MinAge);

                                }

                                // Add a new PatientSchedule entry with multiplied date
                                var newSchedule = new PatientSchedule
                                {
                                    DoseId = dbps.DoseId,
                                    DoctorId = dbps.DoctorId,
                                    ChildId = dbps.ChildId,
                                    IsDone = false,
                                    BrandId = null,
                                    Date = newDate,
                                    GivenDate = null,
                                    IsSkip = false,
                                    IsSpecial = true,
                                    IsSpecial2 = false
                                };

                                _db.PatientSchedules.Add(newSchedule);
                            }
                            


                        }
                    }
                }
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
        public async Task<IActionResult> UpdateBrandIdAndDate([FromBody] List<PatientScheduleUpdateDTO> updateData)
        {
            try
            {
                foreach (var updateItem in updateData)
                {
                    var parsedCurrentDate = System.DateOnly.Parse(updateItem.CurrentDate);
                    var parsedNewDate = System.DateOnly.Parse(updateItem.GivenDate);

                    var dbPS = await _db.PatientSchedules
                        .Where(d => d.Id == updateItem.Id && d.Date.Equals(parsedCurrentDate))
                        .ToListAsync();

                    if (dbPS == null || dbPS.Count == 0)
                    {
                        return NotFound();
                    }
                    foreach (var record in dbPS)
                    {
                        if (!updateItem.IsSkip) // Check if IsSkip is false
                        {
                            record.IsDone = updateItem.IsDone;
                            record.GivenDate = parsedNewDate;
                            record.BrandId = updateItem.BrandId;
                            record.Height= updateItem.Height;
                            record.Weight = updateItem.Weight;
                            record.OFC = updateItem.OFC;

                        }

                        if (record.DoseId != 0)
                        {
                            var dose = await _db.Doses
                        .FirstOrDefaultAsync(d => d.Id == record.DoseId);
                            string doseName = dose.Name;
                            var child = await _db.Childs.FindAsync(record.ChildId);
                            DateOnly dob = child.DOB;
                            
                            DateOnly newchilddob = dob.AddYears(2);

                            if (doseName == "MenACWY # 1" && record.GivenDate > newchilddob)
                            {
                                var dose2 = await _db.Doses.FirstOrDefaultAsync(d => d.Name == "MenACWY # 2");

                                if (dose2 != null)
                                {
                                    var patientScheduleWithDose2 = await _db.PatientSchedules.FirstOrDefaultAsync(ps => ps.DoseId == dose2.Id);

                                    if (patientScheduleWithDose2 != null)
                                    {
                                        // Set IsSkip to true
                                        patientScheduleWithDose2.IsSkip = true;

                                        // Optionally, you can set other properties related to the "MenACWY # 2" dose
                                        // Update other properties as needed

                                        // Save changes to the database
                                        await _db.SaveChangesAsync();
                                    }
                                }

                            }
                            if (dose != null)
                            {
                                var vaccine = await _db.Vaccines
                                    .FirstOrDefaultAsync(v => v.Id == dose.VaccineId);

                                string additionalVaccineName = vaccine?.Name;

                                if (vaccine.Infinite==true || additionalVaccineName == "Flu")
                                {
                                    DateOnly newDate;

                                    bool containsYear = dose.MinAgeText.ToLower().Contains("year");
                                    bool containsMonth = dose.MinAgeText.ToLower().Contains("months");

                                    if (containsYear && containsMonth)
                                    {
                                        // If both "year" and "month" are present, handle accordingly
                                        string numericYearPart = new string(dose.MinAgeText
                                        .TakeWhile(char.IsDigit)
                                        .ToArray());
                                        string numericMonthPart = new string(dose.MinAgeText
                                            .SkipWhile(c => char.IsDigit(c))
                                            .Where(c => char.IsDigit(c) || char.IsWhiteSpace(c))
                                            .ToArray());

                                        if (int.TryParse(numericYearPart, out int yearValue) &&
                                            int.TryParse(numericMonthPart, out int monthValue))
                                        {
                                            // Add both years and months to the startingDate without changing the day
                                            newDate = record.Date.AddYears(yearValue).AddMonths(monthValue);
  
                                        }
                                    }
                                    else if (containsYear)
                                    {
                                        // If only "year" is present, add years to the startingDate without changing day and month
                                        string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                                        if (int.TryParse(numericPart, out int minAgeValue))
                                        {
                                            newDate = record.Date.AddYears(minAgeValue);
                                           
                                        }
                                    }
                                    else if (containsMonth)
                                    {
                                        // If only "month" is present, add months to the startingDate without changing day
                                        string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                                        if (int.TryParse(numericPart, out int minAgeValue))
                                        {
                                            newDate = record.Date.AddMonths(minAgeValue);
                                            
                                        }
                                    }
                                    else
                                    {
                                        // If neither "year" nor "month" is present, add MinAge days to the startingDate
                                        newDate = record.Date.AddDays(dose.MinAge);
                                        
                                    }
                                
                                    

                                    // Add a new PatientSchedule entry with multiplied date
                                    var newSchedule = new PatientSchedule
                                    {
                                        DoseId = record.DoseId,
                                        DoctorId = record.DoctorId,
                                        ChildId = record.ChildId,
                                        IsDone = false,
                                        BrandId = null,
                                        Date = newDate,
                                        GivenDate = null,
                                        IsSkip = false,
                                        IsSpecial = true,
                                        IsSpecial2 = false
                                    };

                                    _db.PatientSchedules.Add(newSchedule);
                                    await _db.SaveChangesAsync();


                                }
                            }
                        }
                    }
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("patient_IsSpecial_doses")]
        [HttpPatch()]
        public async Task<IActionResult> UpdateIsSpecial(long childId,[FromBody] List<long> doseIds)
        {
            try
            {
                if (doseIds == null || !doseIds.Any())
                {
                    return BadRequest("Invalid input data");
                }

                // Find DoseIds for selected vaccineIds
                //var doseIds = await _db.Doses
                //    .Where(d => vaccineIds.Any(v => v == d.VaccineId))
                //    .Select(d => d.Id)
                //    .ToListAsync();

                

                        // Update IsSpecial in PatientSchedules for all doses of the matching vaccines
                foreach (var id in doseIds)
                {
                    var dbPS = await _db.PatientSchedules
                        .Where(ps => ps.ChildId == childId && id == ps.DoseId)
                        .ToListAsync();

                    if (dbPS == null || !dbPS.Any())
                    {
                        return NotFound("No matching records found");
                    }

                    foreach (var record in dbPS)
                    {
                        record.IsSpecial = true;
                        
                    }
                }

                await _db.SaveChangesAsync();
                // Get all vaccines where IsSpecial is true
            
                //var specialVaccineIds = await _db.Vaccines
                //    .Where(ps => ps.IsSpecial==true)
                //    .Select(v => v.Id)
                //    .ToListAsync();

                //var specialDoseIds = await _db.Doses
                //.Where(d => specialVaccineIds.Contains(d.VaccineId))
                //.Select(d => d.Id)
                //.ToListAsync();





     //           var specialPatientSchedulesToUpdate = await _db.PatientSchedules
     //.Where(ps => specialDoseIds.Contains(ps.DoseId ?? 0) && !ps.IsSpecial)
     //.ToListAsync();

     //           if (specialPatientSchedulesToUpdate.Count == 0)
     //           {
     //               return NotFound("No special schedules with IsSpecial set to false found.");
     //           }



     //           foreach (var record in specialPatientSchedulesToUpdate)
     //           {
     //               record.IsSpecial2 = true;
     //           }

     //           await _db.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [Route("isspecial_true")]
        [HttpPatch]
        public async Task<IActionResult> specialTrue(long ChildId, [FromBody] List<long> doseIds)
        {
            try
            {
                if (ChildId == null)
                {
                    return NotFound();
                }
                var patientSchedules = _db.PatientSchedules
                    .Where(ps => ps.ChildId == ChildId && doseIds.Contains(ps.DoseId.GetValueOrDefault()))
                    .ToList();

                foreach (var patientSchedule in patientSchedules)
                {
                    patientSchedule.IsSpecial = true;
                }

                _db.SaveChanges();

                return Ok("IsSpecial set to true for matching rows in PatientSchedule.");
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetDosesWhereIsSpecialIsFalse")]
        public ActionResult<List<Dose>> GetDosesWhereIsSpecialIsFalse(long childId)
        {
            try
            {
                var doses = _db.PatientSchedules
                    .Where(ps => ps.IsSpecial == false && ps.ChildId == childId)
                    .Join(_db.Doses, ps => ps.DoseId, dose => dose.Id, (ps, dose) => dose)
                    .ToList();
                if (doses.Count == 0)
                {
                    // Return an empty response
                    return NotFound();
                }

                return Ok(doses);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
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

                    if (patientSchedule.DoseId != 0)
                    {

                        var dose = await _db.Doses
                            .FirstOrDefaultAsync(d => d.Id == patientSchedule.DoseId);

                        if (dose != null)
                        {
                            var vaccine = await _db.Vaccines
                                .FirstOrDefaultAsync(v => v.Id == dose.VaccineId);

                            string additionalVaccineName = vaccine?.Name;



                            var doseIdToDelete = patientSchedule.DoseId;
                            var currentRowId = patientSchedule.Id;


                            if (additionalVaccineName == "Typhoid" || additionalVaccineName == "Flu")
                            {
                                if (patientSchedule.IsSkip == false)
                                {




                                    // Find rows with the same DoseId and greater Id
                                    var rowsToDelete = await _db.PatientSchedules
                                        .Where(d => d.DoseId == doseIdToDelete && d.Id > currentRowId)
                                        .ToListAsync();

                                    // Delete the found rows
                                    foreach (var row in rowsToDelete)
                                    {
                                        _db.PatientSchedules.Remove(row);
                                    }

                                }


                                else
                                {
                                    DateOnly newDate;

                                    bool containsYear = dose.MinAgeText.ToLower().Contains("year");
                                    bool containsMonth = dose.MinAgeText.ToLower().Contains("months");

                                    if (containsYear && containsMonth)
                                    {
                                        // If both "year" and "month" are present, handle accordingly
                                        string numericYearPart = new string(dose.MinAgeText
                                        .TakeWhile(char.IsDigit)
                                        .ToArray());
                                        string numericMonthPart = new string(dose.MinAgeText
                                            .SkipWhile(c => char.IsDigit(c))
                                            .Where(c => char.IsDigit(c) || char.IsWhiteSpace(c))
                                            .ToArray());

                                        if (int.TryParse(numericYearPart, out int yearValue) &&
                                            int.TryParse(numericMonthPart, out int monthValue))
                                        {
                                            // Add both years and months to the startingDate without changing the day
                                            newDate = patientSchedule.Date.AddYears(yearValue).AddMonths(monthValue);

                                        }
                                    }
                                    else if (containsYear)
                                    {
                                        // If only "year" is present, add years to the startingDate without changing day and month
                                        string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                                        if (int.TryParse(numericPart, out int minAgeValue))
                                        {
                                            newDate = patientSchedule.Date.AddYears(minAgeValue);

                                        }
                                    }
                                    else if (containsMonth)
                                    {
                                        // If only "month" is present, add months to the startingDate without changing day
                                        string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                                        if (int.TryParse(numericPart, out int minAgeValue))
                                        {
                                            newDate = patientSchedule.Date.AddMonths(minAgeValue);

                                        }
                                    }
                                    else
                                    {
                                        // If neither "year" nor "month" is present, add MinAge days to the startingDate
                                        newDate = patientSchedule.Date.AddDays(dose.MinAge);

                                    }

                                    // Add a new PatientSchedule entry with multiplied date
                                    var newSchedule = new PatientSchedule
                                    {
                                        DoseId = patientSchedule.DoseId,
                                        DoctorId = patientSchedule.DoctorId,
                                        ChildId = patientSchedule.ChildId,
                                        IsDone = false,
                                        BrandId = null,
                                        Date = newDate,
                                        GivenDate = null,
                                        IsSkip = false,
                                        IsSpecial = false,
                                        IsSpecial2 = false
                                    };

                                    _db.PatientSchedules.Add(newSchedule);


                                }
                            }
                        }
                    }

                }
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Route("patient_bulk_updateDoneWithSkip")]
        [HttpPatch()]
        public async Task<IActionResult> UpdateBrandIdAndDateWithSkip([FromBody] List<PatientScheduleUpdateDTO> updateData)
        {
            try
            {
                foreach (var updateItem in updateData)
                {
                    var parsedCurrentDate = System.DateOnly.Parse(updateItem.CurrentDate);
                    var parsedNewDate = System.DateOnly.Parse(updateItem.GivenDate);

                    var dbPS = await _db.PatientSchedules
                        .Where(d => d.Id == updateItem.Id && d.Date.Equals(parsedCurrentDate))
                        .ToListAsync();

                    if (dbPS == null || dbPS.Count == 0)
                    {
                        return NotFound();
                    }
                    foreach (var record in dbPS)
                    {
                        if (!updateItem.IsSkip) // Check if IsSkip is false
                        {
                            record.IsDone = false;
                            record.GivenDate = null;
                            record.BrandId = null;
                            record.Height = null;
                            record.Weight = null;
                            record.OFC = null;

                        }

                        if (record.DoseId != 0)
                        {
                            var dose = await _db.Doses
                        .FirstOrDefaultAsync(d => d.Id == record.DoseId);
                            string doseName = dose.Name;
                            var child = await _db.Childs.FindAsync(record.ChildId);
                            DateOnly dob = child.DOB;

                            DateOnly newchilddob = dob.AddYears(2);

                            if (doseName == "MenACWY # 1" && parsedNewDate > newchilddob)
                            {
                                var dose2 = await _db.Doses.FirstOrDefaultAsync(d => d.Name == "MenACWY # 2");

                                if (dose2 != null)
                                {
                                    var patientScheduleWithDose2 = await _db.PatientSchedules.FirstOrDefaultAsync(ps => ps.DoseId == dose2.Id);

                                    if (patientScheduleWithDose2 != null)
                                    {
                                        // Set IsSkip to true
                                        patientScheduleWithDose2.IsSkip = false;

                                        // Optionally, you can set other properties related to the "MenACWY # 2" dose
                                        // Update other properties as needed

                                        // Save changes to the database
                                        await _db.SaveChangesAsync();
                                    }
                                }

                            }
                            if (dose != null)
                            {
                                var vaccine = await _db.Vaccines
                                    .FirstOrDefaultAsync(v => v.Id == dose.VaccineId);
                                var doseIdToDelete = record.DoseId;
                                var currentRowId = record.Id;
                                string additionalVaccineName = vaccine?.Name;

                                if (vaccine.Infinite == true || additionalVaccineName == "Flu")
                                {


                                    if (record.IsSkip == false)
                                    {




                                        // Find rows with the same DoseId and greater Id
                                        var rowsToDelete = await _db.PatientSchedules
                                            .Where(d => d.DoseId == doseIdToDelete && d.Id > currentRowId)
                                            .ToListAsync();

                                        // Delete the found rows
                                        foreach (var row in rowsToDelete)
                                        {
                                            _db.PatientSchedules.Remove(row);
                                        }

                                    }





                                    // Add a new PatientSchedule entry with multiplied date

                                    await _db.SaveChangesAsync();


                                }
                            }
                        }
                    }
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("getData_baseOnDate")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientScheduleDTO>>> GetPatientSchedules(string date)
        {
            // Ensure the date includes only the date part without the time
            var parsedDate = System.DateOnly.Parse(date);

            var patientSchedules = await _db.PatientSchedules
                .Where(ps => ps.Date == parsedDate && ps.IsSpecial==true)
                .Select(ps => new PatientScheduleDTO
                {
                    Id=ps.Id,
                    Date = ps.Date,
                    DoseId = ps.DoseId.HasValue ? ps.DoseId.Value : 0,
                    DoctorId = ps.DoctorId,
                    ChildId = ps.ChildId,
                    IsSkip = ps.IsSkip,
                    IsDone = ps.IsDone,
                    BrandId = ps.BrandId
                })
                .ToListAsync();

            return Ok(patientSchedules);
        }

        [HttpGet("PatientSchedule_by_childid/{ChildId}")]
        public async Task<ActionResult<IEnumerable<PatientSchedule>>> GetPatientSchedulesByChildId(long ChildId)
        {
            Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict2 = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();

            var patientSchedules = await _db.PatientSchedules.Where(d =>  d.ChildId == ChildId && d.IsSpecial==true).ToListAsync();

            foreach (var patientSchedule in patientSchedules)
            {
                var newDate = patientSchedule.Date;
                var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);
                var brand = await _db.Brands.FindAsync(patientSchedule.BrandId);

                if (dose == null || brand == null)
                {
                    // Dose or Brand not found, set the properties to null
                    var dto = new PatientDoseScheduleDTO
                    {
                        ScheduleId = patientSchedule.Id,
                        DoseName = dose?.Name,
                        IsSkip = patientSchedule.IsSkip,
                        IsDone = patientSchedule.IsDone,
                        BrandName = brand?.Name,
                        IsSpecial = patientSchedule.IsSpecial,
                        DoseId = patientSchedule?.DoseId,
                    };

                    if (dict2.ContainsKey(newDate))
                        dict2[newDate].Add(dto);
                    else
                        dict2.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                }
                else
                {
                    // Dose and Brand found, continue with the existing code to create the dto
                    var dto = new PatientDoseScheduleDTO
                    {
                        ScheduleId = patientSchedule.Id,
                        DoseName = dose.Name,
                        IsSkip = patientSchedule.IsSkip,
                        IsDone = patientSchedule.IsDone,
                        BrandName = brand.Name,
                        IsSpecial = patientSchedule.IsSpecial,
                        DoseId = patientSchedule?.DoseId
                    };

                    if (dict2.ContainsKey(newDate))
                        dict2[newDate].Add(dto);
                    else
                        dict2.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                }
            }

            var sortedDict = dict2.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return Ok(sortedDict);
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
        [HttpGet("today_alert_BaseOnDoctor")]
        public ActionResult<IEnumerable<Child>> GetPatientsWithTodayDateAndDoctor(long doctorId)
        {
            try
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                
                // Filter patients by date and doctor ID
                var childIds = _db.PatientSchedules
                    .Where(p => p.Date == today && p.DoctorId == doctorId)
                    .Select(p => p.ChildId)
                    .Distinct()
                    .ToList();

                var children = _db.Childs.Where(c => childIds.Contains(c.Id)).ToList();

                if (children == null || children.Count == 0)
                    return NoContent();

                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("DownLoad-Pdf")]
        [HttpGet]
        public IActionResult DownloadSchedulePDF(long ChildId)
        {
            var query = from child in _db.Childs
                        join doctor in _db.Doctors on child.DoctorId equals doctor.Id
                        join clinic in _db.Clinics on child.ClinicId equals clinic.Id

                        where child.Id == ChildId
                        select new
                        {
                            ChildId = child.Id,
                            ChildName = child.Name,
                            ChildGuardianName = child.GuardianName,
                            ChildMobileNumber = child.MobileNumber,
                            ChildCnicOrPassport = child.CnicOrPassPort,
                            ChildSelectCnicOrPassport = child.SelectCnicOrPassport,
                            Gender = child.Gender,
                            DoctorId = doctor.Id,
                            DoctorName = doctor.Name,
                            DoctorEmail = doctor.Email,
                            DoctorMobileNumber = doctor.MobileNumber,
                            ClinicName = clinic.Name,
                            ClinicAddress = clinic.Address,
                            ClinicNumber = clinic.Number,
                            ClinicCity = clinic.City,

                        };
            var result = query.FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }
            string ChildName = result.ChildName;


            var stream = CreateSchedulePdf(ChildId);
            var FileName =
                ChildName.Replace(" ", "") +
                "_Schedule_" +
                DateTime.UtcNow.AddHours(5).ToString("MMMM-dd-yyyy") +
                ".pdf";
            return File(stream, "application/pdf", FileName);
        }
        private Stream CreateSchedulePdf(long ChildId)
        {
            //Access db data



            var query = from child in _db.Childs
                        join doctor in _db.Doctors on child.DoctorId equals doctor.Id
                        join clinic in _db.Clinics on child.ClinicId equals clinic.Id

                        where child.Id == ChildId
                        select new
                        {
                            ChildId = child.Id,
                            ChildName = child.Name,
                            ChildGuardianName = child.GuardianName,
                            ChildMobileNumber = child.MobileNumber,
                            ChildCnicOrPassport = child.CnicOrPassPort,
                            ChildSelectCnicOrPassport = child.SelectCnicOrPassport,
                            Gender = child.Gender,
                            DoctorId = doctor.Id,
                            DoctorName = doctor.Name,
                            DoctorEmail = doctor.Email,
                            DoctorMobileNumber = doctor.MobileNumber,
                            ClinicName = clinic.Name,
                            ClinicAddress = clinic.Address,
                            ClinicNumber = clinic.Number,
                            ClinicCity = clinic.City,

                        };
            var result = query.FirstOrDefault();
            var query2 = from schedule in _db.PatientSchedules
                         join dose in _db.Doses on schedule.DoseId equals dose.Id
                         join vaccine in _db.Vaccines on dose.VaccineId equals vaccine.Id
                         join brand in _db.Brands on schedule.BrandId equals brand.Id into brandGroup
                         from brand in brandGroup.DefaultIfEmpty() // Perform left outer join
                         where schedule.ChildId == ChildId && vaccine.Infinite == false && schedule.IsSpecial==true
                         select new
                         {
                             schedule.Id,
                             schedule.ChildId,
                             Vaccine = vaccine.Name,
                             DoseName = dose.Name,
                             schedule.Date,
                             schedule.GivenDate,
                             schedule.IsSkip,
                             schedule.IsDone,
                             schedule.Height,
                             schedule.Weight,
                             schedule.OFC,
                             
                             BrandName = (schedule.BrandId == null) ? null : brand.Name
                         };
            var result2 = query2.OrderBy(item => item.Date).ToList();
            var query3 = from schedule2 in _db.PatientSchedules
                         join dose in _db.Doses on schedule2.DoseId equals dose.Id
                         join vaccine in _db.Vaccines on dose.VaccineId equals vaccine.Id
                         join brand in _db.Brands on schedule2.BrandId equals brand.Id into brandGroup
                         from brand in brandGroup.DefaultIfEmpty() // Perform left outer join
                         where schedule2.ChildId == ChildId && vaccine.Infinite == true
                         select new
                         {
                             schedule2.Id,
                             schedule2.ChildId,
                             Vaccine = vaccine.Name,
                             DoseName = dose.Name,
                             schedule2.Date,
                             schedule2.GivenDate,
                             schedule2.IsSkip,
                             schedule2.IsDone,
                             schedule2.Height,
                             schedule2.Weight,
                             schedule2.OFC,
                             BrandName = (schedule2.BrandId == null) ? null : brand.Name
                         };
            var result3 = query3.OrderBy(item => item.Date).ToList();


            string doctorName = result.DoctorName;
            string DoctorEmail = result.DoctorEmail;
            string DoctorMobileNumber = result.DoctorMobileNumber;
            string ClinicName = result.ClinicName;
            string ClinicAddress = result.ClinicAddress;
            string ClinicNumber = result.ClinicNumber;
            string ClinicCity = result.ClinicCity;
            string ChildName = result.ChildName;
            string ChildCnicPassPort = result.ChildCnicOrPassport;
            string ChildSelectCnicOrPassport = result.ChildSelectCnicOrPassport;
            string ChildGuardianName = result.ChildGuardianName;
            string ChildMobileNumber = result.ChildMobileNumber;
            var document = new Document(PageSize.A4, 45, 45, 30, 30);
            {
                //new Document (PageSize.A4, 50, 50, 25, 105); {
                var output = new MemoryStream();
                var writer = PdfWriter.GetInstance(document, output);
                writer.CloseStream = false;
                PDFFooter footer = new PDFFooter("NOTE: 1. Vaccines may cause fever, localised redness and pain. 2. This schedule is valid to produce on demand at all airports, embassies and schools of the world. 3. We always use the best available vaccine brand/manufacturer. With time and continuous research vaccine brand can be different for future doses. Disclaimer: This schedule provides recommended dates for immunisations for individual based date of birth, past history of immunisation and disease. Your consultant may update the due dates or add/remove vaccines. Vaccine.pk, its management or staff holds no responsibility for any loss or damage due to any vaccine given. *OHF = vaccine given at other health faculty (not by vaccine.pk) ");
                writer.PageEvent = footer;
                FontFactory.RegisterDirectories();

                document.Open();
                PdfPTable table2 = new PdfPTable(1);
                table2.WidthPercentage = 100;
               
                PdfPCell DoctorNamecell = new PdfPCell(new Phrase(doctorName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13)));
                DoctorNamecell.Border = Rectangle.NO_BORDER;
                DoctorNamecell.HorizontalAlignment = Element.ALIGN_LEFT;
                table2.AddCell(DoctorNamecell);
                PdfPCell doctorEmailCell = new PdfPCell(new Phrase(DoctorEmail, FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                doctorEmailCell.Border = Rectangle.NO_BORDER;
                doctorEmailCell.HorizontalAlignment = Element.ALIGN_LEFT;
                table2.AddCell(doctorEmailCell);
                PdfPCell doctorMobileNumberCell = new PdfPCell(new Phrase(DoctorMobileNumber, FontFactory.GetFont(FontFactory.HELVETICA, 12)));
                doctorMobileNumberCell.Border = Rectangle.NO_BORDER;
                doctorMobileNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
                table2.AddCell(doctorMobileNumberCell);
                PdfPCell lineBreakCell = new PdfPCell(new Phrase("\n"));
                lineBreakCell.Border = Rectangle.NO_BORDER;
                table2.AddCell(lineBreakCell);
                string imagePath = "./Resource/cliniclogo.png";
                Image logoImage = Image.GetInstance(imagePath);
                // Set the position and size of the image
                float imageWidth = 160f; // Adjust the width of the image as needed
                float imageHeight = 50f; // Adjust the height of the image as needed
                float imageX = document.PageSize.Width - document.RightMargin - imageWidth;
                float imageY = document.PageSize.Height - document.TopMargin - imageHeight;
                // Add the image to the PDF document
                logoImage.SetAbsolutePosition(imageX, imageY);
                logoImage.ScaleToFit(imageWidth, imageHeight);
                writer.DirectContent.AddImage(logoImage);
                PdfPTable mainTable = new PdfPTable(2);
                mainTable.WidthPercentage = 100;
                PdfPTable clinicTable = new PdfPTable(1);
                clinicTable.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell ClinicNameCell = new PdfPCell(new Phrase(ClinicName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                ClinicNameCell.Border = Rectangle.NO_BORDER;
                ClinicNameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                clinicTable.AddCell(ClinicNameCell);

                PdfPCell ClinicAddressCell = new PdfPCell(new Phrase(ClinicAddress, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
                ClinicAddressCell.Border = Rectangle.NO_BORDER;
                ClinicAddressCell.HorizontalAlignment = Element.ALIGN_LEFT;
                clinicTable.AddCell(ClinicAddressCell);
                PdfPCell ClinicNumberCell = new PdfPCell(new Phrase("Phone: " + ClinicNumber, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
                ClinicNumberCell.Border = Rectangle.NO_BORDER;
                ClinicNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
                clinicTable.AddCell(ClinicNumberCell);
                PdfPCell ClinicCityCell = new PdfPCell(new Phrase("City: " + ClinicCity, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
                ClinicCityCell.Border = Rectangle.NO_BORDER;
                ClinicCityCell.HorizontalAlignment = Element.ALIGN_LEFT;
                clinicTable.AddCell(ClinicCityCell);

                PdfPCell clinicCell = new PdfPCell(clinicTable);
                clinicCell.Border = Rectangle.NO_BORDER;
                mainTable.AddCell(clinicCell);

                PdfPTable childTable = new PdfPTable(1);
                childTable.DefaultCell.Border = Rectangle.NO_BORDER;
                
                PdfPCell childNameCell = new PdfPCell(new Phrase(ChildName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                childNameCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                childNameCell.Border = Rectangle.NO_BORDER;
                childTable.AddCell(childNameCell);
                string genderText = result.Gender == Gender.Boy ? "S/O" : "D/O";
                string childWithGuardianName;
               
                
                childWithGuardianName = "S/D/W/O" + " " + ChildGuardianName;
                
                PdfPCell ChildGuardianNameCell = new PdfPCell(new Phrase(childWithGuardianName, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
                ChildGuardianNameCell.Border = Rectangle.NO_BORDER;
                ChildGuardianNameCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                childTable.AddCell(ChildGuardianNameCell);

                PdfPCell ChildMobileNumberCell = new PdfPCell(new Phrase("+92-" + ChildMobileNumber, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
                ChildMobileNumberCell.Border = Rectangle.NO_BORDER;
                ChildMobileNumberCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                childTable.AddCell(ChildMobileNumberCell);
                if (ChildCnicPassPort != null)
                {
                    PdfPCell ChildCnicPassportCell = new PdfPCell(new Phrase(ChildSelectCnicOrPassport + " " + ChildCnicPassPort, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
                    ChildCnicPassportCell.Border = Rectangle.NO_BORDER;
                    ChildCnicPassportCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    childTable.AddCell(ChildCnicPassportCell);
                }
                PdfPCell childCell = new PdfPCell(childTable);
                childCell.Border = Rectangle.NO_BORDER;
                mainTable.AddCell(childCell);
                document.Add(table2);
                document.Add(mainTable);

                Paragraph title = new Paragraph("IMMUNIZATION RECORD");
                title.Font =
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                float[] widths = new float[] { 20f, 145f, 50f, 70, 70f, 60f, 60f, 60f };

                PdfPTable table = new PdfPTable(8);
                table.HorizontalAlignment = 0;
                table.TotalWidth = 510f;
                table.LockedWidth = true;
                table.SpacingBefore = 5;
                table.SetWidths(widths);
                table.AddCell(CreateCell("Sr", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Vaccine", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Status", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Date", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Brand", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Height", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("Weight", "backgroudLightGray", 1, "center", "scheduleRecords"));
                table.AddCell(CreateCell("OFC/BMI", "backgroudLightGray", 1, "center", "scheduleRecords"));
                
                


                int counter = 1;
                foreach (var schedule in result2)
                {
                    Font font = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    Font rangevaluefont = FontFactory.GetFont(FontFactory.HELVETICA, 8);
                    
                    Font rangefont = FontFactory.GetFont(FontFactory.HELVETICA, 6);
                    
                    Font boldfont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
                    Font italicfont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.ITALIC);
                    PdfPCell ageCell = new PdfPCell(new Phrase(counter.ToString(), font));
                    ageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    ageCell.FixedHeight = 15f;
                    ageCell.BorderColor = GrayColor.LIGHT_GRAY;
                    table.AddCell(ageCell);
                    PdfPCell dosenameCell = new PdfPCell(new Phrase(schedule.DoseName, font));
                    dosenameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    dosenameCell.BorderColor = GrayColor.LIGHT_GRAY;
                    table.AddCell(dosenameCell);

                    if (schedule.IsDone == true)
                    {
                        // Status is "Given" and formatted in bold
                        PdfPCell statusCell =
                                    new PdfPCell(new Phrase("Given", boldfont));
                        statusCell.HorizontalAlignment =
                            Element.ALIGN_LEFT;
                        statusCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(statusCell);
                    }
                    else if (schedule.IsSkip == true)
                    {
                        PdfPCell statusCell =
                                    new PdfPCell(new Phrase(" Missed",
                                            italicfont));
                        statusCell.HorizontalAlignment =
                            Element.ALIGN_RIGHT;
                        statusCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(statusCell);
                    }
                    else
                    {
                        PdfPCell statusCell =
                                    new PdfPCell(new Phrase("Due", font));
                        statusCell.HorizontalAlignment =
                            Element.ALIGN_LEFT;
                        statusCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(statusCell);
                    }
                    PdfPCell dateCell =
                                    new PdfPCell(new Phrase(schedule
                                                .Date
                                                .ToString("dd/MM/yyyy"),
                                            font));
                    dateCell.HorizontalAlignment =
                        Element.ALIGN_CENTER;
                    dateCell.BorderColor = GrayColor.LIGHT_GRAY;
                    table.AddCell(dateCell);


                    if (schedule.IsDone == true)
                    {
                        // Status is "Given" and formatted in bold
                        PdfPCell brandCell =
                                   new PdfPCell(new Phrase(schedule.BrandName, font));
                        brandCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        brandCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(brandCell);
                    }
                    else
                    {
                        // Status is empty or any other value
                        PdfPCell brandCell =
                                   new PdfPCell(new Phrase("", font));
                        brandCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        brandCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(brandCell);
                    }
                    if (schedule.Height == null)
                    {
                        // Status is "Given" and formatted in bold
                        PdfPCell heightCell =
                                   new PdfPCell(new Phrase("", font));
                        heightCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        heightCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(heightCell);
                    }
                    else
                    {
                        // Status is empty or any other value
                        PdfPCell heightCell =
                                   new PdfPCell(new Phrase(schedule.Height.ToString(), font));
                        heightCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        heightCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(heightCell);
                    }
                    if (schedule.Weight == null)
                    {
                        // Status is "Given" and formatted in bold
                        PdfPCell WeightCell =
                                   new PdfPCell(new Phrase("", font));
                        WeightCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        WeightCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(WeightCell);
                    }
                    else
                    {
                        // Status is empty or any other value
                        PdfPCell WeightCell =
                                   new PdfPCell(new Phrase(schedule.Weight.ToString(), font));
                        WeightCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        WeightCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(WeightCell);
                    }
                    if (schedule.OFC == null)
                    {
                        // Status is "Given" and formatted in bold
                        PdfPCell OFCCell =
                                   new PdfPCell(new Phrase("", font));
                        OFCCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        OFCCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(OFCCell);
                    }
                    else
                    {
                        // Status is empty or any other value
                        PdfPCell OFCCell =
                                   new PdfPCell(new Phrase(schedule.OFC.ToString(), font));
                        OFCCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        OFCCell.BorderColor = GrayColor.LIGHT_GRAY;
                        table.AddCell(OFCCell);
                    }





                    counter++;


                }
                
               

                document.Add(table);

                
                // special vaccines table start
                float[] lowerwidths = new float[] { 255f, 255f };
                PdfPTable lowertable = new PdfPTable(2);
                lowertable.HorizontalAlignment = 0;
                lowertable.TotalWidth = 510f;
                lowertable.LockedWidth = true;
                lowertable.SpacingBefore = 10;
                lowertable.SetWidths(lowerwidths);
                lowertable.AddCell(CreateCell("Typhoid (Every 2-3 years)","bold",1,"center","scheduleRecords"));
                lowertable.AddCell(CreateCell("Flu (Yearly)","bold",1,"center","scheduleRecords"));
                document.Add(lowertable);

                float[] lowerwidths2 =
                    new float[] { 85f, 85f, 85f };
                PdfPTable lowertable2 = new PdfPTable(3);
                lowertable2.HorizontalAlignment = 0;
                lowertable2.TotalWidth = 255f;
                lowertable2.LockedWidth = true;
                lowertable2.SetWidths(lowerwidths2);
                lowertable2.DefaultCell.Border = PdfPCell.NO_BORDER;

                //header
                lowertable2.AddCell(CreateCell("Brand","backgroudLightGray",1,"center","scheduleRecords"));
                lowertable2.AddCell(CreateCell("Given On ","backgroudLightGray",1,"center","scheduleRecords"));
                lowertable2.AddCell(CreateCell("Next Due","backgroudLightGray",1,"center","scheduleRecords"));

                float[] lowerwidths3 = new float[] { 85f, 85f, 85f };
                PdfPTable lowertable3 = new PdfPTable(3);
                lowertable3.HorizontalAlignment = 0;
                lowertable3.TotalWidth = 255f;  // Adjust the width as needed
                lowertable3.LockedWidth = true;
                lowertable3.SetWidths(lowerwidths3);
                lowertable3.DefaultCell.Border = PdfPCell.NO_BORDER;

                // Header for the second table
                lowertable3.AddCell(CreateCell("Brand", "backgroudLightGray", 1, "center", "scheduleRecords"));
                lowertable3.AddCell(CreateCell("Given On ", "backgroudLightGray", 1, "center", "scheduleRecords"));
                lowertable3.AddCell(CreateCell("Next Due", "backgroudLightGray", 1, "center", "scheduleRecords"));

                int counter3 = 1;
                
                foreach (var schedule2 in result3)
                {
                    if (schedule2.Vaccine == "Typhoid" && schedule2.IsDone==false && schedule2.IsSkip==false)
                    {
                        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                        PdfPCell TyphoidBrand = new PdfPCell(new Phrase("", font3));
                        lowertable2.AddCell(TyphoidBrand);

                        PdfPCell TyphoidGiven = new PdfPCell(new Phrase("", font3));
                        lowertable2.AddCell(TyphoidGiven);
                        
                        PdfPCell TyphoidDate = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                        lowertable2.AddCell(TyphoidDate);
                        


                    }
                    else if (schedule2.Vaccine == "Typhoid" && schedule2.IsDone == true)
                    {
                        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                        PdfPCell TyphoidBrand = new PdfPCell(new Phrase(schedule2.BrandName, font3));
                        lowertable2.AddCell(TyphoidBrand);

                        PdfPCell TyphoidGiven;

                        if (schedule2.GivenDate != null)
                        {
                            TyphoidGiven = new PdfPCell(new Phrase(schedule2.GivenDate.Value.ToString("dd/MM/yyyy"), font3));
                        }
                        else
                        {
                            // Handle the case where GivenDate is null
                            TyphoidGiven = new PdfPCell(new Phrase("N/A", font3)); // or any other default value or placeholder
                        }
                        lowertable2.AddCell(TyphoidGiven);

                        PdfPCell TyphoidDate = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                        lowertable2.AddCell(TyphoidDate);



                    }
                    else if (schedule2.Vaccine == "Typhoid" && schedule2.IsSkip== true)
                    {
                        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                        PdfPCell TyphoidBrand = new PdfPCell(new Phrase(schedule2.BrandName, font3));
                        lowertable2.AddCell(TyphoidBrand);

                        PdfPCell TyphoidGiven;

                        if (schedule2.GivenDate != null)
                        {
                            TyphoidGiven = new PdfPCell(new Phrase(schedule2.GivenDate.Value.ToString("dd/MM/yyyy"), font3));
                        }
                        else
                        {
                            // Handle the case where GivenDate is null
                            TyphoidGiven = new PdfPCell(new Phrase("Skip", font3)); // or any other default value or placeholder
                        }
                        lowertable2.AddCell(TyphoidGiven);

                        PdfPCell TyphoidDate = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                        lowertable2.AddCell(TyphoidDate);



                    }
                    else if (schedule2.Vaccine == "Flu" && schedule2.IsDone == false && schedule2.IsSkip == false)
                    {
                        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                        PdfPCell FluBrand = new PdfPCell(new Phrase("", font3));
                        lowertable3.AddCell(FluBrand);

                        PdfPCell FluGiven = new PdfPCell(new Phrase("", font3));
                        lowertable3.AddCell(FluGiven);

                        PdfPCell DateFlu = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                        lowertable3.AddCell(DateFlu);



                    }
                  
                    else if (schedule2.Vaccine == "Flu" && schedule2.IsDone == true )
                    {
                        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                        PdfPCell FluBrand = new PdfPCell(new Phrase(schedule2.BrandName, font3));
                        lowertable3.AddCell(FluBrand);

                        
                        PdfPCell FluGiven;

                        if (schedule2.GivenDate != null)
                        {
                            FluGiven = new PdfPCell(new Phrase(schedule2.GivenDate.Value.ToString("dd/MM/yyyy"), font3));
                        }
                        else
                        {
                            // Handle the case where GivenDate is null
                            FluGiven = new PdfPCell(new Phrase("N/A", font3)); // or any other default value or placeholder
                        }
                        lowertable3.AddCell(FluGiven);

                        PdfPCell DateFlu = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                        lowertable3.AddCell(DateFlu);



                    }
                    else if (schedule2.Vaccine == "Flu" && schedule2.IsSkip == true)
                    {
                        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                        PdfPCell FluBrand = new PdfPCell(new Phrase(schedule2.BrandName, font3));
                        lowertable3.AddCell(FluBrand);


                        PdfPCell FluGiven;

                        if (schedule2.GivenDate != null)
                        {
                            FluGiven = new PdfPCell(new Phrase(schedule2.GivenDate.Value.ToString("dd/MM/yyyy"), font3));
                        }
                        else
                        {
                            // Handle the case where GivenDate is null
                            FluGiven = new PdfPCell(new Phrase("Skip", font3)); // or any other default value or placeholder
                        }
                        lowertable3.AddCell(FluGiven);

                        PdfPCell DateFlu = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                        lowertable3.AddCell(DateFlu);



                    }
                 
                    else
                    {

                        continue;




                    }
                counter3++;
                    

                }
                PdfPTable containerTable = new PdfPTable(2);
                containerTable.TotalWidth = 510f;  // Adjust the width as needed
                containerTable.LockedWidth = true;
                containerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

                // Add both tables to the container table
                containerTable.AddCell(lowertable2);
                containerTable.AddCell(lowertable3);

                // Add the container table to the document
                document.Add(containerTable);

                //float[] lowerwidths5 = new float[] {255f };
                //PdfPTable lowertable5 = new PdfPTable(1);
                //lowertable5.HorizontalAlignment = 0;
                //lowertable5.TotalWidth = 250f;
                //lowertable5.LockedWidth = true;
                //lowertable5.SpacingBefore = 10;
                //lowertable5.SetWidths(lowerwidths5);
                //lowertable5.AddCell(CreateCell("Tetnus(Every 2-3 years)", "bold", 1, "center", "scheduleRecords"));
            
                //document.Add(lowertable5);

                //float[] lowerwidths6 =
                //    new float[] { 85f, 85f, 85f };
                //PdfPTable lowertable6 = new PdfPTable(3);
                //lowertable6.HorizontalAlignment = 0;
                //lowertable6.TotalWidth = 255f;
                //lowertable6.LockedWidth = true;
                //lowertable6.SetWidths(lowerwidths6);
                //lowertable6.DefaultCell.Border = PdfPCell.NO_BORDER;

                ////header
                //lowertable6.AddCell(CreateCell("Brand", "backgroudLightGray", 1, "center", "scheduleRecords"));
                //lowertable6.AddCell(CreateCell("Given On ", "backgroudLightGray", 1, "center", "scheduleRecords"));
                //lowertable6.AddCell(CreateCell("Next Due", "backgroudLightGray", 1, "center", "scheduleRecords"));

                
               
                //int counter4 = 1;

                //foreach (var schedule2 in result3)
                //{
                   
                    
                //     if (schedule2.Vaccine == "Tetanus" && schedule2.IsDone == false && schedule2.IsSkip == false)
                //    {
                //        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                //        PdfPCell TetanusBrand = new PdfPCell(new Phrase("", font3));
                //        lowertable6.AddCell(TetanusBrand);

                //        PdfPCell TetanusGiven = new PdfPCell(new Phrase("", font3));
                //        lowertable6.AddCell(TetanusGiven);

                //        PdfPCell TetanusDate = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                //        lowertable6.AddCell(TetanusDate);



                //    }
                //    else if (schedule2.Vaccine == "Tetanus" && schedule2.IsDone == true)
                //    {
                //        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                //        PdfPCell TetanusBrand = new PdfPCell(new Phrase(schedule2.BrandName, font3));
                //        lowertable6.AddCell(TetanusBrand);

                //        PdfPCell TetanusGiven;

                //        if (schedule2.GivenDate != null)
                //        {
                //            TetanusGiven = new PdfPCell(new Phrase(schedule2.GivenDate.Value.ToString("dd/MM/yyyy"), font3));
                //        }
                //        else
                //        {
                //            // Handle the case where GivenDate is null
                //            TetanusGiven = new PdfPCell(new Phrase("N/A", font3)); // or any other default value or placeholder
                //        }
                //        lowertable6.AddCell(TetanusGiven);

                //        PdfPCell TetanusDate = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                //        lowertable6.AddCell(TetanusDate);



                //    }
                //    else if (schedule2.Vaccine == "Tetanus" && schedule2.IsSkip == true)
                //    {
                //        Font font3 = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                //        PdfPCell TetanusBrand = new PdfPCell(new Phrase(schedule2.BrandName, font3));
                //        lowertable6.AddCell(TetanusBrand);

                //        PdfPCell TetanusGiven;

                //        if (schedule2.GivenDate != null)
                //        {
                //            TetanusGiven = new PdfPCell(new Phrase(schedule2.GivenDate.Value.ToString("dd/MM/yyyy"), font3));
                //        }
                //        else
                //        {
                //            // Handle the case where GivenDate is null
                //            TetanusGiven = new PdfPCell(new Phrase("Skip", font3)); // or any other default value or placeholder
                //        }
                //        lowertable6.AddCell(TetanusGiven);

                //        PdfPCell TetanusDate = new PdfPCell(new Phrase(schedule2.Date.ToString("dd/MM/yyyy"), font3));
                //        lowertable6.AddCell(TetanusDate);



                //    }
                //    else
                //    {

                //        continue;




                //    }
                  /*  counter4++*/;


                //}

                //document.Add(lowertable6);
                //special vaccines table end
                document.Close();
                output.Seek(0, SeekOrigin.Begin);
                return output;
            }
        }
        public class PDFFooter : PdfPageEventHelper
        {
            private string footerText;

            public PDFFooter(string footerText)
            {
                this.footerText = footerText;
            }


            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                string footer = footerText + " Printed on: " + DateTime.UtcNow.AddHours(5).ToString("MMMM dd, yyyy");

                footer =
                    footer
                        .Replace(Environment.NewLine, String.Empty)
                        .Replace("  ", String.Empty);
                Font georgia = FontFactory.GetFont("georgia", 6f);

                PdfPCell cell = new PdfPCell(new Phrase(footer, georgia));
                cell.Border = 0;


                PdfPTable tabFot = new PdfPTable(1);
                tabFot.SetTotalWidth(new float[] { 510f });
                tabFot.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                tabFot.AddCell(cell);

                float bottomMarginWithSpace = document.BottomMargin + 20; // Add 20 units of extra space
                tabFot.WriteSelectedRows(0, -1, document.LeftMargin, bottomMarginWithSpace, writer.DirectContent);


            }
        }
            protected PdfPCell CreateCell(string value,
            string color,
            int colpan,
            string alignment,
            string table)

        {
            Font font = FontFactory.GetFont(FontFactory.HELVETICA, 11);
            if (color == "bold" || color == "backgroudLightGray")
            {
                font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
                font.Size = 11;
            }
            if (table == "inwordamount")
            {
                font =
                    FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.ITALIC);
            }
            if (color == "unbold")
            {
                font = FontFactory.GetFont(FontFactory.HELVETICA, 11);
            }
            if (color == "sitetitle")
            {
                font = FontFactory.GetFont(FontFactory.HELVETICA, 16);
            }
            PdfPCell cell = new PdfPCell(new Phrase(value, font));
            cell.BorderColor = GrayColor.LIGHT_GRAY;
            if (color == "backgroudLightGray")
            {
                cell.BackgroundColor = new BaseColor(224, 218, 218);
                //  cell.BackgroundColor = GrayColor.LightGray;
                cell.FixedHeight = 20f;
            }
            if (alignment == "right")
            {
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            }
            if (alignment == "left")
            {
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
            }
            if (alignment == "center")
            {
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            }
            cell.Colspan = colpan;
            if (table == "description")
            {
                cell.Border = 0;
                cell.Padding = 2f;
            }
            if (table == "scheduleRecords")
            {
                cell.FixedHeight = 15f;
            }
            if (table == "invoiceRecords" || table == "inwordamount")
            {
                cell.FixedHeight = 18f;
            }
            return cell;
        }

        //[Route("pdf")]
        //[EnableCors("corsapp")]
        //[HttpGet]
        //public IActionResult GetJoinedData(long ChildId)
        //{
        //    var query = from child in _db.Childs
        //                join doctor in _db.Doctors on child.DoctorId equals doctor.Id
        //                join clinic in _db.Clinics on child.ClinicId equals clinic.Id


        //                where child.Id == ChildId
        //                select new
        //                {
        //                    ChildId = child.Id,
        //                    ChildName = child.Name,
        //                    ChildGuardian= child.Guardian,
        //                    ChildGuardianName=child.GuardianName,
        //                    ChildMobileNumber=child.MobileNumber,
        //                    ChildCnicOrPassport=child.CnicOrPassPort,
        //                    ChildSelectCnicOrPassport=child.SelectCnicOrPassport,
        //                    Gender=child.Gender,
        //                    DoctorId = doctor.Id,
        //                    DoctorName = doctor.Name,
        //                    DoctorEmail = doctor.Email,
        //                    DoctorMobileNumber = doctor.MobileNumber,
        //                    ClinicName = clinic.Name,
        //                    ClinicAddress=clinic.Address,
        //                    ClinicNumber=clinic.Number,
        //                    ClinicCity=clinic.City,

        //                };
        //    var result = query.FirstOrDefault();
        //    var query2 = from schedule in _db.PatientSchedules
        //            join dose in _db.Doses on schedule.DoseId equals dose.Id
        //            join vaccine in _db.Vaccines on dose.VaccineId equals vaccine.Id
        //            join brand in _db.Brands on schedule.BrandId equals brand.Id into brandGroup
        //                 from brand in brandGroup.DefaultIfEmpty() // Perform left outer join

        //                 where schedule.ChildId == ChildId && schedule.IsSpecial2==false
        //            select new
        //            {
        //                schedule.Id,
        //                schedule.ChildId,
        //                Vaccine=vaccine.Name,
        //                DoseName = dose.Name,
        //                schedule.Date,
        //                schedule.IsSkip,
        //                schedule.IsDone,
        //                BrandName = (schedule.BrandId == null) ? null : brand.Name

        //            };

        //    var result2 = query2.OrderBy(item => item.Date).ToList();

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }


        //    string doctorName =result.DoctorName;
        //    string DoctorEmail = result.DoctorEmail;
        //    string DoctorMobileNumber = result.DoctorMobileNumber;
        //    string ClinicName = result.ClinicName;
        //    string ClinicAddress = result.ClinicAddress;
        //    string ClinicNumber = result.ClinicNumber;
        //    string ClinicCity = result.ClinicCity;
        //    string ChildName = result.ChildName;
        //    string ChildCnicPassPort=result.ChildCnicOrPassport;
        //    string ChildSelectCnicOrPassport=result.ChildSelectCnicOrPassport;
        //    string ChildGuardian = result.ChildGuardian;
        //    string ChildGuardianName = result.ChildGuardianName;
        //    string ChildMobileNumber=result.ChildMobileNumber;

        //    // Create the PDF document
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        Document document = new Document(PageSize.A4, 45, 45, 30, 30);
        //        var writer = PdfWriter.GetInstance(document, memoryStream);
        //        writer.CloseStream = false;
        //        PDFFooter footer = new PDFFooter("NOTE: 1. Vaccines may cause fever, localised redness and pain. 2. This schedule is valid to produce on demand at all airports, embassies and schools of the world. 3. We always use the best available vaccine brand/manufacturer. With time and continuous research vaccine brand can be different for future doses. Disclaimer: This schedule provides recommended dates for immunisations for individual based date of birth, past history of immunisation and disease. Your consultant may update the due dates or add/remove vaccines. Vaccine.pk, its management or staff holds no responsibility for any loss or damage due to any vaccine given. *OHF = vaccine given at other health faculty (not by vaccine.pk) ");
        //        writer.PageEvent = footer;
        //        FontFactory.RegisterDirectories();
        //        document.Open();
        //        PdfPTable table = new PdfPTable(1);
        //        table.WidthPercentage = 100;

        //        PdfPCell DoctorNamecell = new PdfPCell(new Phrase(doctorName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13)));
        //        DoctorNamecell.Border = Rectangle.NO_BORDER;
        //        DoctorNamecell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        table.AddCell(DoctorNamecell);


        //        PdfPCell doctorEmailCell = new PdfPCell(new Phrase(DoctorEmail, FontFactory.GetFont(FontFactory.HELVETICA, 12)));
        //        doctorEmailCell.Border = Rectangle.NO_BORDER;
        //        doctorEmailCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        table.AddCell(doctorEmailCell);

        //        PdfPCell doctorMobileNumberCell = new PdfPCell(new Phrase(DoctorMobileNumber, FontFactory.GetFont(FontFactory.HELVETICA, 12)));
        //        doctorMobileNumberCell.Border = Rectangle.NO_BORDER;
        //        doctorMobileNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        table.AddCell(doctorMobileNumberCell);

        //        PdfPCell lineBreakCell = new PdfPCell(new Phrase("\n"));
        //        lineBreakCell.Border = Rectangle.NO_BORDER;
        //        table.AddCell(lineBreakCell);


        //        string imagePath = "./Resource/cliniclogo.png";
        //        Image logoImage = Image.GetInstance(imagePath);

        //        // Set the position and size of the image
        //        float imageWidth = 160f; // Adjust the width of the image as needed
        //        float imageHeight = 50f; // Adjust the height of the image as needed
        //        float imageX = document.PageSize.Width - document.RightMargin - imageWidth;
        //        float imageY = document.PageSize.Height - document.TopMargin - imageHeight;

        //        // Add the image to the PDF document
        //        logoImage.SetAbsolutePosition(imageX, imageY);
        //        logoImage.ScaleToFit(imageWidth, imageHeight);
        //        writer.DirectContent.AddImage(logoImage);


        //        PdfPTable mainTable = new PdfPTable(2);
        //        mainTable.WidthPercentage = 100;

        //        PdfPTable clinicTable = new PdfPTable(1);
        //        clinicTable.DefaultCell.Border = Rectangle.NO_BORDER;

        //        PdfPCell ClinicNameCell = new PdfPCell(new Phrase(ClinicName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
        //        ClinicNameCell.Border = Rectangle.NO_BORDER;
        //        ClinicNameCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        clinicTable.AddCell(ClinicNameCell);


        //        PdfPCell ClinicAddressCell = new PdfPCell(new Phrase(ClinicAddress, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
        //        ClinicAddressCell.Border = Rectangle.NO_BORDER;
        //        ClinicAddressCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        clinicTable.AddCell(ClinicAddressCell);


        //        PdfPCell ClinicNumberCell = new PdfPCell(new Phrase("Phone: " +ClinicNumber, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
        //        ClinicNumberCell.Border = Rectangle.NO_BORDER;
        //        ClinicNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        clinicTable.AddCell(ClinicNumberCell);

        //        PdfPCell ClinicCityCell = new PdfPCell(new Phrase("City: " +ClinicCity, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
        //        ClinicCityCell.Border = Rectangle.NO_BORDER;
        //        ClinicCityCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        clinicTable.AddCell(ClinicCityCell);


        //        PdfPCell clinicCell = new PdfPCell(clinicTable);
        //        clinicCell.Border = Rectangle.NO_BORDER;
        //        mainTable.AddCell(clinicCell);




        //        PdfPTable childTable = new PdfPTable(1);
        //        childTable.DefaultCell.Border = Rectangle.NO_BORDER;

        //        PdfPCell gapCell = new PdfPCell();
        //        gapCell.FixedHeight = 10f; // Adjust the height of the gap as needed
        //        gapCell.Border = Rectangle.NO_BORDER;
        //        childTable.AddCell(gapCell);

        //        PdfPCell childNameCell = new PdfPCell(new Phrase(ChildName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
        //        childNameCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //        childNameCell.Border = Rectangle.NO_BORDER;
        //        childTable.AddCell(childNameCell);


        //        //string genderText = result.Gender == Gender.Boy ? "s/o " : "d/o ";
        //        //string childWithGuardianName = genderText + " " + (ChildGuardian == "Husband" ? "w/o":"") + " " + ChildGuardianName;
        //        string genderText = result.Gender == Gender.Boy ? "S/O" : "D/O";

        //        string childWithGuardianName;

        //        if (ChildGuardian == "Father" || ChildGuardian == "Mother")
        //        {
        //            childWithGuardianName = genderText + " " + ChildGuardianName;
        //        }
        //        else
        //        {
        //            childWithGuardianName = "W/O" + " " + ChildGuardianName;
        //        }
        //        PdfPCell ChildGuardianNameCell = new PdfPCell(new Phrase(childWithGuardianName, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
        //        ChildGuardianNameCell.Border = Rectangle.NO_BORDER;
        //        ChildGuardianNameCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //        childTable.AddCell(ChildGuardianNameCell);




        //        PdfPCell ChildMobileNumberCell = new PdfPCell(new Phrase("+92-"+ChildMobileNumber, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
        //        ChildMobileNumberCell.Border = Rectangle.NO_BORDER;
        //        ChildMobileNumberCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //        childTable.AddCell(ChildMobileNumberCell);

        //        if(ChildCnicPassPort!=null)
        //        {
        //            PdfPCell ChildCnicPassportCell = new PdfPCell(new Phrase(ChildSelectCnicOrPassport+" "+ChildCnicPassPort, FontFactory.GetFont(FontFactory.HELVETICA, 11)));
        //            ChildCnicPassportCell.Border = Rectangle.NO_BORDER;
        //            ChildCnicPassportCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //            childTable.AddCell(ChildCnicPassportCell);

        //        }

        //        PdfPCell childCell = new PdfPCell(childTable);
        //        childCell.Border = Rectangle.NO_BORDER;
        //        mainTable.AddCell(childCell);



        //        document.Add(table);
        //        document.Add(mainTable);

        //        Paragraph title = new Paragraph("IMMUNIZATION RECORD");
        //        title.Font =
        //        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        //        title.Alignment = Element.ALIGN_CENTER;
        //        document.Add(title);

        //        float[] widths = new float[] { 20f, 90f, 50f,90f, 70 };
        //        PdfPTable table_Center = new PdfPTable(5);
        //        // table_Center.HorizontalAlignment = Element.ALIGN_LEFT;
        //        table_Center.TotalWidth = 510f;
        //        table_Center.LockedWidth = true;
        //        table_Center.HorizontalAlignment = 0;
        //        table_Center.SpacingBefore = 5;
        //        table_Center.SetWidths(widths);

        //        // Add table headers
        //        table_Center.AddCell(new PdfPCell(new Phrase("Sr")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
        //        table_Center.AddCell(new PdfPCell(new Phrase("Vaccine")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
        //        table_Center.AddCell(new PdfPCell(new Phrase("Status")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
        //        table_Center.AddCell(new PdfPCell(new Phrase("Date")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
        //        table_Center.AddCell(new PdfPCell(new Phrase("Brand")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });


        //        int counter = 1;
        //        foreach (var schedule in result2)
        //        {
        //            table_Center.AddCell(new PdfPCell(new Phrase(counter.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10))) { HorizontalAlignment = Element.ALIGN_CENTER , MinimumHeight = 10f, PaddingBottom = 2f });
        //            table_Center.AddCell(new PdfPCell(new Phrase(schedule.DoseName, FontFactory.GetFont(FontFactory.HELVETICA, 10))) { HorizontalAlignment = Element.ALIGN_CENTER , MinimumHeight = 10f, PaddingBottom = 2f });
        //            PdfPCell statusCell;
        //            if (schedule.IsDone == true)
        //            {
        //                // Status is "Given" and formatted in bold
        //                statusCell = new PdfPCell(new Phrase("Given", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)));
        //            }
        //            else if (schedule.IsSkip==true)
        //            {
        //                // Status is empty or any other value
        //                statusCell = new PdfPCell(new Phrase("Missed", FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        //            }
        //            else
        //            {
        //                statusCell = new PdfPCell(new Phrase("Due", FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        //            }

        //            statusCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            table_Center.AddCell(statusCell);
        //            table_Center.AddCell(new PdfPCell(new Phrase(schedule.Date.ToString("d"), FontFactory.GetFont(FontFactory.HELVETICA, 10)))  { HorizontalAlignment = Element.ALIGN_CENTER, MinimumHeight = 7f, PaddingBottom = 2f });

        //            PdfPCell BrandCell;
        //            if (schedule.IsDone == true)
        //            {
        //            // Status is "Given" and formatted in bold
        //            BrandCell = new PdfPCell(new Phrase(schedule.BrandName, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        //            }
        //            else 
        //            {
        //            // Status is empty or any other value
        //            BrandCell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 10)));
        //            }

        //            BrandCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            table_Center.AddCell(BrandCell);




        //            counter++;
        //        }

        //        document.Add(table_Center);

        //        document.Close();


        //        // Set the response headers
        //        Response.Headers["Content-Type"] = "application/pdf";
        //        Response.Headers["Content-Disposition"] = "attachment; filename=joined_data.pdf";

        //        // Write the PDF file to the response
        //        return File(memoryStream.ToArray(), "application/pdf");
        //    }
        //}

        //public class PDFFooter : PdfPageEventHelper
        //{
        //    private string footerText;

        //    public PDFFooter(string footerText)
        //    {
        //        this.footerText = footerText;
        //    }


        //    // write on end of each page
        //    public override void OnEndPage(PdfWriter writer, Document document)
        //    {
        //        base.OnEndPage(writer, document);
        //        string footer = footerText + " Printed on: "  + DateTime.UtcNow.AddHours(5).ToString("MMMM dd, yyyy");

        //        footer =
        //            footer
        //                .Replace(Environment.NewLine, String.Empty)
        //                .Replace("  ", String.Empty);
        //        Font georgia = FontFactory.GetFont("georgia", 6f);

        //        PdfPCell cell = new PdfPCell(new Phrase(footer, georgia));
        //        cell.Border = 0;


        //        PdfPTable tabFot = new PdfPTable(1);
        //        tabFot.SetTotalWidth(new float[] { 510f });
        //        tabFot.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

        //        cell.PaddingTop = 5;

        //        tabFot.AddCell(cell);


        //        tabFot.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent); 
        //        // PdfContentByte canvas = writer.DirectContent;
        //        // Phrase footerPhrase = new Phrase(footer, georgia);

        //        // float x = document.LeftMargin;
        //        // float y = document.BottomMargin - 15; // Adjust the value as needed
        //        // float width = document.PageSize.Width - document.LeftMargin - document.RightMargin;
        //        // float height = 70; // Adjust the value as needed for the height of the text

        //        // Rectangle rect = new Rectangle(x, y, x + width, y + height);
        //        // ColumnText columnText = new ColumnText(canvas);
        //        // columnText.SetSimpleColumn(rect);
        //        // columnText.Alignment = Element.ALIGN_LEFT;
        //        // columnText.AddElement(footerPhrase);
        //        // columnText.UseAscender = true;
        //        // columnText.Go();
        //    }


    
    }
}
