using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using api.fernflowers.com.ModelDTO;
using AutoMapper;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;
        public VaccineController(VaccineDBContext vaccineDBContext, IMapper mapper)
        {
            _db = vaccineDBContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _db.Vaccines.ToListAsync());
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
                var vaccine = await _db.Vaccines.FindAsync(id);
                if (vaccine == null) return NotFound();
                return Ok(vaccine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] Vaccine vaccine)
        {
            try
            {
                var dbVaccine = await _db.Vaccines.FindAsync(vaccine.Id);
                if (dbVaccine == null) return NotFound();
                dbVaccine.Name = vaccine.Name;
                dbVaccine.Infinite = vaccine.Infinite;
                // dbVaccine.IsSpecial = vaccine.IsSpecial;
                _db.Entry(dbVaccine).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var dbVaccine = await _db.Vaccines.Include(X => X.Doses).Include(x => x.Brands).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (dbVaccine == null) return NotFound();
            if (dbVaccine.Brands.Count > 0) return StatusCode(500, "Cannot delete Vaccine. Delete the brands of this vaccine first.");
            else if (dbVaccine.Doses.Count > 0) return StatusCode(500, "Cannot delete Vaccine. Delete the doses of this vaccine first.");
            _db.Vaccines.Remove(dbVaccine);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpGet("{id}/brands")]
        public async Task<IActionResult> GetBrands(long id)
        {
            var dbvaccine = await _db.Vaccines.Include(x => x.Brands).FirstOrDefaultAsync(x => x.Id == id);
            if (dbvaccine == null) return NotFound();
            else
            {
                var dbBrands = dbvaccine.Brands;
                var brandDTOs = _mapper.Map<List<BrandDTO>>(dbBrands);
                return Ok(brandDTOs);
            }
        }
        [HttpGet("{id}/doses")]
        public async Task<IActionResult> GetDoses(long id)
        {
            var dbvaccine = await _db.Vaccines.Include(x => x.Doses).FirstOrDefaultAsync(x => x.Id == id);
            if (dbvaccine == null) return NotFound();
            else
            {
                var dbDoses = dbvaccine.Doses;
                var dosesDTOs = _mapper.Map<List<DoseDTO>>(dbDoses);
                return Ok(dosesDTOs);
            }
        } 
    }
}