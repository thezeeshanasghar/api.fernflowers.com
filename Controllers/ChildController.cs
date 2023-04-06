using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;



namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly VaccineDBContext _db;

        public ChildController(VaccineDBContext vaccineDBContext)
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
        //         return StatusCode(500, ex.Message); 
        //     }
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try
            {
                var child = await _db.Childs.FindAsync(id);
                if (child == null)
                    return NotFound();
                return Ok(child);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [HttpGet("name")]
        // public async Task<IActionResult> GetAll(string Name, string City, string Gender,DateTime DOB,int DoctorId)
        // {
        //     try
        //     {
        //         // var dIds = _db.Doctors.Select(d=> d.Id).ToList();
        //         // var doctor = _db.Childs.Where(x => dIds.Contains(x.DoctorId));

        //         var child = await _db.Childs.Where(a => a.Name.ToLower().Trim() == Name.ToLower().Trim() || a.City == City && a.Gender == Gender || a.DOB.Date==DOB || a.DoctorId==DoctorId).ToListAsync();

        //         return Ok(child);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        [HttpGet("search-by-doctor-name")]
        public ActionResult<IEnumerable<Child>> SearchByDoctorName( [SwaggerParameter(Required = false)] string? doctorName=null,[SwaggerParameter(Required = false)] string? Name=null,[SwaggerParameter(Required = false)] string? City=null,[SwaggerParameter(Required = false)] string? Gender=null,[SwaggerParameter(Required = false)] DateTime? DOB=null)
        {
            var patients = _db.Childs.Join(_db.Doctors, p => p.DoctorId, d => d.Id, (p, d) => new { Child = p, Doctor = d })
                .Where(pd =>
                (doctorName==null || pd.Doctor.Name.Contains(doctorName)) &&
                (string.IsNullOrEmpty(Name) || pd.Child.Name == Name ) &&
                (City==null || pd.Child.City == City) && 
                (Gender==null ||pd.Child.Gender == Gender) &&
                (DOB ==null ||  pd.Child.DOB.Date == DOB))
                .Select(pd => new {
                    pd.Child.Id,
                    pd.Child.Name,
                    pd.Child.Guardian,
                    pd.Child.FatherName,
                    pd.Child.Email,
                    pd.Child.DOB,
                    pd.Child.Gender,
                    pd.Child.Type,
                    pd.Child.City,
                    pd.Child.CNIC,
                    pd.Child.PreferredSchedule,
                    pd.Child.IsEPIDone,
                    pd.Child.IsVerified,
                    pd.Child.IsInactive,
                    pd.Child.ClinicId,
                    pd.Child.DoctorId,
                    DoctorName = pd.Doctor.Name 
                }).ToList();


            return Ok(patients);
        }
    //  [HttpGet]
    // public IActionResult Search([FromQuery] string Name = "",
    //         [FromQuery] string City = "",
    //         [FromQuery] DateTime DOB=default,
    //         [FromQuery] string Gender = "",
    //         [FromQuery] string DoctorName = "")
    // {
    //     var children = _db.Childs.AsQueryable();

    //     if (!string.IsNullOrEmpty(Name))
    //     {
    //         children = children.Where(c => c.Name.Contains(Name));
    //     }

    //     if (!string.IsNullOrEmpty(City))
    //     {
    //         children = children.Where(c => c.City == City);
    //     }
    //     if (!string.IsNullOrEmpty(Gender))
    //     {
    //         children = children.Where(c => c.Gender == Gender);
    //     }

    //     if (DOB != DateTime.MinValue)
    //     {
    //         children = children.Where(c => c.DOB.Date == DOB.Date);
    //     }

    //     if (!string.IsNullOrEmpty(DoctorName))
    //     {
    //         // Find the doctor with the specified ID
            
    //         var doctor = _db.Doctors.FirstOrDefault(d => d.Name == DoctorName);

    //         if (doctor != null)
    //         {
                
    //             // Filter the children queryable to only include children whose DoctorId matches the specified ID
    //             // children = children.Where(c => c.DoctorId == );
    //             var patients = _db.Childs.Join(_db.Doctors, p => p.DoctorId, d => d.Id, (p, d) => new { Child = p, Doctor = d });

    //             // Create a result object that includes the doctor's name and the filtered children
    //             var search = new
    //             {
    //                 DoctorName = doctor.Name,
    //                 Children = patients.ToList()
    //             };

    //             return Ok(search);
    //         }
    //         else
    //         {
    //             // If the doctor ID is invalid, return an empty result
    //             return Ok(new { DoctorName = "", Children = new List<Child>() });
    //         }
    //     }
    //     else
    //     {
    //         // If no doctor ID was specified, return all children
    //         var search = new
    //         {
    //             DoctorName = "",
    //             Children = children.ToList()
    //         };

    //         return Ok(search);
    //     }
    // }



        // [HttpGet("{name}")]

        // public async Task<ActionResult>Search(string Name,string Gender,string City,System.DateTime DOB)
        // {
        //     try{
        //         // var child = await _db.Childs.FindAsync(name,gender,city,dob);
        //         // if(child==null){
        //         //     return NotFound();

        //         // }
        //         // return Ok(child);
        //         var list=await _db.Childs.Where(a=>a.Name==Name && a.Gender==Gender && a.City==City && a.DOB==DOB).ToListAsync();


        //         return Ok(list);



        //     }
        //     catch(Exception ex){
        //         return StatusCode(500, ex.Message); 
        //     }
        // }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] Child child)
        {
            try
            {
                _db.Childs.Add(child);
                await _db.SaveChangesAsync();
                return Created(new Uri(Request.GetEncodedUrl() + "/" + child.Id), child);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Child childToUpdate)
        {
            try
            {
                if (id != childToUpdate.Id)
                    return BadRequest();
                var dbchild = await _db.Childs.FindAsync(id);
                if (dbchild == null)
                    return NotFound();

                _db.Childs.Update(childToUpdate);
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
                var childToDelete = await _db.Childs.FindAsync(id);
                if (childToDelete == null)
                {
                    return NotFound();
                }
                _db.Childs.Remove(childToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument<Child> patchDocument)
        {
            try
            {
                var dbchild = await _db.Childs.FindAsync(id);
                if (dbchild == null)
                {
                    return NotFound();
                }
                patchDocument.ApplyTo(dbchild);
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