namespace api.fernflowers.com.ModelDTO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PatientScheduleDTO
{
    public long Id { get; set; }
    [JsonProperty("date")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public System.DateOnly Date { get; set; }

    [JsonProperty("GivenDate")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public System.DateOnly GivenDate { get; set; }
    public long DoseId { get; set; }
    public long DoctorId { get; set; }
    public long ChildId { get; set; }
    public bool IsSkip { get; set; }
    public bool IsSpecial { get; set; }
    public bool IsSpecial2 { get; set; }
    public bool IsDone { get; set; }
    public long? BrandId { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public double? OFC { get; set; }
}