using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class BrandInventory
{
   
        public long Id { get; set; }
        public int Count { get; set; }
        public int BrandId { get; set; }
        public int DoctorId { get; set; }
    
    
}