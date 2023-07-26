using api.fernflowers.com.Data.Entities;
using System.Text.Json.Serialization;

namespace api.fernflowers.com.ModelDTO;

public class ClinicTimingDTO
{
    public long Id { get; set; }
    // [JsonConverter(typeof(JsonStringEnumConverter))]
    public string Day { get; set; }
    // [JsonConverter(typeof(JsonStringEnumConverter))]
    public string Session { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public long ClinicId { get; set; }
}
