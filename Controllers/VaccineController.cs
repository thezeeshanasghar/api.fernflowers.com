using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using api.fernflowers.com.ModelDTO;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public VaccineController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccines = await _db.Vaccines.ToListAsync();
            return Ok(vaccines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var vaccine = await _db.Vaccines.FindAsync(id);
            return Ok(vaccine);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Vaccine vaccine)
        {
	        _db.Vaccines.Add(vaccine);
        	await _db.SaveChangesAsync();
	        return Created(new Uri(Request.GetEncodedUrl()+ "/" + vaccine.Id), vaccine);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Vaccine vaccineToUpdate)
        {
            _db.Vaccines.Update(vaccineToUpdate);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var vaccineToDelete = await _db.Vaccines.FindAsync(id);
            if (vaccineToDelete == null)
            {
                return NotFound();
            }
            _db.Vaccines.Remove(vaccineToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("vaccine-with-count")]
        public async Task<IActionResult> GetVaccineWithCounts()
        {
            //var vaccines = await _db.Vaccines.Include(x=> x.Brands).Include(x=>x.Doses).ToListAsync();
            var vaccines = await _db.Vaccines.ToListAsync();
            List<VaccineWithCountDTO> listDTO = new List<VaccineWithCountDTO>();
             foreach (var item in vaccines){
                listDTO.Add(
                    new VaccineWithCountDTO{
                        vaccine =item,
                        DoseCount = _db.Doses.Where(x=>x.VaccineId == item.Id).Count(),
                        BrandCount = 0//item.Brands.Count()
                        });
             }
        
            return Ok(listDTO);
        }
     
    }
}
