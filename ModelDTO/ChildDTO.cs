
using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO
{
    public class ChildDTO
    {
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Guardian { get; set; }
    public string FatherName { get; set; }
    public string Email { get; set; }
    public System.DateTime DOB { get; set; }
    public string Gender { get; set; }
    public string Type { get; set; }
    public string City { get; set; }
    public string CNIC { get; set; }
    public string PreferredSchedule { get; set; }
    public bool? IsEPIDone { get; set; }
    public bool? IsVerified { get; set; }
    public bool? IsInactive { get; set; }
    public int ClinicId { get; set; }
    }
}