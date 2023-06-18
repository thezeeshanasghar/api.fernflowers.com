namespace api.fernflowers.com.ModelDTO
{
    public class DoctorDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public bool IsEnabled { get; set; }
        public string Email { get; set; }
        public string PMDC { get; set; }
        public string ValidUpto{get;set;}
        public List<ClinicDTO> Clinics { get; set; }
    }
}
