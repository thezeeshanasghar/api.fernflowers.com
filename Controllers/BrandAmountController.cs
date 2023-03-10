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
    public class BrandAmountController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public BrandAmountController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var brandamount = await _db.BrandAmounts.ToListAsync();
                List<BrandAmountDTO> BrandAmountdto = null;
                if (brandamount != null)
                {
                    BrandAmountdto = new List<BrandAmountDTO> { };
                    foreach (var ba in brandamount)
                    {
                        var tmp_brandamount = new BrandAmountDTO
                        {
                            Id = ba.Id,
                            Amount = ba.Amount,
                            BrandId = ba.BrandId,
                            DoctorId = ba.DoctorId,
                        };
                        BrandAmountdto.Add(tmp_brandamount);
                    }
                    var brandIds = brandamount.Select(ba => ba.BrandId).ToList();
                    var brands = _db.Brands.Where(b => brandIds.Contains(b.Id)).ToList();
                    foreach (var ba in BrandAmountdto)
                    {
                        ba.BrandName = brands.FirstOrDefault(b => b.Id == ba.BrandId).Name;
                    }
                }


                return Ok(BrandAmountdto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try
            {
                var brandamount = await _db.BrandAmounts.FindAsync(id);
                if (brandamount == null)
                    return NotFound();
                return Ok(brandamount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] BrandAmount brandamount)
        {
            try
            {
                _db.BrandAmounts.Add(brandamount);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + brandamount.Id), brandamount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] BrandAmount brandamountToUpdate)
        {
            try
            {
                if (id != brandamountToUpdate.Id)
                    return BadRequest();
                var dbbrandamount = await _db.BrandAmounts.FindAsync(id);
                if (dbbrandamount == null)
                    return NotFound();

                _db.BrandAmounts.Update(brandamountToUpdate);
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
                var brandamountToDelete = await _db.BrandAmounts.FindAsync(id);
                if (brandamountToDelete == null)
                {
                    return NotFound();
                }
                _db.BrandAmounts.Remove(brandamountToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<BrandAmount> patchDocument)
        {
            try
            {
                var dbbrandamount = await _db.BrandAmounts.FindAsync(id);
                if (dbbrandamount == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbbrandamount);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // [HttpGet]
        // [Route("/BrandName")]
        // public async Task<IActionResult> GetBrandAmount()
        // {
        //     return Ok( ) ;
        // }




    }
}