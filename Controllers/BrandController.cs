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
    public class BrandController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public BrandController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        [HttpGet("brand_name/{vaccineId}")]
        public IActionResult GetBrandName(long vaccineId)
        {
                var brand = _db.Brands.Where(b => b.VaccineId == vaccineId).ToList();
                if (brand == null)
                    return NotFound();
                return Ok(brand);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] long id)
        {
            try
            {
                var brand = await _db.Brands.FindAsync(id);
                if (brand == null)
                    return NotFound();
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Brand brand)
        {
            try
            {
                _db.Brands.Add(brand);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + brand.Id), brand);
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
                var brandToDelete = await _db.Brands.FindAsync(id);
                if (brandToDelete == null)
                {
                    return NotFound();
                }
                _db.Brands.Remove(brandToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] Brand brand)
        {
            try
            {
                var dbBrand = await _db.Brands.FindAsync(brand.Id);
                if (dbBrand == null)
                {
                    return NotFound();
                }

                dbBrand.Name = brand.Name;

                _db.Entry(dbBrand).State = EntityState.Modified;
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
