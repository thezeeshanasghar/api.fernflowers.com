using Newtonsoft.Json;

using Newtonsoft.Json.Converters;
namespace api.fernflowers.com.Data.Entities;

public class Child
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string FatherName { get; set; }
    public string Email { get; set; }
    // [JsonProperty("date")]
    // [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public DateTime DOB { get; set; }
    public Gender Gender { get; set; }
    public string City { get; set; }
    public string CNIC { get; set; }
    public string MobileNumber { get; set; }
    public bool IsEPIDone { get; set; }
    public bool IsVerified { get; set; }
    public bool IsInactive { get; set; }
    public long ClinicId { get; set; }
    public long DoctorId { get; set; }
}

public enum Gender
{
    Boy, 
    Girl
}