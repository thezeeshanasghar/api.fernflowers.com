namespace api.fernflowers.com.Data.Entities;

public class Vaccine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
    public int? MinGap { get; set; }
}