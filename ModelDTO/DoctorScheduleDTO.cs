namespace api.fernflowers.com.ModelDTO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class DoctorScheduleDTO
{
    public long Id { get; set; }
    [JsonProperty("date")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public System.DateOnly Date { get; set; }
    public long DoseId { get; set; }
    public bool SelectedDose { get; set; }
    public long DoctorId { get; set; }
   
}
