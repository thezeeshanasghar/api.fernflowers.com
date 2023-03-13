using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public ClinicController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        [HttpGet]
        [Route("/ClinicName")]
        public async Task<ActionResult<IEnumerable<string>>> Getname()
        {
            try
            {
                var clinic = await _db.Clinics.ToListAsync();
                return Ok(clinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clinics = await _db.Clinics.ToListAsync();
                return Ok(clinics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try
            {
                var clinic = await _db.Clinics.FindAsync(id);
                if (clinic == null)
                    return NotFound();
                return Ok(clinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Clinic clinic)
        {
            try
            {
                _db.Clinics.Add(clinic);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + clinic.Id), clinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Clinic clinicToUpdate)
        {
            try
            {
                if (id != clinicToUpdate.Id)
                    return BadRequest();
                var dbClinic = await _db.Clinics.FindAsync(id);
                if (dbClinic == null)
                    return NotFound();

                _db.Clinics.Update(clinicToUpdate);
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
                var clinicToDelete = await _db.Clinics.FindAsync(id);
                if (clinicToDelete == null)
                {
                    return NotFound();
                }
                _db.Clinics.Remove(clinicToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<Clinic> patchDocument)
        {
            try
            {
                var dbClinic = await _db.Clinics.FindAsync(id);
                if (dbClinic == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbClinic);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
