// using api.fernflowers.com.Data;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace api.fernflowers.com.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class DoctorsController : ControllerBase
//     {
//         private readonly VaccineDBContext _vaccineDBContext;

//         public DoctorsController(VaccineDBContext vaccineDBContext)
//         {
//             _vaccineDBContext = vaccineDBContext;
//         }

//         [HttpGet]
//         public async Task<IActionResult> GetAsync()
//         {
//             var doctors = await _vaccineDBContext.Doctors.ToListAsync();
//             return Ok(doctors);
//         }
//         [HttpGet]
//         [Route("get-doctor-by-id")]
//         public async Task<IActionResult> GetDoctorByIdAsync(int id)
//         {
//             var doctor = await _vaccineDBContext.Doctors.FindAsync(id);
//             return Ok(doctor);
//         }

//         [HttpPost]
//         public async Task<IActionResult> PostAsync(Doctor doctors)
//         {
// 	        _vaccineDBContext.Doctors.Add(doctors);
//         	await _vaccineDBContext.SaveChangesAsync();
// 	        return Created($"/get-doctor-by-id?id={doctors.Id}", doctors);
//         }

//         [HttpPut]
//         public async Task<IActionResult> PutAsync(Brand brandToUpdate)
//         {
//             _vaccineDBContext.Brands.Update(brandToUpdate);
//             await _vaccineDBContext.SaveChangesAsync();
//             return NoContent();
//         }

//         [Route("{id}")]
//         [HttpDelete]
//         public async Task<IActionResult> DeleteAsync(int id)
//         {
//             var brandToDelete = await _vaccineDBContext.Brands.FindAsync(id);
//             if (brandToDelete == null)
//             {
//                 return NotFound();
//             }
//             _vaccineDBContext.Brands.Remove(brandToDelete);
//             await _vaccineDBContext.SaveChangesAsync();
//             return NoContent();
//         }
//     }
// }
