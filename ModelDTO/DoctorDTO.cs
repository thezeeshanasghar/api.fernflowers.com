namespace api.fernflowers.com.ModelDTO
{

    public class DoctorDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int MobileNumber { get; set; }
        public string Password {get; set;}
        public bool IsApproved { get; set; } // Naming convension pe goor kiya kro ap.ok
        public bool IsEnabled { get; set; }
        public string Email { get; set; }
        public string DoctorType { get; set; }
        public string PMDC { get; set; }
        public ClinicDTO Clinic { get; set; }
    }

}
