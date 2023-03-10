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
    public class DoseScheduleController : ControllerBase
    {
         private readonly VaccineDBContext _db;

        public DoseScheduleController(VaccineDBContext vaccineDBContext)
        {
            _db = vaccineDBContext;
        }
 
    

     [HttpPatch]
        public async Task<IActionResult> Update([FromBody] DoseSchedule ds)
        {
            try{
                var dbDoc = await _db.DoseSchedules.Where(x=>x.DoseId==ds.DoseId).FirstOrDefaultAsync();
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
        [Route("updateDate/{date}")]
        [HttpPatch]
        
      
         public async Task<IActionResult> PatchAsync(DateTime date,[FromBody] JsonPatchDocument<DoseSchedule> patchDocument)
        {
            try{
                var dbDoc = _db.DoseSchedules.Where(d=>d.Date.Date==date.Date).ToList(); 
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
                var doseSchedule =  _db.DoseSchedules.ToList();
                var doseIds = doseSchedule.Select(d=> d.DoseId).ToList();
                var doses = _db.Doses.Where(x => doseIds.Contains(x.Id));
                List<DoseScheduleDTO> dsDTOList = new List<DoseScheduleDTO>();
                foreach(var ds in doseSchedule){
                    var dsDTO = new DoseScheduleDTO
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
