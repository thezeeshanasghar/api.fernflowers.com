namespace api.fernflowers.com.Data.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
public class Dose
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int MinAge { get; set; }
    public long VaccineId { get; set; }
    
    // public virtual AdminSchedule AdminSchedules { get; set; }
   
    // public virtual DoctorSchedule DoctorSchedules { get; set; }

    // public virtual PatientSchedule PatientSchedules { get; set; }
}