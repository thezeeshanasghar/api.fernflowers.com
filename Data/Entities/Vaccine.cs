namespace api.fernflowers.com.Data.Entities;
using Newtonsoft.Json;

public class Vaccine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSpecial { get; set; }
    public bool Infinite { get; set; }


    [JsonIgnore]
    public virtual ICollection<Dose> Doses {get;set;}
    [JsonIgnore]
    public virtual ICollection<Brand> Brands {get;set;}
}