using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoseController : ControllerBase
    {
        private readonly VaccineDBContext _vaccineDBContext;

        public DoseController(VaccineDBContext vaccineDBContext)
        {
            _vaccineDBContext = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var doses = await _vaccineDBContext.Doses.ToListAsync();
            return Ok(doses);
        }
        [HttpGet]
        [Route("get-dose-by-id")]
        public async Task<IActionResult> GetDoseByIdAsync(int id)
        {
            var dose = await _vaccineDBContext.Doses.FindAsync(id);
            return Ok(dose);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Dose doses)
        {
	        _vaccineDBContext.Doses.Add(doses);
        	await _vaccineDBContext.SaveChangesAsync();
	        return Created($"/get-brand-by-id?id={doses.Id}", doses);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Dose doseToUpdate)
        {
            _vaccineDBContext.Doses.Update(doseToUpdate);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var doseToDelete = await _vaccineDBContext.Doses.FindAsync(id);
            if (doseToDelete == null)
            {
                return NotFound();
            }
            _vaccineDBContext.Doses.Remove(doseToDelete);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }
     
    }
}
