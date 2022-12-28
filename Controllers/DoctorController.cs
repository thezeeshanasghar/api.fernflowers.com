using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.ModelDTO;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly VaccineDBContext _vaccineDBContext;

        public DoctorController(VaccineDBContext vaccineDBContext)
        {
            _vaccineDBContext = vaccineDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var doctors = await _vaccineDBContext.Doctors.ToListAsync();
            return Ok(doctors);
        }

        [HttpGet]
        [Route("get-doctor-by-id")]
        public async Task<IActionResult> GetDoctorByIdAsync(int id)
        {
            var doctor = await _vaccineDBContext.Doctors.FindAsync(id);
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Doctor doctors)
        {
            _vaccineDBContext.Doctors.Add(doctors);
            await _vaccineDBContext.SaveChangesAsync();
            return Created($"/get-doctor-by-id?id={doctors.Id}", doctors);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Doctor doctorToUpdate)
        {
            _vaccineDBContext.Doctors.Update(doctorToUpdate);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var doctorToDelete = await _vaccineDBContext.Doctors.FindAsync(id);
            if (doctorToDelete == null)
            {
                return NotFound();
            }
            _vaccineDBContext.Doctors.Remove(doctorToDelete);
            await _vaccineDBContext.SaveChangesAsync();
            return NoContent();
        }

        // [HttpPost("login")]
        // public Response<DoctorDTO> login(DoctorDTO userDTO)
        // {

        //     {
        //         var dbUser = _vaccineDBContext.Doctors.FirstOrDefault(x =>
        //                                                     x.MobileNumber == userDTO.MobileNumber &&
        //                                                     x.Password == userDTO.Password);
        //         if (dbUser == null)
        //         {
        //             return new Response<DoctorDTO>(false, "Invalid Mobile Number and Password.", null);

        //             userDTO.Id = dbUser.Id;
        //         }
        //         else (userDTO.DoctorType.Equals("DOCTOR"))
        //             {

        //             var doctorDb = _vaccineDBContext.Doctors.Where(x => x.Id == dbUser.Id).FirstOrDefault();
        //             if (doctorDb == null)
        //                 return new Response<DoctorDTO>(false, "Doctor not found.", null);
        //             if (doctorDb.IsApproved != true)
        //                 return new Response<DoctorDTO>(false, "You are not approved. Contact admin for approval at 923335196658", null);

        //             userDTO.Id = doctorDb.Id;


        //             userDTO.DoctorType = doctorDb.DoctorType;

        //         }


        //         return new Response<DoctorDTO>(true, null, userDTO);
        //     }
        // }
    }
}