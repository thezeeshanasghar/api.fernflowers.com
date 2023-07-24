using api.fernflowers.com.Data.Entities;
using System.Text.Json.Serialization;


namespace api.fernflowers.com.ModelDTO
{
    public class ChildDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        public string City { get; set; }
        public string CNIC { get; set; }
        public string MobileNumber { get; set; }
        public bool IsEPIDone { get; set; }
        public bool IsVerified { get; set; }
        public bool IsInactive { get; set; }
        public long ClinicId { get; set; }
        public long DoctorId {get; set;}
    }
}
