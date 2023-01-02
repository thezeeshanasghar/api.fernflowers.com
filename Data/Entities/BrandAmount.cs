using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class BrandAmount
{
   public long Id { get; set; }
        public int Amount { get; set; }
        public long BrandId { get; set; }
         [JsonIgnore]
        public virtual Brand Brand { get; set; }
    
        public long DoctorId { get; set; }
         [JsonIgnore]
        public virtual Doctor Doctor { get; set; }
      //  public virtual string VaccineName { get; set; }
    
   
    
}