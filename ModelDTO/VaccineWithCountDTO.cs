using api.fernflowers.com.Data.Entities;

namespace api.fernflowers.com.ModelDTO
{
    public class VaccineWithCountDTO
    {
        public Vaccine vaccine { get; set; }
        public int DoseCount { get; set; }
        public int BrandCount { get; set; }
    }
}