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
using AutoMapper;
namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;

        public ChildController(VaccineDBContext vaccineDBContext,IMapper mapper)
        {
            _db = vaccineDBContext;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] long id)
        {
            try
            {
                var child = await _db.Childs.FindAsync(id);
                if (child == null)
                    return NotFound();

                var childDTO = _mapper.Map<ChildDTO>(child);
                return Ok(childDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("search-by-doctor-name")]
        public ActionResult<IEnumerable<ChildDTO>> SearchByDoctorName(
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
                return StatusCode(404, "No records found.");
            }
            var childDTOs = _mapper.Map<List<ChildDTO>>(query.Select(pd => pd.Child));
            return Ok(childDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> PostNew([FromBody] ChildDTO childDTO)
        {
            try
            {
                 var childEntity = _mapper.Map<Child>(childDTO);

                 _db.Childs.Add(childEntity);
               
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
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
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

  

        [HttpGet]
        [Route("allpatients")]
        public async Task<IActionResult> GetAllc()
        {
            try
            {
                var patients = await _db.Childs.ToListAsync();
                var patientDTOs = _mapper.Map<List<ChildDTO>>(patients);

                return Ok(patientDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("patients_get_by_doctor_id")]
        public IActionResult GetChildrenByDoctorId(long doctorId)
        {
            try
            {
                var children = _db.Childs.Where(c => c.DoctorId == doctorId).ToList();

                if (children.Count == 0)
                {
                    return NotFound();
                }
                var patientDTOs = _mapper.Map<List<ChildDTO>>(children);

                return Ok(patientDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("pagination/children")]
        public IActionResult GetChildren(int page = 1, int perPage = 20)
        {
            int startIndex = (page - 1) * perPage;
            int endIndex = startIndex + perPage;

            var childrenFromDb = _db.Childs.Skip(startIndex).Take(perPage).ToList();

            // Map children data from Entity to DTO
            var patientDTOs = _mapper.Map<List<ChildDTO>>(childrenFromDb);

            return Ok(patientDTOs);
        }
    }
}
