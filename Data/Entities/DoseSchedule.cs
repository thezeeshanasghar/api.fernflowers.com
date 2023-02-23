using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class DoseSchedule
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int DoseId { get; set; }
}