using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.Data.Entities;

namespace api.fernflowers.com.Data;

public class VaccineDBContext : DbContext
{
    public VaccineDBContext(DbContextOptions<VaccineDBContext> options) : base(options)
    {

    }

    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<Dose> Doses { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<Clinictiming> Clinictimings { get; set; }
    // public DbSet<BrandAmount> BrandAmounts { get; set; }
    // public DbSet<BrandInventory> BrandInventories { get; set; }
    public DbSet<Child> Childs { get; set; }
}