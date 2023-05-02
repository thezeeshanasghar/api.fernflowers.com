namespace api.fernflowers.com.ModelDTO
{
    public class ClinicDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public int? DoctorId { get; set; }
        public List<ClinictimingDTO> ClinicTiming { get; set; }
    }
}
