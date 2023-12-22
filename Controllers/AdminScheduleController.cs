using api.fernflowers.com.Data;
using api.fernflowers.com.Data.Entities;
using api.fernflowers.com.ModelDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Globalization;
using System.Text.RegularExpressions;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminScheduleController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;

        public AdminScheduleController(VaccineDBContext vaccineDBContext, IMapper mapper)
        {
            _db = vaccineDBContext;
            _mapper = mapper;
        }

        [Route("Admin_single_updateDate")]
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] AdminScheduleDTO ds)
        {
            try
            {
                var dbDoc = await _db.AdminSchedules.Where(x=>x.DoseId==ds.DoseId).FirstOrDefaultAsync();
                if (dbDoc == null)
                {
                    return NotFound();
                }
                
                dbDoc.Date = ds.Date;
                _db.Entry(dbDoc).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("admin_bulk_update_Date")]
        [HttpPatch]
        public async Task<IActionResult> UpdateBulkDate(string oldDate, string newDate)
        {
            try
            {
                var parsedOldDate = System.DateOnly.Parse(oldDate);
                var parsedNewDate = System.DateOnly.Parse(newDate);

                var db = await _db.AdminSchedules
                    .Where(d => d.Date.Equals(parsedOldDate))
                    .ToListAsync();

                if (db == null || db.Count == 0)
                {
                    return NotFound();
                }

                foreach (var adminSchedule in db)
                {
                    adminSchedule.Date = parsedNewDate;
            
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [HttpGet]
        // [Route("admin_post_doseSchedule")]
        // public async Task<IActionResult> GetUpdatedSchedule()
        // {
        //     try
        //     {
        //         if (!_db.AdminSchedules.Any())
        //         {
        //             var doses = await _db.Doses.OrderBy(x => x.MinAge).ToListAsync();
        //             var today = DateOnly.FromDateTime(DateTime.Now);

        //             foreach (var dose in doses)
        //             {
        //                 var newDate = today.AddDays(dose.MinAge);
        //                 _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = dose.Id });
        //             }

        //             await _db.SaveChangesAsync();
        //         }

        //         // Generate schedule entries for new doses (outside the loop)
        //         var existingDoseIds = await _db.AdminSchedules.Select(schedule => schedule.DoseId).ToListAsync();
        //         var newDoses = await _db.Doses.Where(dose => !existingDoseIds.Contains(dose.Id)).ToListAsync();

        //         var todayForNewDoses = DateOnly.FromDateTime(DateTime.Now);

        //         foreach (var dose in newDoses)
        //         {
        //             var newDate = todayForNewDoses.AddDays(dose.MinAge);
        //             _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = dose.Id });
        //         }

        //         await _db.SaveChangesAsync();

        //         // Create dictionary to store the schedule
        //         Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();

        //         // Retrieve admin schedules from the database
        //         var adminSchedules = await _db.AdminSchedules.Include(schedule => schedule.Dose).ToListAsync();

        //         // Map admin schedules to DTOs and organize them in the dictionary
        //         foreach (var adminSchedule in adminSchedules)
        //         {
        //             var doseDTO = _mapper.Map<DoseDTO>(adminSchedule.Dose);

        //             if (dict.ContainsKey(adminSchedule.Date))
        //                 dict[adminSchedule.Date].Add(doseDTO);
        //             else
        //                 dict.Add(adminSchedule.Date, new List<DoseDTO> { doseDTO });
        //         }

        //         // Sort the dictionary by date
        //         var sortedDict = dict.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        //         return Ok(sortedDict);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        [HttpGet]
        [Route("admin_post_doseSchedule")]
        public async Task<IActionResult> GetUpdatedSchedule()
        {
            try
            {
                var startingDate = new DateOnly(2023, 1, 1); // Set your desired starting date

                if (!_db.AdminSchedules.Any())
                {
                    var doses = await _db.Doses.OrderBy(x => x.MinAge).ToListAsync();

                    foreach (var dose in doses)
                    {
                        DateOnly newDate;

                        bool containsYear = dose.MinAgeText.ToLower().Contains("year");
                        bool containsMonth = dose.MinAgeText.ToLower().Contains("months");

                        if (containsYear && containsMonth)
                        {
                            // If both "year" and "month" are present, handle accordingly
                            string numericYearPart = new string(dose.MinAgeText
                            .TakeWhile(char.IsDigit)
                            .ToArray());
                            string numericMonthPart = new string(dose.MinAgeText
                                .SkipWhile(c => char.IsDigit(c))
                                .Where(c => char.IsDigit(c) || char.IsWhiteSpace(c))
                                .ToArray());

                            if (int.TryParse(numericYearPart, out int yearValue) &&
                                int.TryParse(numericMonthPart, out int monthValue))
                            {
                                // Add both years and months to the startingDate without changing the day
                                newDate = startingDate.AddYears(yearValue).AddMonths(monthValue);

                                // Print the result to verify
                                
                                _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = dose.Id });
                            }
                        }
                        else if (containsYear)
                        {
                            // If only "year" is present, add years to the startingDate without changing day and month
                            string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                            if (int.TryParse(numericPart, out int minAgeValue))
                            {
                                newDate = startingDate.AddYears(minAgeValue);
                                _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = dose.Id });
                            }
                        }
                        else if (containsMonth)
                        {
                            // If only "month" is present, add months to the startingDate without changing day
                            string numericPart = new string(dose.MinAgeText.Where(char.IsDigit).ToArray());

                            if (int.TryParse(numericPart, out int minAgeValue))
                            {
                                newDate = startingDate.AddMonths(minAgeValue);
                                _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = dose.Id });
                            }
                        }
                        else
                        {
                            // If neither "year" nor "month" is present, add MinAge days to the startingDate
                            newDate = startingDate.AddDays(dose.MinAge);
                            _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = dose.Id });
                        }
                    }

                    await _db.SaveChangesAsync();
                }

                // Generate schedule entries for new doses (outside the loop)
                var existingDoseIds = await _db.AdminSchedules.Select(schedule => schedule.DoseId).ToListAsync();
                var newDoses = await _db.Doses.Where(dose => !existingDoseIds.Contains(dose.Id)).ToListAsync();

                foreach (var newDose in newDoses)
                {


                    DateOnly newDate;
                    

                    bool containsYear = newDose.MinAgeText.ToLower().Contains("year");
                    bool containsMonth = newDose.MinAgeText.ToLower().Contains("months");

                    if (containsYear && containsMonth)
                    {
                        // If both "year" and "month" are present, handle accordingly
                        string numericYearPart = new string(newDose.MinAgeText.Where(char.IsDigit).ToArray());
                        string numericMonthPart = new string(newDose.MinAgeText
                            .SkipWhile(c => char.IsDigit(c))
                            .Where(c => char.IsDigit(c) || char.IsWhiteSpace(c))
                            .ToArray());

                        if (int.TryParse(numericYearPart, out int yearValue) &&
                            int.TryParse(numericMonthPart, out int monthValue))
                        {
                            // Add both years and months to the startingDate without changing the day
                            startingDate = startingDate.AddYears(yearValue).AddMonths(monthValue);

                            // Print the result to verify
                            Console.WriteLine(startingDate); // This should now be 01-07-2033
                            _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = newDose.Id });
                        }
                    }
                    else if (containsYear)
                    {
                        // If only "year" is present, add years to the startingDate without changing day and month
                        string numericPart = new string(newDose.MinAgeText.Where(char.IsDigit).ToArray());

                        if (int.TryParse(numericPart, out int minAgeValue))
                        {
                            newDate = startingDate.AddYears(minAgeValue);
                            _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = newDose.Id });
                        }
                    }
                    else if (containsMonth)
                    {
                        // If only "month" is present, add months to the startingDate without changing day
                        string numericPart = new string(newDose.MinAgeText.Where(char.IsDigit).ToArray());

                        if (int.TryParse(numericPart, out int minAgeValue))
                        {
                            newDate = startingDate.AddMonths(minAgeValue);
                            _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = newDose.Id });
                        }
                    }
                    else
                    {
                        // If neither "year" nor "month" is present, add MinAge days to the startingDate
                        newDate = startingDate.AddDays(newDose.MinAge);
                        _db.AdminSchedules.Add(new AdminSchedule { Date = newDate, DoseId = newDose.Id });
                    }
                }
                        

                await _db.SaveChangesAsync();

                // Create dictionary to store the schedule
                Dictionary<DateOnly, List<DoseDTO>> dict = new Dictionary<DateOnly, List<DoseDTO>>();

                // Retrieve admin schedules from the database
                var adminSchedules = await _db.AdminSchedules.Include(schedule => schedule.Dose).ToListAsync();

                // Map admin schedules to DTOs and organize them in the dictionary
                foreach (var adminSchedule in adminSchedules)
                {
                    var doseDTO = _mapper.Map<DoseDTO>(adminSchedule.Dose);

                    if (dict.ContainsKey(adminSchedule.Date))
                        dict[adminSchedule.Date].Add(doseDTO);
                    else
                        dict.Add(adminSchedule.Date, new List<DoseDTO> { doseDTO });
                }

                // Sort the dictionary by date
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
