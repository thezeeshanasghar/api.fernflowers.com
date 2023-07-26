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

                // var jsonSettings = new JsonSerializerSettings
                // {
                //     ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //     Converters = { new StringEnumConverter() }
                // };

                // var convertedTimings = JsonConvert.SerializeObject(clinicTimings, Formatting.None, jsonSettings);

                return Ok(clinicTimings);
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
         [HttpGet]
        [Route("GET-ClinicTiming/{clinicId}")]
        public async Task<List<ClinicTiming>> GetClinictimingsByClinicId(long clinicId)
        {
            var clinictimings = await _db.ClinicTimings
                .Where(c => c.ClinicId == clinicId)
                .ToListAsync();

            return clinictimings;
        }
 

        [Route("api/clintimings/AddorUpdate/{clinicId}")]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateClinicTimings(long clinicId, [FromBody] List<ClinicTiming> clinicTimings)
        {
            try
            {
                if (clinicTimings == null || !clinicTimings.Any())
                {
                    return BadRequest("No clinic timings provided.");
                }

                foreach (var ct in clinicTimings)
                {
                    // Check if a ClinicTiming with the same day and session already exists
                    var existingClinicTiming = await _db.ClinicTimings.FirstOrDefaultAsync(c =>
                        c.Day == ct.Day && c.Session == ct.Session && c.ClinicId == clinicId);

                    if (existingClinicTiming != null)
                    {
                        // Update existing ClinicTiming entity
                        existingClinicTiming.StartTime = ct.StartTime;
                        existingClinicTiming.EndTime = ct.EndTime;
                    }
                    else
                    {
                        // Add new ClinicTiming entity
                        var newClinicTiming = new ClinicTiming
                        {
                            Day = ct.Day,
                            Session = ct.Session,
                            StartTime = ct.StartTime,
                            EndTime = ct.EndTime,
                            ClinicId = clinicId
                        };
                        _db.ClinicTimings.Add(newClinicTiming);
                    }
                }

                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
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

        [Route("api/clintimings/{clinicId}")]
        [HttpPatch]
        public async Task<IActionResult> UpdateClinicTimings(long clinicId, [FromBody] List<ClinicTiming> updatedTimings)
        {
            try
            {
                if (updatedTimings == null || !updatedTimings.Any())
                {
                    return BadRequest("No updated clinic timings provided.");
                }

                var timingIds = updatedTimings.Select(t => t.Id).ToList();

                var existingTimings = await _db.ClinicTimings.Where(t => timingIds.Contains(t.Id) && t.ClinicId == clinicId).ToListAsync();

                if (existingTimings == null || existingTimings.Count == 0)
                {
                    return NotFound();
                }

                foreach (var updatedTiming in updatedTimings)
                {
                    var existingTiming = existingTimings.FirstOrDefault(t => t.Id == updatedTiming.Id);

                    if (existingTiming != null)
                    {
                        existingTiming.Day = updatedTiming.Day;
                        existingTiming.Session = updatedTiming.Session;
                        existingTiming.StartTime = updatedTiming.StartTime;
                        existingTiming.EndTime = updatedTiming.EndTime;
                        existingTiming.ClinicId = updatedTiming.ClinicId;
                    }
                }

                await _db.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // [HttpPatch("{id}")]
        // public async Task<IActionResult> PatchAsync([FromRoute] long id, [FromBody] JsonPatchDocument<ClinicTiming> patchDocument)
        // {
        //     try
        //     {
        //         var dbClinictime = await _db.ClinicTimings.FindAsync(id);
        //         if (dbClinictime == null)
        //         {
        //             return NotFound();
        //         }
        //         patchDocument.ApplyTo(dbClinictime);
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
    }
}

