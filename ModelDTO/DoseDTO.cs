using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO
{
    public class DoseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public string MinAgeText { get; set; }
         public bool IsSpecial { get; set; }

        public long VaccineId { get; set; }
    }
}
