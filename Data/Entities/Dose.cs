namespace api.fernflowers.com.Data.Entities;

public class Dose
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int MinAge { get; set; }
    public int? MinGap { get; set; }
    public long VaccineId { get; set; }
}