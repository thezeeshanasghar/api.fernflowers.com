using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using api.fernflowers.com.ModelDTO;
using System.Linq;
using System.Globalization;

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
        public async Task<IActionResult> GetSingle([FromRoute] long id)
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
        
        
        //[HttpPost]
        //public async Task<IActionResult> PostNew([FromBody] Doctor doctor)
        //{
        //    if (doctor != null)
        //    {
        //        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        //        DateOnly yesterday = today.AddDays(-1);
        //        var doctorEntity = new Doctor
        //        {
        //            Name = doctor.Name,
        //            MobileNumber = doctor.MobileNumber,
        //            Password = doctor.Password,
        //            Email = doctor.Email,
        //            PMDC = doctor.PMDC,
        //            ValidUpto = yesterday
        //        };

        //        _db.Doctors.Add(doctorEntity);
        //        await _db.SaveChangesAsync();

        //        if (doctor.Clinics != null && doctor.Clinics.Any())
        //        {
        //            foreach (var clinic in doctor.Clinics)
        //            {
        //                var clinicEntity = new Clinic
        //                {
        //                    Name = clinic.Name,
        //                    Address = clinic.Address,
        //                    Number = clinic.Number,
        //                    City = clinic.City,
        //                    Fees = clinic.Fees,
        //                    DoctorId = doctorEntity.Id,
        //                    IsOnline = true
        //                };

        //                _db.Clinics.Add(clinicEntity);
        //                await _db.SaveChangesAsync();

        //                if (clinic.ClinicTimings != null && clinic.ClinicTimings.Any())
        //                {
        //                    foreach (var clinicTiming in clinic.ClinicTimings)
        //                    {
        //                        var clinicTimingEntity = new ClinicTiming
        //                        {
        //                            Day = clinicTiming.Day,
        //                            Session = clinicTiming.Session,
        //                            StartTime = clinicTiming.StartTime,
        //                            EndTime = clinicTiming.EndTime,
        //                            ClinicId = clinicEntity.Id
        //                        };

        //                        _db.ClinicTimings.Add(clinicTimingEntity);
        //                    }
        //                    await _db.SaveChangesAsync();
        //                }
        //            }
        //        }

        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest("Doctor data is required");
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> PostDoctor([FromBody] Doctor doctor)
        {
            try
            {


                if (doctor != null)
                {
                    DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                    DateOnly yesterday = today.AddDays(-1);
                    var doctorEntity = new Doctor
                    {
                        Name = doctor.Name,
                        MobileNumber = doctor.MobileNumber,
                        Password = doctor.Password,
                        Email = doctor.Email,
                        PMDC = doctor.PMDC,
                        ValidUpto = yesterday
                    };

                    _db.Doctors.Add(doctorEntity);
                    await _db.SaveChangesAsync();
                }
                    return Created(new Uri(Request.GetEncodedUrl()), doctor); // For testing, return the DTO directly

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
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
        [HttpPatch()]
        [Route("UpdateDoctor/{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] Doctor doc)
        {
            try
            {
                var dbDoc = await _db.Doctors.FindAsync(doc.Id);
                if (dbDoc == null)
                {
                    return NotFound();
                }
                if (!string.IsNullOrEmpty(doc.Name))
                {
                    dbDoc.Name = doc.Name;
                }
                if (!string.IsNullOrEmpty(doc.Email))
                {
                    dbDoc.Email = doc.Email;
                }
                if (!string.IsNullOrEmpty(doc.MobileNumber))
                {
                    dbDoc.MobileNumber = doc.MobileNumber;
                }
                if (!string.IsNullOrEmpty(doc.PMDC))
                {
                    dbDoc.PMDC = doc.PMDC;
                }
                
                await _db.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPatch()]
        [Route("password/{id}")]
        public async Task<IActionResult> password([FromRoute] long id, [FromBody] JsonPatchDocument<Doctor> patchDocument)
        {
            try
            {
                var dbDoc = await _db.Doctors.FindAsync(id);
                if (dbDoc == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbDoc);
                await _db.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPatch]
        [Route("UpdateValidUpto/{id}")]
        public async Task<IActionResult> UpdateValidUpto([FromRoute] long id, [FromBody] Doctor doc)
        {
            try
            {
                var dbDoc = await _db.Doctors.Where(x=>x.Id==doc.Id).FirstOrDefaultAsync();
                if (dbDoc == null)
                {
                    return NotFound();
                }
               
                dbDoc.ValidUpto = doc.ValidUpto;
                
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
        [Route("doctors/{id}")]
        public async Task<IActionResult> UpdateDoctorto([FromRoute] long id, [FromBody] Doctor doc)
        {
            try
            {
                var dbDoc = await _db.Doctors.FindAsync(doc.Id);
                if (dbDoc == null)
                {
                    return NotFound();
                }
             
                 if (doc.Name != null)
                {
                    dbDoc.Name = doc.Name;
                }
                  if (doc.Email != null)
                {
                    dbDoc.Email = doc.Email;
                }
                  if (doc.MobileNumber != null)
                {
                    dbDoc.MobileNumber = doc.MobileNumber;
                }
                  if (doc.PMDC != null)
                {
                    dbDoc.PMDC = doc.PMDC;
                }
                
                await _db.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("login")]
        [HttpGet()]
        public async Task<IActionResult> Login(string MobileNumber, string Password)
        {
            try
            {
                var today = System.DateOnly.FromDateTime(DateTime.Today);
                var doctor = _db.Doctors.FirstOrDefault(a => a.MobileNumber == MobileNumber && a.Password == Password);

                if (doctor != null)
                {
                    if (doctor.ValidUpto < today)
                    {
                        // ValidUpto date has passed, contact admin
                        return BadRequest("Your account has expired. Please contact the admin.");
                    }

                    var clinics = _db.Clinics.Where(c => c.DoctorId == doctor.Id).ToList();

                    DoctorDTO doctorDTO = new DoctorDTO
                    {
                        Id = doctor.Id,
                        Name = doctor.Name,
                        Email = doctor.Email,
                        MobileNumber = doctor.MobileNumber,
                        Password = doctor.Password,
                        PMDC = doctor.PMDC,
                        ValidUpto = doctor.ValidUpto,
                        Clinics = new List<ClinicDTO>()
                    };

                    foreach (var clinic in clinics)
                    {
                        var clinictiming = _db.ClinicTimings.Where(ct => ct.ClinicId == clinic.Id).ToList();

                        ClinicDTO clinicDTO = new ClinicDTO
                        {
                            Id = clinic.Id,
                            Address = clinic.Address,
                            Name = clinic.Name,
                            Number = clinic.Number,
                            City = clinic.City,
                            Fees = clinic.Fees,
                            DoctorId = clinic.DoctorId,
                            IsOnline = clinic.IsOnline,
                            ClinicTimings = new List<ClinicTimingDTO>()
                        };

                        foreach (var ct in clinictiming)
                        {
                            ClinicTimingDTO clinicTimingDTO = new ClinicTimingDTO
                            {
                                Id = ct.Id,
                                Day = ct.Day,
                                Session = ct.Session,
                                StartTime = ct.StartTime,
                                EndTime = ct.EndTime,
                                ClinicId = ct.ClinicId
                            };

                            clinicDTO.ClinicTimings.Add(clinicTimingDTO);
                        }

                        doctorDTO.Clinics.Add(clinicDTO);
                    }

                    if (doctorDTO.Clinics.Count > 0)
                    {
                        return Ok(doctorDTO);
                    }
                    else
                    {
                        // No clinics associated with the doctor
                        return Ok(doctorDTO);
                    }
                }
                else
                {
                    return Unauthorized("Invalid MobileNumber or Password.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}