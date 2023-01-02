using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;

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

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Doctor doctor)
        {
            try{
                _db.Doctors.Add(doctor);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + doctor.Id), doctor);
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
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
        [Route("get-AllDoctors")]
        public async Task<IActionResult> GetDoctorByAsync()
        {
            try{
                var doctor = await _db.Doctors.ToListAsync();
                return Ok(doctor);
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