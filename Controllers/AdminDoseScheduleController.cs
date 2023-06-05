using System.Linq;
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
    public class AdminDoseScheduleController : ControllerBase
    {
         private readonly VaccineDBContext _db;

        public AdminDoseScheduleController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }
        [HttpGet]
        [Route("/admin_post_doseSchedule")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<DoseDTO> doseDTOList = new List<DoseDTO>();
                List<AdminDoseSchedule> doseScheduleList = new List<AdminDoseSchedule>();
                var doses = await _db.Doses.ToListAsync();
                DateTime ? doseDate = null;
                int ? lastVaccineId = null;
                foreach(var dos in doses){
                    var dosDTo = new DoseDTO{
                        Id = dos.Id,
                        Name = dos.Name,
                        MinGap = dos.MinGap,
                        VaccineId = dos.VaccineId
                    };
                    var doseSchedule = new AdminDoseSchedule{
                        DoseId = dos.Id
                    };
                    if(doseDate == null || (dosDTo.VaccineId != lastVaccineId)){
                        doseDate = DateTime.Now;
                    }else{
                        var dateOfLastDoseOfSameVaccine = doseDTOList.LastOrDefault(d=> d.VaccineId == dosDTo.VaccineId)?.DoseDate;
                        if(dateOfLastDoseOfSameVaccine!=null){
                           doseDate = dateOfLastDoseOfSameVaccine.Value.AddDays(dos.MinGap);
                        }
                    }
                    dosDTo.DoseDate = doseDate;
                    doseDTOList.Add(dosDTo); 
                    doseSchedule.Date = doseDate.Value;
                    doseScheduleList.Add(doseSchedule);
                    lastVaccineId = dosDTo.VaccineId;                   
                }
                _db.AdminDoseSchedules.AddRange(doseScheduleList);
                _db.SaveChanges();
                return Ok(doseDTOList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    [Route("Admin_single_updateDate")]

     [HttpPatch]
        public async Task<IActionResult> Update([FromBody] AdminDoseSchedule ds)
        {
            try{
                var dbDoc = await _db.AdminDoseSchedules.Where(x=>x.DoseId==ds.DoseId).FirstOrDefaultAsync();
                if (dbDoc == null)
                {
                    return NotFound();
                }
             
                dbDoc.Date = ds.Date;
                _db.Entry(dbDoc).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }
        [Route("Admin_bulk_updateDate/{date}")]
        [HttpPatch]
        
      
         public async Task<IActionResult> PatchAsync(DateTime date,[FromBody] JsonPatchDocument<AdminDoseSchedule> patchDocument)
        {
            try{
                var dbDoc = _db.AdminDoseSchedules.Where(d=>d.Date.Date==date.Date).ToList(); 
                if (dbDoc == null)
                {
                    return NotFound();
                }
                dbDoc.ForEach(d => patchDocument.ApplyTo(d));
                await _db.SaveChangesAsync();
                return NoContent();
                
            }
            catch(Exception ex){
                return StatusCode(500,ex.Message); 
            }
        }
    
        [HttpGet]
        public async Task<IActionResult>Getnew()
        {
            try
            {
                var doseSchedule =  _db.AdminDoseSchedules.ToList();
                var doseIds = doseSchedule.Select(d=> d.DoseId).ToList();
                var doses = _db.Doses.Where(x => doseIds.Contains(x.Id));
                List<AdminDoseScheduleDTO> dsDTOList = new List<AdminDoseScheduleDTO>();
                foreach(var ds in doseSchedule){
                    var dsDTO = new AdminDoseScheduleDTO
                    {
                        Id = ds.Id,
                        Date = ds.Date,
                        DoseId = ds.DoseId
                    };
                    var dose = doses.FirstOrDefault(d=> d.Id == ds.DoseId);
                    if (dose != null)
                    {
                        dsDTO.Dose = new DoseDTO {
                            Id= dose.Id,
                            DoseDate = dsDTO.Date,
                            MinAge = dose.MinAge,
                            MinGap = dose.MinGap,
                            Name = dose.Name ,
                            VaccineId = dose.VaccineId
                        };
                    }
                    dsDTOList.Add(dsDTO);
                };
                return Ok(dsDTOList.OrderBy(x=>x.Date));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
