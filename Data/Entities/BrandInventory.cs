using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class BrandInventory
{
   
        public long Id { get; set; }
        public int Count { get; set; }
        public long BrandId { get; set; }
        
        [JsonIgnore]
        public Brand Brand { get; set; }
      
        public long DoctorId { get; set; }
         [JsonIgnore]
        public Doctor Doctor { get; set; }
      //  public virtual ICollection<Vaccine> VaccineName { get; set; }
    
}