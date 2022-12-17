namespace api.fernflowers.com.Data.Entities;

public class User
{
    public int Id { get; set; }
    public int MobileNumber { get; set; }
    public string Password { get; set; }
    public int UserType { get; set; }
    public int CountryCode { get; set; }
}