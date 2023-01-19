using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.fernflowers.com.Data.Entities;

public class Clinic
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Number { get; set; }
    public int DoctorId { get; set; }
   
}