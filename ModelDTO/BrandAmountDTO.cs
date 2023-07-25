using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO;

public class BrandAmountDTO
{
    public long Id { get; set; }
    public int Amount { get; set; }
    public long? BrandId { get; set; }
    public long? DoctorId { get; set; }
    public string? BrandName { get; set; }
    // public BrandDTO Brand { get; set; }
}
