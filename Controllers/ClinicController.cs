using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public ClinicController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        [HttpGet]
        [Route("clinicByDoctor")]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetClinicsByDoctorId(long doctorId)
        {
            var clinics = await _db.Clinics
                .Where(c => c.DoctorId == doctorId)
                .ToListAsync();

            return clinics;
        }
        [HttpGet]
        [Route("/ClinicName")]
        public async Task<ActionResult<IEnumerable<string>>> Getname()
        {
            try
            {
                var clinic = await _db.Clinics.ToListAsync();
                return Ok(clinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clinics = await _db.Clinics.ToListAsync();
                return Ok(clinics);
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
                var clinic = await _db.Clinics.FindAsync(id);
                if (clinic == null)
                    return NotFound();
                return Ok(clinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Clinic clinic)
        {
            try
            {
                clinic.IsOnline = false;
                _db.Clinics.Add(clinic);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + clinic.Id), clinic);
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
                var clinicToDelete = await _db.Clinics.FindAsync(id);
                if (clinicToDelete == null)
                {
                    return NotFound();
                }
                _db.Clinics.Remove(clinicToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] long id, [FromBody] JsonPatchDocument<Clinic> patchDocument)
        {
            try
            {
                var dbClinic = await _db.Clinics.FindAsync(id);
                if (dbClinic == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbClinic);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
        // [HttpPatch("ClinicIsonline/{id}")]
        
        // public async Task<IActionResult> PatchIsOnline([FromRoute] long id, [FromBody] JsonPatchDocument<Clinic> patchDocument)
        // {
        //     try
        //     {
        //         var dbClinic = await _db.Clinics.FindAsync(id);
        //         if (dbClinic == null)
        //         {
        //             return NotFound();
        //         }
        //         patchDocument.ApplyTo(dbClinic);
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
        [HttpPatch("ClinicIsonline/{doctorId}/{clinicId}")]
        public async Task<IActionResult> PatchIsOnline([FromRoute] long doctorId, [FromRoute] long clinicId, [FromBody] JsonPatchDocument<Clinic> patchDocument)
        {
            try
            {
                var dbClinic = await _db.Clinics.FindAsync(clinicId);
                if (dbClinic == null)
                {
                    return NotFound();
                }

                // Verify that the clinic being updated belongs to the specified doctor
                if (dbClinic.DoctorId != doctorId)
                {
                    return BadRequest("The specified clinic does not belong to the specified doctor.");
                }

                // Apply the patch to the clinic
                patchDocument.ApplyTo(dbClinic);

                // If the clinic is set to IsOnline == true, set all other clinics of the same doctor to IsOnline == false
                if (dbClinic.IsOnline)
                {
                    var otherClinicsOfSameDoctor = await _db.Clinics.Where(c => c.DoctorId == doctorId && c.Id != clinicId).ToListAsync();

                    foreach (var otherClinic in otherClinicsOfSameDoctor)
                    {
                        otherClinic.IsOnline = false;
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


        [HttpPatch()]
        [Route("clinic/{id}")]
        public async Task<IActionResult> UpdateClinicto([FromRoute] long id, [FromBody] Clinic cli)
        {
            try
            {
                var dbClinic = await _db.Clinics.FindAsync(cli.Id);
                if (dbClinic == null)
                {
                    return NotFound();
                }
             
                 if (cli.Name != null)
                {
                    dbClinic.Name = cli.Name;
                }
                 if (cli.Address != null)
                {
                    dbClinic.Address = cli.Address;
                }
                if (cli.Number != null)
                {
                    dbClinic.Number = cli.Number;
                }
                if (cli.City != null)
                {
                    dbClinic.City = cli.City;
                }
                if (cli.Fees != null)
                {
                    dbClinic.Fees = cli.Fees;
                }
                
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
