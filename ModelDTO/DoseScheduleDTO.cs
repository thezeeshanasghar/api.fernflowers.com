namespace api.fernflowers.com.ModelDTO {
    public class DoseScheduleDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? DoseId { get; set; }
        public DoseDTO Dose { get; set; }
    }

}

