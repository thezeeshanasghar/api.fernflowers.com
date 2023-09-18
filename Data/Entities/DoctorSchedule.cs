namespace api.fernflowers.com.Data.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class DoctorSchedule
{
    public long Id { get; set; }
    public System.DateOnly Date { get; set; }
    public long DoseId { get; set; }
    public long DoctorId { get; set; }
    public virtual Dose Dose {get;set;}
}
