using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace api.fernflowers.com.Data.Entities;

public class PatientSchedule
{
    public long Id { get; set; }
    public System.DateOnly Date { get; set; }


    public System.DateOnly? GivenDate { get; set; }
    public long? DoseId { get; set; }
    public long DoctorId { get; set; }
    public long ChildId { get; set; }
    public bool IsSkip { get; set; }
    public bool IsSpecial { get; set; }
    public bool IsSpecial2 { get; set; }
    public bool IsDone { get; set; }
    public long? BrandId { get; set; }
    public virtual Dose Dose {get;set;}
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public double? OFC { get; set; }
}
