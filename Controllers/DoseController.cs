using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoseController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public DoseController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }

        // [HttpGet]
        // [Route("/doseschedule_date")]
        // public async Task<IActionResult> GetAll()
        // {
        //     try
        //     {
        //         List<DoseDTO> doseDTOList = new List<DoseDTO>();
        //         var doses = await _db.Doses.ToListAsync();
        //         DateTime? doseDate = null;
        //         long lastVaccineId = -1;
        //         foreach (var dos in doses)
        //         {
        //             var dosDTo = new DoseDTO
        //             {
        //                 Id = dos.Id,
        //                 Name = dos.Name,
        //                 VaccineId = dos.VaccineId
        //             };

        //             if (doseDate == null || (dosDTo.VaccineId != lastVaccineId))
        //             {
        //                 doseDate = DateTime.Now;
        //             }
        //             doseDTOList.Add(dosDTo);
        //             lastVaccineId = dosDTo.VaccineId;
        //         }

        //         return Ok(doseDTOList);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        // [HttpGet]
        // [Route("/post_doseSchedule")]
        // public async Task<IActionResult> Get()
        // {
        //     try
        //     {
        //         List<DoseDTO> doseDTOList = new List<DoseDTO>();
        //         List<AdminSchedule> doseScheduleList = new List<AdminSchedule>();
        //         var doses = await _db.Doses.ToListAsync();
        //         DateTime? doseDate = null;
        //         long lastVaccineId = -1;
        //         foreach (var dos in doses)
        //         {
        //             var dosDTo = new DoseDTO
        //             {
        //                 Id = dos.Id,
        //                 Name = dos.Name,
        //                 VaccineId = dos.VaccineId
        //             };
        //             var doseSchedule = new AdminSchedule
        //             {
        //                 DoseId = dos.Id
        //             };
        //             if (doseDate == null || (dosDTo.VaccineId != lastVaccineId))
        //             {
        //                 doseDate = DateTime.Now;
        //             }
        //             else
        //             {
        //                 // var dateOfLastDoseOfSameVaccine = doseDTOList.LastOrDefault(d=> d.VaccineId == dosDTo.VaccineId)?.DoseDate;
        //                 // if(dateOfLastDoseOfSameVaccine!=null){
        //                 //    doseDate = dateOfLastDoseOfSameVaccine.Value.AddDays(dos.MinGap);
        //                 // }
        //             }
        //             // dosDTo.DoseDate = doseDate;
        //             doseDTOList.Add(dosDTo);
        //             // doseSchedule.Date = doseDate.Value;
        //             doseScheduleList.Add(doseSchedule);
        //             lastVaccineId = dosDTo.VaccineId;
        //         }
        //         _db.AdminSchedules.AddRange(doseScheduleList);
        //         _db.SaveChanges();
        //         return Ok(doseDTOList);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] long id)
        {
            try
            {
                var dose = await _db.Doses.FindAsync(id);
                if (dose == null)
                    return NotFound();
                return Ok(dose);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("alldoses")]
        public async Task<IActionResult> GetAllc()
        {
            try
            {
                var doses = await _db.Doses.ToListAsync();
                return Ok(doses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Dose dose)
        {
            try
            {
                _db.Doses.Add(dose);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + dose.Id), dose);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [HttpPut]
        // public async Task<IActionResult> PutAsync([FromRoute] long id, [FromBody] Dose doseToUpdate)
        // {
        //     try
        //     {
        //         if (id != doseToUpdate.Id)
        //             return BadRequest();
        //         var dbDose = await _db.Doses.FindAsync(id);
        //         if (dbDose == null)
        //             return NotFound();


        //         _db.Doses.Update(doseToUpdate);
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
                var doseToDelete = await _db.Doses.FindAsync(id);
                if (doseToDelete == null)
                {
                    return NotFound();
                }
                _db.Doses.Remove(doseToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //  [HttpPatch("{id}")]
        // public async Task<IActionResult> PatchAsync([FromRoute] int id[FromBody] JsonPatchDocument<Dose> patchDocument)
        // {
        //     try{
        //         var dbDose = await _db.Doses.FindAsync(id);
        //         if (dbDose == null)
        //         {
        //             return NotFound();
        //         }
        //         patchDocument.ApplyTo(dbDose);
        //         await _db.SaveChangesAsync();
        //         return NoContent();

        //     }
        //     catch(Exception ex){
        //         return StatusCode(500, ex.Message); 
        //     }
        // }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] Dose ds)
        {
            try
            {
                var dbDose = await _db.Doses.FindAsync(ds.Id);
                if (dbDose == null)
                {
                    return NotFound();
                }
                dbDose.Name = ds.Name;
                dbDose.MinAge = ds.MinAge;
                _db.Entry(dbDose).State = EntityState.Modified;
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
