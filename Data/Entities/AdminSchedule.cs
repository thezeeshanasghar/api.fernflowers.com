using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace api.fernflowers.com.Data.Entities;

public class AdminSchedule
{
    public long Id { get; set; }
    public System.DateOnly Date { get; set; }
    public long DoseId { get; set; }
    public virtual Dose Dose {get;set;}
}
