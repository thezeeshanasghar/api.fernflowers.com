using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace api.fernflowers.com.Data.Entities;

public class AdminSchedule
{
    public long Id { get; set; }
    [JsonProperty("date")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public System.DateOnly Date { get; set; }
    public long DoseId { get; set; }
}