namespace api.fernflowers.com.Data.Entities;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MobileNumber { get; set; }
    public string Password {get; set;}
    public bool Isapproved { get; set; }
    public bool IsEnabled { get; set; }
    public string Email { get; set; }
    public string DoctorType { get; set; }
    public string PMDC { get; set; }
    public int BrandAmountId { get; set; }

}