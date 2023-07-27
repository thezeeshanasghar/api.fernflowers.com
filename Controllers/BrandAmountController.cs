using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using AutoMapper;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandAmountController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;

        public BrandAmountController(VaccineDBContext vaccineDBContext, IMapper mapper)
        {
            _db = vaccineDBContext;
            _mapper = mapper;
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
                    
                    // Get a list of BrandIds that are still valid (not deleted)
                    var validBrandIds = brandamount
                        .Where(ba => _db.Brands.Any(b => b.Id == ba.BrandId))
                        .Select(ba => ba.BrandId)
                        .ToList();

                    // Update the BrandName for BrandAmounts that have valid BrandIds
                    var brands = _db.Brands.Where(b => validBrandIds.Contains(b.Id)).ToList();
                    foreach (var ba in BrandAmountdto)
                    {
                        var brand = brands.FirstOrDefault(b => b.Id == ba.BrandId);
                        if (brand != null)
                        {
                            ba.BrandName = brand.Name;
                            ba.BrandId=ba.BrandId;
                        }
                        else
                        {
                            // Handle the case where the BrandId is no longer valid (deleted)
                            ba.BrandName = "Brand Deleted"; // Or any other appropriate message
                            ba.BrandId=null;
                        }
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
        public async Task<IActionResult> GetSingle([FromRoute] long id)
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
                 var existingBrandAmount = _db.BrandAmounts.SingleOrDefault(b => b.BrandId == brandamount.BrandId && b.DoctorId == brandamount.DoctorId);
        
                if (existingBrandAmount != null)
                {
                    // If the BrandId already exists for the DoctorId, add the amount to the existing row
                    existingBrandAmount.Amount += brandamount.Amount;
                    await _db.SaveChangesAsync();
                    
                    return Ok(existingBrandAmount);
                }
                _db.BrandAmounts.Add(brandamount);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + brandamount.Id), brandamount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [HttpPut]
        // public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] BrandAmount brandamountToUpdate)
        // {
        //     try
        //     {
        //         if (id != brandamountToUpdate.Id)
        //             return BadRequest();
        //         var dbbrandamount = await _db.BrandAmounts.FindAsync(id);
        //         if (dbbrandamount == null)
        //             return NotFound();

        //         _db.BrandAmounts.Update(brandamountToUpdate);
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
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
        public async Task<IActionResult> PatchAsync([FromRoute] long id, [FromBody] JsonPatchDocument<BrandAmount> patchDocument)
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
        
        [HttpGet("doctor-vaccine-price/{doctorId}")]
        public IActionResult GetDoctorVaccinePrices(long doctorId)
        {
            var brandAmounts = _db.BrandAmounts.Where(ba => ba.DoctorId == doctorId).ToList();
            var result = new List<object>();
            foreach (var ba in brandAmounts)
            {
                var brandName = _db.Brands.Where(b => b.Id == ba.BrandId).Select(b => b.Name).FirstOrDefault();
                var vaccineName = _db.Vaccines.Where(v => v.Brands.Any(b => b.Id == ba.BrandId)).Select(v => v.Name).FirstOrDefault();
                var obj = new {
                    VaccineName = vaccineName,
                    Brand = brandName,
                    Price = ba.Amount
                };
                result.Add(obj);
            }
            return Ok(result);
        }
    }
}