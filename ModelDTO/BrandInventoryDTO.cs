
namespace api.fernflowers.com.ModelDTO;

public class BrandInventoryDTO
{
    public long Id { get; set; }
    public int Count { get; set; }
    public long BrandId { get; set; }
    public long DoctorId { get; set; }
    public BrandDTO Brand { get; set; }
}