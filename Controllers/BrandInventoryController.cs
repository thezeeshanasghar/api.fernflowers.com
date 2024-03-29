using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using api.fernflowers.com.ModelDTO;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandInventoryController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public BrandInventoryController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var brandinventory = await _db.BrandInventories.ToListAsync();
                List<BrandInventoryDTO> BrandInventorydto = null;
                if (brandinventory != null)
                {
                    BrandInventorydto = new List<BrandInventoryDTO> { };
                    foreach (var ba in brandinventory)
                    {
                        var tmp_brandinventory = new BrandInventoryDTO
                        {
                            Id = ba.Id,
                            Count = ba.Count,
                            BrandId = ba.BrandId,
                            DoctorId = ba.DoctorId,
                        };
                        BrandInventorydto.Add(tmp_brandinventory);
                    }

                     var validBrandIds = brandinventory
                        .Where(ba => _db.Brands.Any(b => b.Id == ba.BrandId))
                        .Select(ba => ba.BrandId)
                        .ToList();
                  
                    var brands = _db.Brands.Where(b => validBrandIds.Contains(b.Id)).ToList();
                    foreach (var bi in BrandInventorydto)
                    {
                        var brand = brands.FirstOrDefault(b => b.Id == bi.BrandId);
                        if (brand != null)
                        {
                            bi.BrandName = brand.Name;
                            bi.BrandId=bi.BrandId;
                        }
                        else
                        {
                            // Handle the case where the BrandId is no longer valid (deleted)
                            bi.BrandName = "Brand Deleted"; // Or any other appropriate message
                            bi.BrandId=null;
                        }
                    }
                }
                return Ok(BrandInventorydto);
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
                var brandinventory = await _db.BrandInventories.FindAsync(id);
                if (brandinventory == null)
                    return NotFound();
                return Ok(brandinventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] BrandInventory brandinventory)
        {
            try
            {
                 var existingBrandInv = _db.BrandInventories.SingleOrDefault(b => b.BrandId == brandinventory.BrandId && b.DoctorId == brandinventory.DoctorId);
        
                if (existingBrandInv != null)
                {
                    // If the BrandId already exists for the DoctorId, add the amount to the existing row
                    existingBrandInv.Count = brandinventory.Count;;
                    await _db.SaveChangesAsync();
                    
                    return Ok(existingBrandInv);
                }
                _db.BrandInventories.Add(brandinventory);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + brandinventory.Id), brandinventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [HttpPut]
        // public async Task<IActionResult> PutAsync([FromRoute] long id, [FromBody] BrandInventory brandinventoryToUpdate)
        // {
        //     try
        //     {
        //         if (id != brandinventoryToUpdate.Id)
        //             return BadRequest();
        //         var dbbrandinventory = await _db.BrandInventories.FindAsync(id);
        //         if (dbbrandinventory == null)
        //             return NotFound();

        //         _db.BrandInventories.Update(brandinventoryToUpdate);
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
                var brandinventoryToDelete = await _db.BrandInventories.FindAsync(id);
                if (brandinventoryToDelete == null)
                {
                    return NotFound();
                }
                _db.BrandInventories.Remove(brandinventoryToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] long id, [FromBody] JsonPatchDocument<BrandInventory> patchDocument)
        {
            try
            {
                var dbbrandinventory = await _db.BrandInventories.FindAsync(id);
                if (dbbrandinventory == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbbrandinventory);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    [HttpGet("doctor-vaccine-Count/{doctorId}")]
    public IActionResult GetDoctorVaccinePrices(long doctorId)
    {
        var brandInventories = _db.BrandInventories.Where(ba => ba.DoctorId == doctorId).ToList();
        var result = new List<object>();
        foreach (var ba in brandInventories)
        {
            var brandName = _db.Brands.Where(b => b.Id == ba.BrandId).Select(b => b.Name).FirstOrDefault();
            var vaccineName = _db.Vaccines.Where(v => v.Brands.Any(b => b.Id == ba.BrandId)).Select(v => v.Name).FirstOrDefault();
            var obj = new {
                VaccineName = vaccineName,
                Brand = brandName,
                Count = ba.Count
            };
            result.Add(obj);
        }
        
        return Ok(result);
    }

    }
}