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
    public class ClinictimingController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public ClinictimingController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clinictimings = await _db.Clinictimings.ToListAsync();
                return Ok(clinictimings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try
            {
                var clinictimings = await _db.Clinictimings.FindAsync(id);
                if (clinictimings == null)
                    return NotFound();
                return Ok(clinictimings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Clinictiming clinictiming)
        {
            try
            {
                _db.Clinictimings.Add(clinictiming);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + clinictiming.Id), clinictiming);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Clinictiming clinictimingToUpdate)
        {
            try
            {
                if (id != clinictimingToUpdate.Id)
                    return BadRequest();
                var dbClinic = await _db.Clinictimings.FindAsync(id);
                if (dbClinic == null)
                    return NotFound();

                _db.Clinictimings.Update(clinictimingToUpdate);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                var clinictimeToDelete = await _db.Clinictimings.FindAsync(id);
                if (clinictimeToDelete == null)
                {
                    return NotFound();
                }
                _db.Clinictimings.Remove(clinictimeToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<Clinictiming> patchDocument)
        {
            try
            {
                var dbClinictime = await _db.Clinictimings.FindAsync(id);
                if (dbClinictime == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbClinictime);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

