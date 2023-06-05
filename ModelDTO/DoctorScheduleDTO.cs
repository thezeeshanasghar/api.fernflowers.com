
namespace api.fernflowers.com.ModelDTO{
    public class DoctorScheduleDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? DoseId { get; set; }
        public int? DoctorId { get; set; }
        public DoseDTO Dose { get; set; }

    }

}
