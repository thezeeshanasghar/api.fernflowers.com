namespace api.fernflowers.com.ModelDTO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PatientScheduleDTO
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
    public long? BrandId { get; set; }
}