using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class BrandInventory
{
    public long Id { get; set; }
    public int Count { get; set; }
    public long? BrandId { get; set; }
    public long? DoctorId { get; set; }
}
