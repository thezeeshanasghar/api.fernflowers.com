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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] long id)
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

        // [HttpGet("search-by-doctor-name")]
        // public ActionResult<IEnumerable<Child>> SearchByDoctorName(
        // string? doctorName = null,string? Name = null,string? City = null,
        // string? Gender = null,int? fromDay = null,int? toDay = null,
        // int? fromMonth = null,int? toMonth = null,int? fromYear = null,
        // int? toYear = null
        // )
        // {
        //     List<Child> patients = _db.Childs
        //         .Join(
        //             _db.Doctors,
        //             p => p.DoctorId,
        //             d => d.Id,
        //             (p, d) => new { Child = p, Doctor = d }
        //         )
        //         .Where(
        //             pd =>
        //                 (doctorName == null || pd.Doctor.Name.Contains(doctorName))
        //                 && (string.IsNullOrEmpty(Name) || pd.Child.Name == Name)
        //                 && (City == null || pd.Child.City == City)
        //                 && (Gender == null || pd.Child.Gender == api.fernflowers.com.Data.Entities.Gender.Boy)
        //                 && (fromDay == null || pd.Child.DOB.Day >= fromDay)
        //                 && (toDay == null || pd.Child.DOB.Day <= toDay)
        //                 && (fromMonth == null || pd.Child.DOB.Month >= fromMonth)
        //                 && (toMonth == null || pd.Child.DOB.Month <= toMonth)
        //                 && (fromYear == null || pd.Child.DOB.Year >= fromYear)
        //                 && (toYear == null || pd.Child.DOB.Year <= toYear)
        //         )
        //         .ToList<Child>();

        //     if (patients.Count == 0)
        //     {
        //         return StatusCode(500, "Not Found .Add more details");
        //     }

        //     return Ok();
        // }


        
        [HttpGet("search-by-doctor-name")]
        public ActionResult<IEnumerable<Child>> SearchByDoctorName(
            string? doctorName = null,
            string? name = null,
            string? city = null,
            int? gender = null,
            int? fromDay = null,
            int? toDay = null,
            int? fromMonth = null,
            int? toMonth = null,
            int? fromYear = null,
            int? toYear = null
        )
        {
            var query = _db.Childs
                .Join(
                    _db.Doctors,
                    child => child.DoctorId,
                    doctor => doctor.Id,
                    (child, doctor) => new { Child = child, Doctor = doctor }
                )
                .Where(
                    pd =>
                        (string.IsNullOrEmpty(name) || pd.Child.Name == name)
                        && (string.IsNullOrEmpty(doctorName) || pd.Doctor.Name.Contains(doctorName))
                        && (string.IsNullOrEmpty(city) || pd.Child.City == city)
                        && (gender == null || (int)pd.Child.Gender == gender)
                        && (fromDay == null || pd.Child.DOB.Day >= fromDay)
                        && (toDay == null || pd.Child.DOB.Day <= toDay)
                        && (fromMonth == null || pd.Child.DOB.Month >= fromMonth)
                        && (toMonth == null || pd.Child.DOB.Month <= toMonth)
                        && (fromYear == null || pd.Child.DOB.Year >= fromYear)
                        && (toYear == null || pd.Child.DOB.Year <= toYear)
                )
                .ToList();

            if (query.Count == 0)
            {
                return StatusCode(404, "No matching records found. Please add more details.");
            }

            return Ok(query.Select(pd => pd.Child));
        }

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

        // [HttpPut]
        // public async Task<IActionResult> PutAsync(
        //     [FromRoute] int id,
        //     [FromBody] Child childToUpdate
        // )
        // {
        //     try
        //     {
        //         if (id != childToUpdate.Id)
        //             return BadRequest();
        //         var dbchild = await _db.Childs.FindAsync(id);
        //         if (dbchild == null)
        //             return NotFound();

        //         _db.Childs.Update(childToUpdate);
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        // [Route("{id}")]
        // [HttpDelete]
        // public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        // {
        //     try
        //     {
        //         var childToDelete = await _db.Childs.FindAsync(id);
        //         if (childToDelete == null)
        //         {
        //             return NotFound();
        //         }
        //         _db.Childs.Remove(childToDelete);
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(
            [FromRoute] long id,
            [FromBody] JsonPatchDocument<Child> patchDocument
        )
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

        [HttpGet("count")]
        public async Task<IActionResult> GetChildCount(long doctorId)
        {
            int count = await _db.Childs.CountAsync(c => c.DoctorId == doctorId);
            return Ok(count);
        }

        [HttpGet("download")]
        public IActionResult DownloadCsv()
        {
            try
            {
                // Retrieve all child data from the database
                List<Child> children = _db.Childs.ToList();

                if (children.Count == 0)
                {
                    return NotFound("No child data found.");
                }

                // Generate the CSV content
                StringBuilder csvContent = new StringBuilder();
                csvContent.AppendLine(
                    "Id,Name,FatherName,Guardian,DOB,Gender,Email,Type,City,CNIC,IsEPIDone,IsVerified,IsInactive,ClinicId,DoctorId"
                );
                foreach (Child child in children)
                {
                    csvContent.AppendLine(
                        $"{child.Id},{child.Name},{child.FatherName},{child.DOB},{child.Gender},{child.Email},{child.City},{child.CNIC},{child.IsEPIDone},{child.IsVerified},{child.IsInactive},{child.ClinicId},{child.DoctorId}"
                    );
                }

                // Set the response headers for CSV file download
                byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent.ToString());
                MemoryStream memoryStream = new MemoryStream(csvBytes);
                string fileName = "children.csv";
                string contentType = "text/csv";

                // Return the CSV file as a download
                return File(memoryStream, contentType, fileName);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while generating the CSV file.");
            }
        }

        [HttpGet("/patients_get_by_doctor_id")]
        public IActionResult GetChildrenByDoctorId(long doctorId)
        {
            try
            {
                var children = _db.Childs.Where(c => c.DoctorId == doctorId).ToList();

                if (children.Count == 0)
                {
                    return NotFound();
                }

                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
