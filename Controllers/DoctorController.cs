using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using api.fernflowers.com.ModelDTO;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly VaccineDBContext _db;
      

       
       


        public DoctorController(VaccineDBContext vaccineDBContext )
        {
            _db = vaccineDBContext;
            
         
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try{
                var doctor = await _db.Doctors.ToListAsync();
                return Ok(doctor);
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try{
                var doctor = await _db.Doctors.FindAsync(id);
                if(doctor==null)
                    return NotFound();
                return Ok(doctor);
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
         
            }
        }


        [Route("login")] 
        [HttpGet()]
        
        public async Task<IActionResult>Login(int MobileNumber , string Password)
        {
            try{
                
                var list=await _db.Doctors.Where(a=>a.MobileNumber==MobileNumber && a.Password==Password && a.Isapproved==true).ToListAsync();
                

                return Ok(list);

                    
                
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] DoctorDTO doctor)
        {
                var doctorEntity = new Doctor{
                    Name = doctor.Name,
                    Email = doctor.Email,
                    Isapproved = doctor.IsApproved,
                    IsEnabled = doctor.IsEnabled,
                    DoctorType = doctor.DoctorType,
                    MobileNumber = doctor.MobileNumber,
                    Password = doctor.Password,
                    PMDC = doctor.PMDC
                };
                var clinicEntity = new Clinic{
                    Address = doctor.Clinic.Address,
                    Name = doctor.Clinic.Name,
                    Number = doctor.Clinic.Number
                };
                // var clinictimingEntity = new Clinictiming{
                //     Day=doctor.Clinic.Clinictiming.Day,
                //     Session = doctor.Clinic.Clinictiming.Session,
                //     StartTime=doctor.Clinic.Clinictiming.StartTime,
                //     EndTime=doctor.Clinic.Clinictiming.EndTime
                // };
                _db.Doctors.Add(doctorEntity);
                await _db.SaveChangesAsync();

                clinicEntity.DoctorId = doctorEntity.Id;
                _db.Clinics.Add(clinicEntity);
                await _db.SaveChangesAsync();

                // clinictimingEntity.ClinicId=clinicEntity.Id;
                // _db.Clinictimings.Add(clinictimingEntity);
                // await _db.SaveChangesAsync();

                return Created(new Uri(Request.GetEncodedUrl() + "/" + doctorEntity.Id), doctorEntity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Doctor doctorToUpdate)
        {
            try{
                if(id != doctorToUpdate.Id)
                    return BadRequest();
                var dbDoctor = await _db.Doctors.FindAsync(id);
                if(dbDoctor==null)
                    return NotFound();

                _db.Doctors.Update(doctorToUpdate);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try{
                var doctorToDelete = await _db.Doctors.FindAsync(id);
                if (doctorToDelete == null)
                {
                    return NotFound();
                }
                _db.Doctors.Remove(doctorToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id,[FromBody] JsonPatchDocument<Doctor> patchDocument)
        {
            try{
                var dbDoctor = await _db.Doctors.FindAsync(id);
                if (dbDoctor == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbDoctor);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }



        [HttpGet]
        [Route("approved/{approved:bool}")]
        public async Task<IActionResult> GetApprovedDoctors(bool approved)
        {
            try{
                var doctor = await _db.Doctors.Where(x => x.Isapproved == approved).ToListAsync();
                return Ok(doctor);
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }

    }
}