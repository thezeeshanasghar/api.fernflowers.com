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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace api.fernflowers.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientScheduleController : ControllerBase
    {
        private readonly VaccineDBContext _db;
        private readonly IMapper _mapper;

        public PatientScheduleController(VaccineDBContext vaccineDBContext,IMapper mapper)
        {
            _db = vaccineDBContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Patient_DoseSchedule")]
        public async Task<IActionResult> GetNew(long ChildId, long DoctorId)
        {
            try
            {
                var child = await _db.Childs.FindAsync(ChildId);

                if (child == null)
                {
                    return NotFound();
                }

                Dictionary<DateOnly, List<PatientDoseScheduleDTO>> dict = new Dictionary<DateOnly, List<PatientDoseScheduleDTO>>();

                if (!_db.PatientSchedules.Any(d => d.ChildId == ChildId && d.DoctorId == DoctorId))
                {
                    var doctorSchedules = _db.DoctorSchedules.Where(d => d.DoctorId == DoctorId).OrderBy(d => d.Date).ToList();

                    DateOnly childDOB = child.DOB; // Example date of birth for the child

                    DateOnly oldDate = default; // Variable to store the previous date
                    DateOnly updateDate = default; // Variable to store the updated date

                    foreach (var schedule in doctorSchedules)
                    {
                        var pdate = schedule.Date; // Store the current schedule's date in pdate

                        if (oldDate == default)
                        {
                            oldDate = pdate;
                            updateDate = childDOB; // Replace the first date with the child's date of birth
                        }
                        else
                        {
                            if (pdate != oldDate)
                            {
                                var gap = (int)(pdate.DayNumber - oldDate.DayNumber); // Calculate the gap between oldDate and pdate
                                updateDate = updateDate.AddDays(gap); // Add the gap to the updateDate
                            }
                        }

                        var newDate = updateDate;

                        var dose = await _db.Doses.FindAsync(schedule.DoseId);
                        if (dose == null)
                        {
                            // Dose not found, set the properties to null
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = 0, // Set to 0 as it will be generated when saved
                                DoseName = null,
                                IsSkip = false,
                                IsDone = false,
                                BrandName = null
                            };

                            if (dict.ContainsKey(newDate))
                                dict[newDate].Add(dto);
                            else
                                dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });

                            // Save the DoctorSchedule record with DoseId set to null (optional)
                            var patientSchedule = new PatientSchedule
                            {
                                Date = newDate,
                                DoseId = null, // Set DoseId to null or remove this line if you prefer
                                DoctorId = DoctorId,
                                ChildId = ChildId,
                                IsDone = false,
                                BrandId = null
                            };
                            _db.PatientSchedules.Add(patientSchedule);
                            await _db.SaveChangesAsync();

                            dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                            oldDate = pdate; // Update the oldDate for the next iteration
                        }
                        else
                        {
                            // Dose found, continue with the existing code to create the dto
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = 0, // Set to 0 as it will be generated when saved
                                DoseName = dose.Name,
                                IsSkip = false,
                                IsDone = false,
                                // BrandName = brand.Name (you can remove this line since we don't need it when dose is deleted)
                            };

                            if (dict.ContainsKey(newDate))
                                dict[newDate].Add(dto);
                            else
                                dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });

                            // Save the DoctorSchedule record.
                            var patientSchedule = new PatientSchedule
                            {
                                Date = newDate,
                                DoseId = schedule.DoseId,
                                DoctorId = DoctorId,
                                ChildId = ChildId,
                                IsDone = false,
                                BrandId = null
                            };
                            _db.PatientSchedules.Add(patientSchedule);
                            await _db.SaveChangesAsync();

                            dto.ScheduleId = patientSchedule.Id; // Assign the generated Id to the DTO

                            oldDate = pdate; // Update the oldDate for the next iteration
                        }
                    }
                }
                    var patientSchedules = await _db.PatientSchedules.Where(d => d.DoctorId == DoctorId && d.ChildId == ChildId).ToListAsync();

                    foreach (var patientSchedule in patientSchedules)
                    {
                        var newDate = patientSchedule.Date;
                        var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);
                        var brand = await _db.Brands.FindAsync(patientSchedule.BrandId);

                        if (dose == null || brand == null)
                        {
                            // Dose or Brand not found, set the properties to null
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose?.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone,
                                BrandName = brand?.Name
                            };

                            if (dict.ContainsKey(newDate))
                                dict[newDate].Add(dto);
                            else
                                dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                        }
                        else
                        {
                            // Dose and Brand found, continue with the existing code to create the dto
                            var dto = new PatientDoseScheduleDTO
                            {
                                ScheduleId = patientSchedule.Id,
                                DoseName = dose.Name,
                                IsSkip = patientSchedule.IsSkip,
                                IsDone = patientSchedule.IsDone,
                                BrandName = brand.Name
                            };

                            if (dict.ContainsKey(newDate))
                                dict[newDate].Add(dto);
                            else
                                dict.Add(newDate, new List<PatientDoseScheduleDTO> { dto });
                        }
                    }
                

               var sortedDict = dict.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                return Ok(sortedDict);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Route("single_updateDate")]
        [HttpPatch]
        public async Task<IActionResult> Update(long Id,[FromBody] PatientScheduleDTO ps)
        {
            try
            {
                var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.Id == ps.Id);
                if (dbps == null)
                {
                    return NotFound();
                }

                dbps.Date = ps.Date;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }   

        [Route("single_updateDone")]
        [HttpPatch]
        public async Task<IActionResult> UpdateDone(long Id,[FromBody] PatientScheduleDTO ps)
        {
            try
            {
                var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.Id==ps.Id);
                if (dbps == null)
                {
                    return NotFound();
                }

                dbps.IsDone = ps.IsDone;
                dbps.BrandId = ps.BrandId; 
                dbps.Date= ps.Date;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetBrandForPatientSchedule")]
        public async Task<IActionResult> GetBrandForPatientSchedule(long Id)
        {
            try
            {
                // Find the patient schedule by its ID
                var patientSchedule = await _db.PatientSchedules.FindAsync(Id);

                if (patientSchedule == null)
                {
                    return NotFound("Patient schedule not found");
                }

                // Find the dose by its ID
                var dose = await _db.Doses.FindAsync(patientSchedule.DoseId);

                if (dose == null)
                {
                    return NotFound("Dose not found");
                }

                // Get all brands that are associated with the given doseId
                var brandsForDose = await _db.Brands
                    .Where(brand => brand.VaccineId == dose.VaccineId)
                    .ToListAsync();

                return Ok(brandsForDose);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("single_update_Skip")]
        [HttpPatch]
        public async Task<IActionResult> UpdateSkip(long Id,[FromBody] PatientScheduleDTO ps)
        {
            try
            {
               var dbps = await _db.PatientSchedules
                    .FirstOrDefaultAsync(d => d.Id==ps.Id);
                if (dbps == null)
                {
                    return NotFound();
                }

                dbps.IsSkip = ps.IsSkip;
                _db.Entry(dbps).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("patient_bulk_updateDone")]
        [HttpPatch()]
        public async Task<IActionResult> UpdateBrandIdAndDate([FromBody] List<PatientScheduleUpdateDTO> updateData)
        {
            try
            {
                foreach (var updateItem in updateData)
                {
                    var parsedCurrentDate = System.DateOnly.Parse(updateItem.CurrentDate);
                    var parsedNewDate = System.DateOnly.Parse(updateItem.NewDate);

                    var dbPS = await _db.PatientSchedules
                        .Where(d => d.Id == updateItem.Id && d.Date.Equals(parsedCurrentDate))
                        .ToListAsync();

                    if (dbPS == null || dbPS.Count == 0)
                    {
                        return NotFound();
                    }

                    // Update BrandId for each record
                    foreach (var record in dbPS)
                    {
                        record.IsDone = updateItem.IsDone;
                        record.Date = parsedNewDate;
                        record.BrandId = updateItem.BrandId; // Use the BrandId from the updateItem
                    }
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Route("patient_bulk_update_IsSkip")]
        [HttpPatch()]
        public async Task<IActionResult> UpdateIsSkip(long childId,string date, bool IsSkip)
        {
            try
            {
                var parsedFromDate = System.DateOnly.Parse(date);
                var dbPS = await _db.PatientSchedules
                    .Where(d => d.ChildId == childId && d.Date.Equals(parsedFromDate))
                    .ToListAsync();

                if (dbPS == null || dbPS.Count == 0)
                {
                    return NotFound();
                }

                foreach (var patientSchedule in dbPS)
                {
                    patientSchedule.IsSkip = IsSkip;
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("getData_baseOnDate")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientScheduleDTO>>> GetPatientSchedules(string date)
        {
            // Ensure the date includes only the date part without the time
            var parsedDate = System.DateOnly.Parse(date);

            var patientSchedules = await _db.PatientSchedules
                .Where(ps => ps.Date == parsedDate)
                .Select(ps => new PatientScheduleDTO
                {
                    Id=ps.Id,
                    Date = ps.Date,
                    DoseId = ps.DoseId.HasValue ? ps.DoseId.Value : 0,
                    DoctorId = ps.DoctorId,
                    ChildId = ps.ChildId,
                    IsSkip = ps.IsSkip,
                    IsDone = ps.IsDone,
                    BrandId = ps.BrandId
                })
                .ToListAsync();

            return Ok(patientSchedules);
        }

            
        [Route("patient_bulk_update_Date")]
        [HttpPatch]
        public async Task<IActionResult> UpdateBulkDate(long ChildId,long DoctorId,string oldDate, string newDate)
        {
            try
            {
                var parsedOldDate = System.DateOnly.Parse(oldDate);
                var parsedNewDate = System.DateOnly.Parse(newDate);

                var db = await _db.PatientSchedules
                    .Where(d =>d.ChildId==ChildId && d.DoctorId==DoctorId && d.Date.Equals(parsedOldDate))
                    .ToListAsync();

                if (db == null || db.Count == 0)
                {
                    return NotFound();
                }

                foreach (var Schedule in db)
                {
                    Schedule.Date = parsedNewDate;
            
                }

                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("today_alert")]
        public ActionResult<IEnumerable<Child>> GetPatientsWithTodayDate()
        {
            try
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                var childIds = _db.PatientSchedules
                    .Where(p => p.Date == today)
                    .Select(p => p.ChildId)
                    .Distinct()
                    .ToList();

                var children = _db.Childs.Where(c => childIds.Contains(c.Id)).ToList();

                if (children == null || children.Count == 0)
                    return Ok("no one visiting today");

                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("today_alert_BaseOnDoctor")]
        public ActionResult<IEnumerable<Child>> GetPatientsWithTodayDateAndDoctor(long doctorId)
        {
            try
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                
                // Filter patients by date and doctor ID
                var childIds = _db.PatientSchedules
                    .Where(p => p.Date == today && p.DoctorId == doctorId)
                    .Select(p => p.ChildId)
                    .Distinct()
                    .ToList();

                var children = _db.Childs.Where(c => childIds.Contains(c.Id)).ToList();

                if (children == null || children.Count == 0)
                    return Ok("No patients visiting today for the given doctor.");

                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("pdf")]
        [HttpGet]
        public IActionResult GetJoinedData(long ChildId)
        {
            var query = from child in _db.Childs
                        join doctor in _db.Doctors on child.DoctorId equals doctor.Id
                        join clinic in _db.Clinics on child.ClinicId equals clinic.Id

                
                        where child.Id == ChildId
                        select new
                        {
                            ChildId = child.Id,
                            ChildName = child.Name,
                            ChildFatherName=child.FatherName,
                            ChildMobileNumber=child.MobileNumber,
                            Gender=child.Gender,
                            DoctorId = doctor.Id,
                            DoctorName = doctor.Name,
                            DoctorEmail = doctor.Email,
                            DoctorMobileNumber = doctor.MobileNumber,
                            ClinicName = clinic.Name,
                            ClinicAddress=clinic.Address,
                            ClinicNumber=clinic.Number,
                            ClinicCity=clinic.City,
                        
                        };
            var result = query.FirstOrDefault();
            var query2 = from schedule in _db.PatientSchedules
                    join dose in _db.Doses on schedule.DoseId equals dose.Id
                    join vaccine in _db.Vaccines on dose.VaccineId equals vaccine.Id
                    join brand in _db.Brands on schedule.BrandId equals brand.Id into brandGroup
                         from brand in brandGroup.DefaultIfEmpty() // Perform left outer join

                         where schedule.ChildId == ChildId
                    select new
                    {
                        schedule.Id,
                        schedule.ChildId,
                        Vaccine=vaccine.Name,
                        DoseName = dose.Name,
                        schedule.Date,
                        schedule.IsSkip,
                        schedule.IsDone,
                        BrandName = (schedule.BrandId == null) ? null : brand.Name

                    };

            var result2 = query2.OrderBy(item => item.Date).ToList();

            if (result == null)
            {
                return NotFound();
            }

        
            string doctorName =result.DoctorName;
            string DoctorEmail = result.DoctorEmail;
            string DoctorMobileNumber = result.DoctorMobileNumber;
            string ClinicName = result.ClinicName;
            string ClinicAddress = result.ClinicAddress;
            string ClinicNumber = result.ClinicNumber;
            string ClinicCity = result.ClinicCity;
            string ChildName = result.ChildName;
            string ChildFatherName = result.ChildFatherName;
            string ChildMobileNumber=result.ChildMobileNumber;

            // Create the PDF document
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 45, 45, 30, 30);
                var writer = PdfWriter.GetInstance(document, memoryStream);
                writer.CloseStream = false;
                PDFFooter footer = new PDFFooter("NOTE: 1. Vaccines may cause fever, localised redness and pain. 2. This schedule is valid to produce on demand at all airports, embassies and schools of the world. 3. We always use the best available vaccine brand/manufacturer. With time and continuous research vaccine brand can be different for future doses. Disclaimer: This schedule provides recommended dates for immunisations for individual based date of birth, past history of immunisation and disease. Your consultant may update the due dates or add/remove vaccines. Vaccine.pk, its management or staff holds no responsibility for any loss or damage due to any vaccine given. *OHF = vaccine given at other health faculty (not by vaccine.pk) ");
                writer.PageEvent = footer;
                FontFactory.RegisterDirectories();
                document.Open();
                PdfPTable table = new PdfPTable(1);
                table.WidthPercentage = 100;

                PdfPCell DoctorNamecell = new PdfPCell(new Phrase(doctorName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                DoctorNamecell.Border = Rectangle.NO_BORDER;
                DoctorNamecell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(DoctorNamecell);


                PdfPCell doctorEmailCell = new PdfPCell(new Phrase(DoctorEmail, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                doctorEmailCell.Border = Rectangle.NO_BORDER;
                doctorEmailCell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(doctorEmailCell);

                PdfPCell doctorMobileNumberCell = new PdfPCell(new Phrase(DoctorMobileNumber, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                doctorMobileNumberCell.Border = Rectangle.NO_BORDER;
                doctorMobileNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(doctorMobileNumberCell);

                PdfPCell lineBreakCell = new PdfPCell(new Phrase("\n"));
                lineBreakCell.Border = Rectangle.NO_BORDER;
                table.AddCell(lineBreakCell);


                string imagePath = "./Resource/cliniclogo.png";
                Image logoImage = Image.GetInstance(imagePath);

                // Set the position and size of the image
                float imageWidth = 160f; // Adjust the width of the image as needed
                float imageHeight = 50f; // Adjust the height of the image as needed
                float imageX = document.PageSize.Width - document.RightMargin - imageWidth;
                float imageY = document.PageSize.Height - document.TopMargin - imageHeight;

                // Add the image to the PDF document
                logoImage.SetAbsolutePosition(imageX, imageY);
                logoImage.ScaleToFit(imageWidth, imageHeight);
                writer.DirectContent.AddImage(logoImage);


                PdfPTable mainTable = new PdfPTable(2);
                mainTable.WidthPercentage = 100;

                PdfPTable clinicTable = new PdfPTable(1);
                clinicTable.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell ClinicNameCell = new PdfPCell(new Phrase(ClinicName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                ClinicNameCell.Border = Rectangle.NO_BORDER;
                ClinicNameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                clinicTable.AddCell(ClinicNameCell);

            
                PdfPCell ClinicAddressCell = new PdfPCell(new Phrase(ClinicAddress, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                ClinicAddressCell.Border = Rectangle.NO_BORDER;
                ClinicAddressCell.HorizontalAlignment = Element.ALIGN_LEFT;
                clinicTable.AddCell(ClinicAddressCell);


                PdfPCell ClinicNumberCell = new PdfPCell(new Phrase("Phone: " +ClinicNumber, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                ClinicNumberCell.Border = Rectangle.NO_BORDER;
                ClinicNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
                clinicTable.AddCell(ClinicNumberCell);

                PdfPCell ClinicCityCell = new PdfPCell(new Phrase("City: " +ClinicCity, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                ClinicCityCell.Border = Rectangle.NO_BORDER;
                ClinicCityCell.HorizontalAlignment = Element.ALIGN_LEFT;
                clinicTable.AddCell(ClinicCityCell);
                

                PdfPCell clinicCell = new PdfPCell(clinicTable);
                clinicCell.Border = Rectangle.NO_BORDER;
                mainTable.AddCell(clinicCell);



                
                PdfPTable childTable = new PdfPTable(1);
                childTable.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell gapCell = new PdfPCell();
                gapCell.FixedHeight = 10f; // Adjust the height of the gap as needed
                gapCell.Border = Rectangle.NO_BORDER;
                childTable.AddCell(gapCell);

                PdfPCell childNameCell = new PdfPCell(new Phrase(ChildName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                childNameCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                childNameCell.Border = Rectangle.NO_BORDER;
                childTable.AddCell(childNameCell);
                

                string genderText = result.Gender == Gender.Boy ? "s/o " : "d/o ";
                string childWithFatherName = genderText + ChildFatherName;
                PdfPCell ChildFatherNameCell = new PdfPCell(new Phrase(childWithFatherName, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                ChildFatherNameCell.Border = Rectangle.NO_BORDER;
                ChildFatherNameCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                childTable.AddCell(ChildFatherNameCell);


                PdfPCell ChildMobileNumberCell = new PdfPCell(new Phrase(ChildMobileNumber, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                ChildMobileNumberCell.Border = Rectangle.NO_BORDER;
                ChildMobileNumberCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                childTable.AddCell(ChildMobileNumberCell);

                PdfPCell childCell = new PdfPCell(childTable);
                childCell.Border = Rectangle.NO_BORDER;
                mainTable.AddCell(childCell);



                document.Add(table);
                document.Add(mainTable);

                Paragraph title = new Paragraph("IMMUNIZATION RECORD");
                title.Font =
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                float[] widths = new float[] { 20f, 90f, 50f,90f, 70 };
                PdfPTable table_Center = new PdfPTable(5);
                // table_Center.HorizontalAlignment = Element.ALIGN_LEFT;
                table_Center.TotalWidth = 510f;
                table_Center.LockedWidth = true;
                table_Center.HorizontalAlignment = 0;
                table_Center.SpacingBefore = 5;
                table_Center.SetWidths(widths);

                // Add table headers
                table_Center.AddCell(new PdfPCell(new Phrase("Sr")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
                table_Center.AddCell(new PdfPCell(new Phrase("Vaccine")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
                table_Center.AddCell(new PdfPCell(new Phrase("Status")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
                table_Center.AddCell(new PdfPCell(new Phrase("Date")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
                table_Center.AddCell(new PdfPCell(new Phrase("Brand")) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });


                int counter = 1;
                foreach (var schedule in result2)
                {
                    table_Center.AddCell(new PdfPCell(new Phrase(counter.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8))) { HorizontalAlignment = Element.ALIGN_CENTER , MinimumHeight = 10f, PaddingBottom = 2f });
                    table_Center.AddCell(new PdfPCell(new Phrase(schedule.DoseName, FontFactory.GetFont(FontFactory.HELVETICA, 8))) { HorizontalAlignment = Element.ALIGN_CENTER , MinimumHeight = 10f, PaddingBottom = 2f });
                    PdfPCell statusCell;
                    if (schedule.IsDone == true)
                    {
                        // Status is "Given" and formatted in bold
                        statusCell = new PdfPCell(new Phrase("Given", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8)));
                    }
                    else if (schedule.IsSkip==true)
                    {
                        // Status is empty or any other value
                        statusCell = new PdfPCell(new Phrase("Missed", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    }
                    else
                    {
                        statusCell = new PdfPCell(new Phrase("Due", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    }

                    statusCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table_Center.AddCell(statusCell);
                    table_Center.AddCell(new PdfPCell(new Phrase(schedule.Date.ToString("d"), FontFactory.GetFont(FontFactory.HELVETICA, 8)))  { HorizontalAlignment = Element.ALIGN_CENTER, MinimumHeight = 7f, PaddingBottom = 2f });

                    PdfPCell BrandCell;
                    if (schedule.IsDone == true)
                    {
                    // Status is "Given" and formatted in bold
                    BrandCell = new PdfPCell(new Phrase(schedule.BrandName, FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    }
                    else 
                    {
                    // Status is empty or any other value
                    BrandCell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    }

                    BrandCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table_Center.AddCell(BrandCell);
                    
                    
            

                    counter++;
                }

                document.Add(table_Center);

                document.Close();
            

                // Set the response headers
                Response.Headers["Content-Type"] = "application/pdf";
                Response.Headers["Content-Disposition"] = "attachment; filename=joined_data.pdf";

                // Write the PDF file to the response
                return File(memoryStream.ToArray(), "application/pdf");
            }
        }

        public class PDFFooter : PdfPageEventHelper
        {
            private string footerText;

            public PDFFooter(string footerText)
            {
                this.footerText = footerText;
            }


            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                string footer = footerText + " Printed on: "  + DateTime.UtcNow.AddHours(5).ToString("MMMM dd, yyyy");

                footer =
                    footer
                        .Replace(Environment.NewLine, String.Empty)
                        .Replace("  ", String.Empty);
                Font georgia = FontFactory.GetFont("georgia", 6f);

                PdfPCell cell = new PdfPCell(new Phrase(footer, georgia));
                cell.Border = 0;
                

                PdfPTable tabFot = new PdfPTable(1);
                tabFot.SetTotalWidth(new float[] { 510f });
                tabFot.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            
                cell.PaddingTop = 5;
                
                tabFot.AddCell(cell);
                
            
                tabFot.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent); 
                // PdfContentByte canvas = writer.DirectContent;
                // Phrase footerPhrase = new Phrase(footer, georgia);

                // float x = document.LeftMargin;
                // float y = document.BottomMargin - 15; // Adjust the value as needed
                // float width = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                // float height = 70; // Adjust the value as needed for the height of the text

                // Rectangle rect = new Rectangle(x, y, x + width, y + height);
                // ColumnText columnText = new ColumnText(canvas);
                // columnText.SetSimpleColumn(rect);
                // columnText.Alignment = Element.ALIGN_LEFT;
                // columnText.AddElement(footerPhrase);
                // columnText.UseAscender = true;
                // columnText.Go();
            }

        
        }
    }
}
