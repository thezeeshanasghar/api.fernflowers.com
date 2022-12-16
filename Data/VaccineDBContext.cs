using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.Data.Entities;

namespace api.fernflowers.com.Data;

public class VaccineDBContext : DbContext
{
    public VaccineDBContext(DbContextOptions<VaccineDBContext> options) : base(options)
    {

    }

    public DbSet<Vaccine> Vaccines { get; set; }
}