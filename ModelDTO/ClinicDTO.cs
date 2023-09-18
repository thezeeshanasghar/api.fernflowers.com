namespace api.fernflowers.com.ModelDTO
{
    public class ClinicDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Fees { get; set; }
        public long DoctorId { get; set; }
        public List<ClinicTimingDTO> ClinicTimings { get; set; }
    }
}
