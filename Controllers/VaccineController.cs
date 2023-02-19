using System.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
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
            try
            {

                var vaccines = await _db.Vaccines.ToListAsync();

                return Ok(vaccines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try
            {
                var vaccine = await _db.Vaccines.FindAsync(id);
                if (vaccine == null)
                    return NotFound();
                return Ok(vaccine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Vaccine vaccine)
        {
            try
            {
                _db.Vaccines.Add(vaccine);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + vaccine.Id), vaccine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Vaccine vaccineToUpdate)
        {
            try
            {
                if (id != vaccineToUpdate.Id)
                    return BadRequest();
                var dbVaccine = await _db.Vaccines.FindAsync(id);
                if (dbVaccine == null)
                    return NotFound();
                _db.Vaccines.Update(vaccineToUpdate);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        // {
        //     try{
        //         var vaccineToDelete = await _db.Vaccines.FindAsync(id);
        //         if (vaccineToDelete == null)
        //         {
        //             return NotFound();
        //         }
        //         _db.Vaccines.Remove(vaccineToDelete);
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch(Exception ex){
        //         return StatusCode(500, "Internal server error"); 
        //     }

        // }

        [HttpGet]
        [Route("vaccine-with-count")]
        public async Task<IActionResult> GetVaccineWithCounts()
        {
            try
            {

                var vaccines = await _db.Vaccines.ToListAsync();
                List<VaccineWithCountDTO> listDTO = new List<VaccineWithCountDTO>();
                foreach (var item in vaccines)
                {
                    listDTO
                        .Add(new VaccineWithCountDTO
                        {
                            vaccine = item,
                            DoseCount = _db.Doses.Where(x => x.VaccineId == item.Id).Count(),
                            BrandCount = _db.Brands.Where(x => x.VaccineId == item.Id).Count()
                        });
                }
                return Ok(listDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] Vaccine vaccine)
        {
            try
            {
                var dbVaccine = await _db.Vaccines.FindAsync(vaccine.Id);
                if (dbVaccine == null)
                {
                    return NotFound();
                }

                dbVaccine.Name = vaccine.Name;

                _db.Entry(dbVaccine).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)

        {
            //try {
            var dbVaccine = _db.Vaccines.Include(X => X.Doses).Include(x => x.Brands).Where(x => x.Id == id).FirstOrDefault();
            if (dbVaccine.Brands.Count > 0)
                return StatusCode(500, "del brand first");
            else if (dbVaccine.Doses.Count > 0)
                return StatusCode(500, "del doses first");
            _db.Vaccines.Remove(dbVaccine);
            _db.SaveChanges();
            return NoContent();
            //    }
            //    catch (Exception ex)
            // {
            // if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
            //     return new Response<string>(false, "Cannot delete vaccine because it's doses exists. Delete the doses first.", null);

            // }

        }

    }
}
