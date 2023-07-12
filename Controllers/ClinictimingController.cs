using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicTimingController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public ClinicTimingController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clinicTimings = await _db.ClinicTimings.ToListAsync();

                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = { new StringEnumConverter() }
                };

                var convertedTimings = JsonConvert.SerializeObject(clinicTimings, Formatting.None, jsonSettings);

                return Ok(convertedTimings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] long id)
        {
            try
            {
                var clinictimings = await _db.ClinicTimings.FindAsync(id);
                if (clinictimings == null)
                    return NotFound();
                return Ok(clinictimings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] List<string> data)
        {
            try
            {
                List<ClinicTiming> clinictimings = new List<ClinicTiming>();
                foreach (var item in data)
                {
                    var clinictiming = JsonConvert.DeserializeObject<ClinicTiming>(item);
                    _db.ClinicTimings.Add(clinictiming);
                }
                await _db.SaveChangesAsync();
                return Ok(new { Message = "Sucessfully Added" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] long id, [FromBody] ClinicTiming clinictimingToUpdate)
        {
            try
            {
                if (id != clinictimingToUpdate.Id)
                    return BadRequest();
                var dbClinic = await _db.ClinicTimings.FindAsync(id);
                if (dbClinic == null)
                    return NotFound();

                _db.ClinicTimings.Update(clinictimingToUpdate);
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
                var clinictimeToDelete = await _db.ClinicTimings.FindAsync(id);
                if (clinictimeToDelete == null)
                {
                    return NotFound();
                }
                _db.ClinicTimings.Remove(clinictimeToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<ClinicTiming> patchDocument)
        {
            try
            {
                var dbClinictime = await _db.ClinicTimings.FindAsync(id);
                if (dbClinictime == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbClinictime);
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

