using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly VaccineDBContext _vaccineDBContext;

        public BrandsController(VaccineDBContext vaccineDBContext)
        {
            _vaccineDBContext = vaccineDBContext;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAsync()
        {
            var brands = await _vaccineDBContext.Brands.ToListAsync();
            return Ok(brands);
        }

        [HttpGet]
        [Route("get-brand-by-id")]
        public async Task<IActionResult> GetBrandByIdAsync(int id)
        {
            var brand = await _vaccineDBContext.Brands.FindAsync(id);
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Brand brands)
        {
	        _vaccineDBContext.Brands.Add(brands);
        	await _vaccineDBContext.SaveChangesAsync();
	        return Created($"/get-brand-by-id?id={brands.Id}", brands);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Brand brandToUpdate)
        {
            _vaccineDBContext.Brands.Update(brandToUpdate);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var brandToDelete = await _vaccineDBContext.Brands.FindAsync(id);
            if (brandToDelete == null)
            {
                return NotFound();
            }
            _vaccineDBContext.Brands.Remove(brandToDelete);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }
     }
}
