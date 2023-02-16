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

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     try{
        //         var brandinventory = await _db.BrandInventories.ToListAsync();
        //         return Ok(brandinventory);
        //     }
        //     catch(Exception ex){
        //         return StatusCode(500, ex.Message); 
        //     }
        // }
           [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try{
                var brandinventory = await _db.BrandInventories.ToListAsync();
                List<BrandInventoryDTO> BrandInventorydto = null;
                if(brandinventory!=null)
                {
                    BrandInventorydto=new List<BrandInventoryDTO>{};
                    foreach(var ba in brandinventory)
                    {
                        var tmp_brandinventory= new BrandInventoryDTO{
                        Id=ba.Id,
                        Count= ba.Count,
                        BrandId =ba.BrandId,
                        DoctorId = ba.DoctorId,
                        };
                        BrandInventorydto.Add(tmp_brandinventory);
                    }
                    var brandIds = brandinventory.Select(bi => bi.BrandId).ToList();
                    var brands = _db.Brands.Where( b=> brandIds.Contains(b.Id)).ToList();
                    foreach(var bi in BrandInventorydto)
                    {
                        bi.BrandName = brands.FirstOrDefault(b=>b.Id == bi.BrandId).Name;
                    }
                }
               

                return Ok(BrandInventorydto);
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try{
                var brandinventory = await _db.BrandInventories.FindAsync(id);
                if(brandinventory==null)
                    return NotFound();
                return Ok(brandinventory);
            }
            catch(Exception ex){
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] BrandInventory brandinventory)
        {
            try{
                _db.BrandInventories.Add(brandinventory);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + brandinventory.Id), brandinventory);
            }
            catch(Exception ex){
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] BrandInventory brandinventoryToUpdate)
        {
            try{
                if(id != brandinventoryToUpdate.Id)
                    return BadRequest();
                var dbbrandinventory = await _db.BrandInventories.FindAsync(id);
                if(dbbrandinventory==null)
                    return NotFound();

                _db.BrandInventories.Update(brandinventoryToUpdate);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500, ex.Message); 
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try{
                var brandinventoryToDelete = await _db.BrandInventories.FindAsync(id);
                if (brandinventoryToDelete == null)
                {
                    return NotFound();
                }
                _db.BrandInventories.Remove(brandinventoryToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500, ex.Message); 
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id,[FromBody] JsonPatchDocument<BrandInventory> patchDocument)
        {
            try{
                var dbbrandinventory = await _db.BrandInventories.FindAsync(id);
                if (dbbrandinventory == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbbrandinventory);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }



        

    }
}