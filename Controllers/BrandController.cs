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
        [HttpGet]
        [Route("/BrandName")]
        public async Task<ActionResult<IEnumerable<string>>> Getname()
        {
            try
            {
                var brandamount = await _db.Brands.ToListAsync();
                return Ok(brandamount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("brand_name/{vaccineId}")]
        
        public IActionResult GetBrandName(int vaccineId)
        {
            {
                var brand = _db.Brands.Where(b => b.VaccineId == vaccineId).Select(b => b.Name).ToList();
                if (brand == null)
                {
                    return NotFound();
                }
                return Ok(brand);
            }
        }

        [HttpGet]


        public async Task<IActionResult> GetAll()
        {
            try
            {
                var brand = await _db.Brands.ToListAsync();
                return Ok(brand);
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

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Brand brandToUpdate)
        {
            try
            {
                if (id != brandToUpdate.Id)
                    return BadRequest();
                var dbBrand = await _db.Brands.FindAsync(id);
                if (dbBrand == null)
                    return NotFound();

                _db.Brands.Update(brandToUpdate);
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

        // [HttpPatch("{id}")]
        // public async Task<IActionResult> PatchAsync([FromRoute] int id,[FromBody] JsonPatchDocument<Brand> patchDocument)
        // {
        //     try{
        //         var dbBrand = await _db.Brands.FindAsync(id);
        //         if (dbBrand == null)
        //         {
        //             return NotFound();
        //         }
        //         patchDocument.ApplyTo(dbBrand);
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch(Exception ex){
        //         return StatusCode(500, ex.Message); 
        //     }
        // }
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
