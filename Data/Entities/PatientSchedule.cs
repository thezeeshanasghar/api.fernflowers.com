using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
// using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace api.fernflowers.com.Data.Entities;

public class PatientSchedule
{
    public long Id { get; set; }
    [JsonProperty("date")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public System.DateOnly Date { get; set; }
    
    public long DoseId { get; set; }
    public long DoctorId { get; set; }
    public long ChildId { get; set; }
    public bool IsSkip { get; set; }
    public bool IsDone { get; set; }
}
