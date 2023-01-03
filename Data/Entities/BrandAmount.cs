using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class BrandAmount
{
  public BrandAmount()
    {
        this.Brands = new HashSet<Brand>();
        this.Doctors = new HashSet<Doctor>();
    }
    

    
    
    public long Id { get; set; }
    public int Amount { get; set; }
    public int BrandId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Brand> Brands { get; set; }

    public int DoctorId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Doctor> Doctors { get; set; }
    //  public virtual string VaccineName { get; set; }



}