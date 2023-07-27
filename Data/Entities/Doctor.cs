using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace api.fernflowers.com.Data.Entities;

public class Doctor
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string MobileNumber { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PMDC { get; set; }
    [JsonProperty("ValidUpto")]
    [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
    public System.DateOnly ValidUpto { get; set; }

    public virtual ICollection<Clinic> Clinics { get; set; }

    public Doctor()
    {
        this.Clinics = new HashSet<Clinic>();
    }
}
