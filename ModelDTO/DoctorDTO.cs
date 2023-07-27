
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace api.fernflowers.com.ModelDTO
{
    public class DoctorDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PMDC { get; set; }
        [JsonProperty("ValidUpto")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public System.DateOnly ValidUpto{get;set;}
        public List<ClinicDTO> Clinics { get; set; }
    }
}
