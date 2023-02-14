using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO;

public class BrandAmountDTO
{
  
    public long? Id { get; set; }
    public int? Amount { get; set; }
    public int? BrandId { get; set; }
    public int DoctorId { get; set; }
  
    
}