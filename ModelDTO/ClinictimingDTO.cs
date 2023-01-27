
using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO
{
    public class ClinictimingDTO
    {
        public int? Id { get; set; }
        public string Day { get; set; }
        public string Session { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public int? ClinicId { get; set; }
    }
}