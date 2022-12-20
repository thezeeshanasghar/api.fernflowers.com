using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly VaccineDBContext _vaccineDBContext;

        public ClinicsController(VaccineDBContext vaccineDBContext)
        {
            _vaccineDBContext = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var clinics = await _vaccineDBContext.Clinics.ToListAsync();
            return Ok(clinics);
        }
        [HttpGet]
        [Route("get-clinic-by-id")]
        public async Task<IActionResult> GetClinicByIdAsync(int id)
        {
            var clinic = await _vaccineDBContext.Clinics.FindAsync(id);
            return Ok(clinic);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Clinic clinics)
        {
	        _vaccineDBContext.Clinics.Add(clinics);
        	await _vaccineDBContext.SaveChangesAsync();
	        return Created($"/get-clinic-by-id?id={clinics.Id}", clinics);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Clinic clinicToUpdate)
        {
            _vaccineDBContext.Clinics.Update(clinicToUpdate);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var clinicToDelete = await _vaccineDBContext.Clinics.FindAsync(id);
            if (clinicToDelete == null)
            {
                return NotFound();
            }
            _vaccineDBContext.Clinics.Remove(clinicToDelete);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
