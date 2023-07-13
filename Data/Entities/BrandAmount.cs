using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace api.fernflowers.com.Data.Entities;

public class BrandAmount
{
    public long Id { get; set; }
    public int Amount { get; set; }
    public long BrandId { get; set; }
    public long DoctorId { get; set; }

    public virtual Brand Brands { get; set; }
    public virtual Doctor Doctors { get; set; }
    // public BrandAmount()
    // {
    //     this.Brands = new HashSet<Brand>();
    //     this.Doctors = new HashSet<Doctor>();
    // }
}
