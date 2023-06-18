using System.Collections.Generic;
using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO
{
    public class BrandDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long VaccineId { get; set; }
    }
}
