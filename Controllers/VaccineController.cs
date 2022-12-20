using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly VaccineDBContext _vaccineDBContext;

        public VaccineController(VaccineDBContext vaccineDBContext)
        {
            _vaccineDBContext = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var vaccines = await _vaccineDBContext.Vaccines.ToListAsync();
            return Ok(vaccines);
        }
        [HttpGet]
        [Route("get-vaccine-by-id")]
        public async Task<IActionResult> GetBrandByIdAsync(int id)
        {
            var vaccine = await _vaccineDBContext.Vaccines.FindAsync(id);
            return Ok(vaccine);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Vaccine vaccines)
        {
	        _vaccineDBContext.Vaccines.Add(vaccines);
        	await _vaccineDBContext.SaveChangesAsync();
	        return Created($"/get-brand-by-id?id={vaccines.Id}", vaccines);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Vaccine vaccineToUpdate)
        {
            _vaccineDBContext.Vaccines.Update(vaccineToUpdate);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var vaccineToDelete = await _vaccineDBContext.Vaccines.FindAsync(id);
            if (vaccineToDelete == null)
            {
                return NotFound();
            }
            _vaccineDBContext.Vaccines.Remove(vaccineToDelete);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }
     
    }
}
