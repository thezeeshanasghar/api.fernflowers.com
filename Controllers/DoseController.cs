using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoseController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public DoseController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] long id)
        {
            try
            {
                var dose = await _db.Doses.FindAsync(id);
                if (dose == null)
                    return NotFound();
                return Ok(dose);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
  
        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Dose dose)
        {
            try
            {
                _db.Doses.Add(dose);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + dose.Id), dose);
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
                var doseToDelete = await _db.Doses.FindAsync(id);
                if (doseToDelete == null)
                {
                    return NotFound();
                }
                _db.Doses.Remove(doseToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] Dose ds)
        {
            try
            {
                var dbDose = await _db.Doses.FindAsync(ds.Id);
                if (dbDose == null)
                {
                    return NotFound();
                }
                dbDose.Name = ds.Name;
                dbDose.MinAge = ds.MinAge;
                _db.Entry(dbDose).State = EntityState.Modified;
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
