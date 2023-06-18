using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api.fernflowers.com.Data.Entities;

public class Doctor
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string MobileNumber { get; set; }
    public string Password { get; set; }
    public bool IsApproved { get; set; }
    public bool IsEnabled { get; set; }
    public string Email { get; set; }
    public string PMDC { get; set; }
    public DateTime ValidUpto { get; set; }

    public virtual ICollection<Clinic> Clinics { get; set; }

    public Doctor()
    {
        this.Clinics = new HashSet<Clinic>();
    }
}
