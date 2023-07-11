using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class AdminSchedule
{
    public long Id { get; set; }
    public DateOnly Date { get; set; }
    public long DoseId { get; set; }
}