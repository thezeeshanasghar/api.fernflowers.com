using System.Collections.Generic;
using Newtonsoft.Json;

namespace api.fernflowers.com.ModelDTO {
    public class AdminDoseScheduleDTO
    {
        public int Id { get; set; }
       
        public DateTime? Date { get; set; }
        public int? DoseId { get; set; }
        public DoseDTO Dose { get; set; }
    }

}

