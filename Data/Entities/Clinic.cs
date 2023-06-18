using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.fernflowers.com.Data.Entities;

public class Clinic
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Number { get; set; }
    public long DoctorId { get; set; }

    public virtual ICollection<ClinicTiming> ClinicTimings { get; set; }

    public Clinic()
    {
        this.ClinicTimings = new HashSet<ClinicTiming>();
    }
}
