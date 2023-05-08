using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class BrandAmount
{

  
    public long Id { get; set; }
    public int Amount { get; set; }
    public int BrandId { get; set; }
  

    public int DoctorId { get; set; }
  
    
}