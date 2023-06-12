namespace api.fernflowers.com.Data.Entities;
public class PattientsSchedule{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int DoseId { get; set; }
    public int DoctorId { get; set; }
    public int childId {get;set;}
}