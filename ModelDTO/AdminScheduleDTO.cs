using System.Collections.Generic;
using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO
{
    public class AdminScheduleDTO
    {
        public long Id { get; set; }
        [JsonProperty("date")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateOnly Date { get; set; }
        public long DoseId { get; set; }
    }
}
