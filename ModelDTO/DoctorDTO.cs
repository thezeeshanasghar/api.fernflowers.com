namespace api.fernflowers.com.ModelDTO
{
    public class DoctorDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public bool Isapproved { get; set; }
        public bool IsEnabled { get; set; }
        public string Email { get; set; }
        public string DoctorType { get; set; }
        public string PMDC { get; set; }
        public ClinicDTO Clinic { get; set; }
        public List<DoctorScheduleDTO> DoctorSchedule { get; set; }
    }
}
