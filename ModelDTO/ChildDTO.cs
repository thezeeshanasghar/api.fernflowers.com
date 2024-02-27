using api.fernflowers.com.Data.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO
{
    public class ChildDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string GuardianName { get; set; }
        public string? Email { get; set; }
        [JsonProperty("DOB")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public System.DateOnly DOB { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string City { get; set; }
        public string SelectCnicOrPassport { get; set; } = "CNIC";
        public string? CnicOrPassPort { get; set; }
        public string MobileNumber { get; set; }
        public bool IsEPIDone { get; set; }
        public bool IsVerified { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsInactive { get; set; }
        public long ClinicId { get; set; }
        public long DoctorId {get; set;}
    }
}
