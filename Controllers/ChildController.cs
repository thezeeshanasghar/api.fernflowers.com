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
    public class ChildController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public ChildController (VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     try{
        //         var child = await _db.Childs.ToListAsync();
        //         return Ok(child);
        //     }
        //     catch(Exception ex){
        //         return StatusCode(500, "Internal server error"); 
        //     }
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try{
                var child = await _db.Childs.FindAsync(id);
                if(child==null)
                    return NotFound();
                return Ok(child);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error"); 
            }
        }
        
         [HttpGet]
        public async Task<IActionResult> GetAll(string Name,string City, string DOB,string Gender)
        {
            try{
                var child = await _db.Childs.Where(a=>a.Name==Name && a.City==City  && a.DOB==Convert.ToDateTime(DOB) && a.Gender==Gender ).ToListAsync();
                return Ok(child);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error"); 
            }
        }
        
  

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Child child)
        {
            try{
                _db.Childs.Add(child);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + child.Id), child);
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Child childToUpdate)
        {
            try{
                if(id != childToUpdate.Id)
                    return BadRequest();
                var dbchild= await _db.Childs.FindAsync(id);
                if(dbchild==null)
                    return NotFound();

                _db.Childs.Update(childToUpdate);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try{
                var childToDelete = await _db.Childs.FindAsync(id);
                if (childToDelete == null)
                {
                    return NotFound();
                }
                _db.Childs.Remove(childToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id,[FromBody] JsonPatchDocument<Child> patchDocument)
        {
            try{
                var dbchild = await _db.Childs.FindAsync(id);
                if (dbchild == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbchild);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return StatusCode(500, "Internal server error"); 
            }
        }



    }
}