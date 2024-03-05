namespace api.fernflowers.com.ModelDTO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PatientScheduleUpdateDTO
{
     public long Id { get; set; }
    public string CurrentDate { get; set; }
    public bool IsDone { get; set; }
    public string GivenDate { get; set; }
    public long? BrandId { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public double? OFC { get; set; }
    public bool IsSkip { get; set; }
}