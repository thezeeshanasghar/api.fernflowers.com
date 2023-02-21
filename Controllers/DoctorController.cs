using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using api.fernflowers.com.ModelDTO;
using System.Linq;
namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly VaccineDBContext _db;






        public DoctorController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;


        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var doctor = await _db.Doctors.ToListAsync();
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try
            {
                var doctor = await _db.Doctors.FindAsync(id);
                if (doctor == null)
                    return NotFound();
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }


        [Route("login")]
        [HttpGet()]

        public async Task<IActionResult> Login(int MobileNumber, string Password)
        {
            try
            {
                var doctor = _db.Doctors.FirstOrDefault(a => a.MobileNumber == MobileNumber && a.Password == Password && a.Isapproved == true);
                var clinic = _db.Clinics.FirstOrDefault(c => c.DoctorId == doctor.Id);

                var clinictiming = _db.Clinictimings.Where(ct => ct.ClinicId == clinic.Id).ToList();
                var doctor_schedule = _db.DoctorSchedules.Where(ct => ct.DoctorId == doctor.Id).ToList();

                DoctorDTO doctorDTO = null;

                if (doctor != null)
                {

                    doctorDTO = new DoctorDTO
                    {
                        Id = doctor.Id,
                        Name = doctor.Name,
                        Email = doctor.Email,
                        Isapproved = doctor.Isapproved,
                        IsEnabled = doctor.IsEnabled,
                        DoctorType = doctor.DoctorType,
                        MobileNumber = doctor.MobileNumber,
                        Password = doctor.Password,
                        PMDC = doctor.PMDC
                    };

                    if (clinic != null)
                    {
                        doctorDTO.Clinic = new ClinicDTO
                        {
                            Id = clinic.Id,
                            Address = clinic.Address,
                            Name = clinic.Name,
                            Number = clinic.Number,
                            DoctorId = clinic.DoctorId
                        };
                    }
                    if (clinictiming != null)
                    {
                        doctorDTO.Clinic.ClinicTiming = new List<ClinictimingDTO> { };
                        foreach (var ct in clinictiming)
                        {
                            var tmp_clinictiming = new ClinictimingDTO
                            {
                                Id = ct.Id,
                                Day = ct.Day,
                                Session = ct.Session,
                                StartTime = ct.StartTime,
                                EndTime = ct.EndTime,
                                ClinicId = ct.ClinicId
                            };
                            doctorDTO.Clinic.ClinicTiming.Add(tmp_clinictiming);
                        }
                    }

                    if (doctor_schedule != null)
                    {
                        doctorDTO.DoctorSchedule = new List<DoctorScheduleDTO> { };
                        foreach (var ds in doctor_schedule)
                        {
                            var tmp_doctor_schedule = new DoctorScheduleDTO
                            {
                                Id = ds.Id,
                                DoctorId = ds.DoctorId,
                                DoseId = ds.DoseId,

                            };
                            doctorDTO.DoctorSchedule.Add(tmp_doctor_schedule);
                        }
                    }


                }
                return Ok(doctorDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] DoctorDTO doctor)
        {
            if (doctor != null)
            {
                var doctorEntity = new Doctor
                {
                    Name = doctor.Name,
                    Email = doctor.Email,
                    Isapproved = doctor.Isapproved,
                    IsEnabled = doctor.IsEnabled,
                    DoctorType = doctor.DoctorType,
                    MobileNumber = doctor.MobileNumber,
                    Password = doctor.Password,
                    PMDC = doctor.PMDC
                };

                _db.Doctors.Add(doctorEntity);
                await _db.SaveChangesAsync();

                if (doctor.Clinic != null)
                {
                    var clinicEntity = new Clinic
                    {
                        Address = doctor.Clinic.Address,
                        Name = doctor.Clinic.Name,
                        Number = doctor.Clinic.Number
                    };
                    clinicEntity.DoctorId = doctorEntity.Id;
                    _db.Clinics.Add(clinicEntity);
                    await _db.SaveChangesAsync();


                    if (doctor.Clinic.ClinicTiming != null)
                    {
                        foreach (var ct in doctor.Clinic.ClinicTiming)
                        {
                            var entityClinicTiming = new Clinictiming
                            {
                                Day = ct.Day,
                                Session = ct.Session,
                                StartTime = ct.StartTime,
                                EndTime = ct.EndTime,
                                ClinicId = clinicEntity.Id
                            };
                            _db.Clinictimings.Add(entityClinicTiming);
                        }
                        await _db.SaveChangesAsync();
                    }
                }

                if (doctor.DoctorSchedule != null)
                {
                    foreach (var schedule in doctor.DoctorSchedule)
                    {
                        var entityDoctorSchedule = new DoctorsSchedule
                        {
                            DoctorId = doctorEntity.Id,
                            DoseId = Convert.ToInt32(schedule.DoseId)

                        };
                        _db.DoctorSchedules.Add(entityDoctorSchedule);

                    }
                    await _db.SaveChangesAsync();
                }

                return Created(new Uri(Request.GetEncodedUrl() + "/" + doctorEntity.Id), doctorEntity.Id);
            }
            else
            {
                return BadRequest("Doctor data is required");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Doctor doctorToUpdate)
        {
            try
            {
                if (id != doctorToUpdate.Id)
                    return BadRequest();
                var dbDoctor = await _db.Doctors.FindAsync(id);
                if (dbDoctor == null)
                    return NotFound();

                _db.Doctors.Update(doctorToUpdate);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                var doctorToDelete = await _db.Doctors.FindAsync(id);
                if (doctorToDelete == null)
                {
                    return NotFound();
                }
                _db.Doctors.Remove(doctorToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] Doctor doc)
        {
            try
            {
                var dbDoc = await _db.Doctors.FindAsync(doc.Id);
                if (dbDoc == null)
                {
                    return NotFound();
                }
                dbDoc.Email = doc.Email;
                dbDoc.Name = doc.Name;
                dbDoc.Password = doc.Password;
                dbDoc.DoctorType = doc.DoctorType;
                dbDoc.MobileNumber = doc.MobileNumber;
                dbDoc.PMDC = doc.PMDC;
                _db.Entry(dbDoc).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
        [HttpPatch()]
        [Route("notapproved/{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id,[FromBody] JsonPatchDocument<Doctor> patchDocument)
        {
            try{
                var dbDoc = await _db.Doctors.FindAsync(id);
                if (dbDoc == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbDoc);
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }

        [HttpGet]
        [Route("approved/{approved:bool}")]
        public async Task<IActionResult> GetApprovedDoctors(bool approved)
        {
            try
            {
                var doctor = await _db.Doctors.Where(x => x.Isapproved == approved).ToListAsync();
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}