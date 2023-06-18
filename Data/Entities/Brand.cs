using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api.fernflowers.com.Data.Entities;

public class Brand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long VaccineId { get; set; }
}