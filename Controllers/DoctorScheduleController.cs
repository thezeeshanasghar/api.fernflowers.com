using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
using AutoMapper;


namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;


        public DoctorScheduleController(VaccineDBContext vaccineDBContext, IMapper mapper)
        {
            _db = vaccineDBContext;
              _mapper = mapper;
        }

        [Route("single_updateDate")]
        [HttpPatch]
        public async Task<IActionResult> Update(long doseId,long doctorId,[FromBody] DoctorScheduleDTO ds)
        {
            try{
                
                var dbDoc = await _db.DoctorSchedules.FirstOrDefaultAsync(d => d.DoctorId == ds.DoctorId && d.DoseId==ds.DoseId); 
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
        [Route("doctor_bulk_update_Date")]
        [HttpPatch]
        public async Task<IActionResult> UpdateBulkDate(long DoctorId,string oldDate, string newDate)
        {
            try
            {
                var parsedOldDate = System.DateOnly.Parse(oldDate);
                var parsedNewDate = System.DateOnly.Parse(newDate);

                var db = await _db.DoctorSchedules
                    .Where(d =>d.DoctorId==DoctorId && d.Date.Equals(parsedOldDate))
                    .ToListAsync();

                if (db == null || db.Count == 0)
                {
                    return NotFound();
                }

                foreach (var doctorSchedule in db)
                {
                    doctorSchedule.Date = parsedNewDate;
            
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("/update_date_for_Vacations")]
        public async Task<IActionResult> UpdateDoseDates(long doctorId, [FromQuery] string fromDate, [FromQuery] string toDate)
        {
            try
            {
                var parsedFromDate = System.DateOnly.Parse(fromDate);
                var parsedToDate = System.DateOnly.Parse(toDate);

                var dosesToUpdate = _db.DoctorSchedules
                    .Where(ds => ds.Date >= parsedFromDate && ds.Date <= parsedToDate && ds.DoctorId == doctorId)
                    .ToList();

                var updatedDate = parsedToDate.AddDays(1);

                foreach (var dose in dosesToUpdate)
                {
                    dose.Date = updatedDate;
                }

                await _db.SaveChangesAsync();
                return Ok("Dose Dates Updated for vacations");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpGet]
        //[Route("Doctor_DoseSchedule")]
        //public async Task<IActionResult> GetNew(long doctorId)
        //{
        //    try
        //    {
        //        Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();

        //        Check if the DoctorSchedules table already exists.
        //        if (!_db.DoctorSchedules.Any(d => d.DoctorId == doctorId))
        //        {
        //            If not, get the data from the AdminSchedules table and save it in the DoctorSchedules table.
        //           var adminSchedules = await _db.AdminSchedules.ToListAsync();

        //            foreach (var adminSchedule in adminSchedules)
        //            {
        //                var newDate = (adminSchedule.Date);
        //                var dose = await _db.Doses.FindAsync(adminSchedule.DoseId);
        //                var dto = _mapper.Map<DoseDTO>(dose);

        //                if (dict.ContainsKey(newDate))
        //                    dict[newDate].Add(dto);
        //                else
        //                    dict.Add(newDate, new List<DoseDTO>() { dto });

        //                Save the DoctorSchedule record.
        //                var doctorSchedule = _mapper.Map<DoctorSchedule>(new DoctorScheduleDTO
        //                {
        //                    Date = adminSchedule.Date,
        //                    DoseId = adminSchedule.DoseId,
        //                    DoctorId = doctorId
        //                });

        //                _db.DoctorSchedules.Add(doctorSchedule);
        //            }
        //            foreach (var adminSchedule in adminSchedules)
        //            {
        //                var newDate = adminSchedule.Date;
        //                Save the DoctorSchedule record if it doesn't already exist for the doctorId.
        //                if (!_db.DoctorSchedules.Any(d => d.DoctorId == doctorId && d.Date == newDate && d.DoseId == adminSchedule.DoseId))
        //                {
        //                    var doctorSchedule = _mapper.Map<DoctorSchedule>(new DoctorScheduleDTO
        //                    {
        //                        Date = newDate,
        //                        DoseId = adminSchedule.DoseId,
        //                        DoctorId = doctorId
        //                    });
        //                    _db.DoctorSchedules.Add(doctorSchedule);
        //                }
        //            }
        //            await _db.SaveChangesAsync();
        //        }
        //        var doctorSchedules = await _db.DoctorSchedules.Include(schedule => schedule.Dose).Where(d => d.DoctorId == doctorId).ToListAsync();
        //        foreach (var doctorSchedule in doctorSchedules)
        //        {
        //            var doseDTO = _mapper.Map<DoseDTO>(doctorSchedule.Dose);

        //            if (dict.ContainsKey(doctorSchedule.Date))
        //                dict[doctorSchedule.Date].Add(doseDTO);
        //            else
        //                dict.Add(doctorSchedule.Date, new List<DoseDTO>() { doseDTO });
        //        }

        //        var sortedDict = dict.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        //        return Ok(sortedDict);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpGet]
        //[Route("Doctor_DoseSchedule")]
        //public async Task<IActionResult> GetNew(long doctorId)
        //{
        //    try
        //    {
        //        Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();

        //        Check if the DoctorSchedules table already exists.
        //        if (_db.DoctorSchedules.Any(d => d.DoctorId == doctorId))
        //        {
        //            Fetch and organize data from the DoctorSchedules table for the specified doctorId.

        //           var doctorSchedules = await _db.DoctorSchedules.Include(schedule => schedule.Dose).Where(d => d.DoctorId == doctorId).ToListAsync();
        //            foreach (var doctorSchedule in doctorSchedules)
        //            {
        //                var doseDTO = _mapper.Map<DoseDTO>(doctorSchedule.Dose);

        //                if (dict.ContainsKey(doctorSchedule.Date))
        //                    dict[doctorSchedule.Date].Add(doseDTO);
        //                else
        //                    dict.Add(doctorSchedule.Date, new List<DoseDTO>() { doseDTO });
        //            }
        //        }

        //        Fetch new lines from AdminSchedules table and add them to DoctorSchedules.
        //        var newAdminSchedules = await _db.AdminSchedules
        //            .Where(adminSchedule => !_db.DoctorSchedules.Any(ds => ds.DoctorId == doctorId && ds.Date == adminSchedule.Date && ds.DoseId == adminSchedule.DoseId))
        //            .ToListAsync();

        //        foreach (var newAdminSchedule in newAdminSchedules)
        //        {
        //            Add a new entry to DoctorSchedules.
        //            var doctorSchedule = _mapper.Map<DoctorSchedule>(new DoctorScheduleDTO
        //            {
        //                Date = newAdminSchedule.Date,
        //                DoseId = newAdminSchedule.DoseId,
        //                DoctorId = doctorId
        //            });
        //            _db.DoctorSchedules.Add(doctorSchedule);

        //            Update the dictionary with the new entry.
        //           var newDate = newAdminSchedule.Date;
        //            var dose = await _db.Doses.FindAsync(newAdminSchedule.DoseId);
        //            var dto = _mapper.Map<DoseDTO>(dose);

        //            if (dict.ContainsKey(newDate))
        //                dict[newDate].Add(dto);
        //            else
        //                dict.Add(newDate, new List<DoseDTO>() { dto });
        //        }

        //        await _db.SaveChangesAsync();

        //        Sort the dictionary by date.
        //       var sortedDict = dict.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        //        return Ok(sortedDict);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        [HttpGet]
        [Route("Doctor_DoseSchedule")]
        public async Task<IActionResult> GetNew(long doctorId)
        {
            try
            {
                Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();

                // Fetch and organize data from the DoctorSchedules table for the specified doctorId.
                var doctorSchedules = await _db.DoctorSchedules
                    .Include(schedule => schedule.Dose)
                    .Where(d => d.DoctorId == doctorId)
                    .ToListAsync();

                foreach (var doctorSchedule in doctorSchedules)
                {
                    var doseDTO = _mapper.Map<DoseDTO>(doctorSchedule.Dose);

                    if (dict.ContainsKey(doctorSchedule.Date))
                        dict[doctorSchedule.Date].Add(doseDTO);
                    else
                        dict.Add(doctorSchedule.Date, new List<DoseDTO> { doseDTO });
                }

                // Fetch new lines from AdminSchedules table and add them to DoctorSchedules.
                var newAdminSchedules = await _db.AdminSchedules
                    .Where(adminSchedule => !_db.DoctorSchedules.Any(ds => ds.DoctorId == doctorId && ds.Date == adminSchedule.Date && ds.DoseId == adminSchedule.DoseId))
                    .ToListAsync();

                foreach (var newAdminSchedule in newAdminSchedules)
                {
                    // Create a new entry for DoctorSchedules.
                    var doctorSchedule = new DoctorSchedule
                    {
                        Date = newAdminSchedule.Date,
                        DoseId = newAdminSchedule.DoseId,
                        DoctorId = doctorId
                    };

                    // Add the new entry to DoctorSchedules.
                    _db.DoctorSchedules.Add(doctorSchedule);

                    // Update the dictionary with the new entry.
                    var newDate = newAdminSchedule.Date;
                    var dose = await _db.Doses.FindAsync(newAdminSchedule.DoseId);
                    var dto = _mapper.Map<DoseDTO>(dose);

                    if (dict.ContainsKey(newDate))
                        dict[newDate].Add(dto);
                    else
                        dict.Add(newDate, new List<DoseDTO> { dto });
                }

                // Save changes to the database.
                await _db.SaveChangesAsync();

                // Sort the dictionary by date.
                var sortedDict = dict.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                return Ok(sortedDict);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}