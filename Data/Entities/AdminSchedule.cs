using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace api.fernflowers.com.Data.Entities;

public class AdminSchedule
{
    public long Id { get; set; }
    // [JsonConverter(typeof(DateOnlyConverter))] 
    [JsonProperty("date")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public System.DateOnly Date { get; set; }
    public long DoseId { get; set; }
}