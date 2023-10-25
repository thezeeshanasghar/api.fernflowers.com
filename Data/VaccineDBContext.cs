using Microsoft.EntityFrameworkCore;
using api.fernflowers.com.Data.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
    public DbSet<ClinicTiming> ClinicTimings { get; set; }
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    public DbSet<BrandAmount> BrandAmounts { get; set; }
    public DbSet<BrandInventory> BrandInventories { get; set; }
    public DbSet<Child> Childs { get; set; }
    public DbSet<AdminSchedule> AdminSchedules { get; set; }
    public DbSet<PatientSchedule> PatientSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                    base.OnModelCreating(modelBuilder);
                    modelBuilder.Entity< Doctor >().HasData(
                        new Doctor{
                            Id=1,
                            Name="Ali",
                            MobileNumber="03352920239",
                            Password="123",
                            Email="ali.iiui1234@gmail.com",
                            PMDC="a1234",
                            ValidUpto =new DateOnly(2023, 7, 26)
                        }
                    );

                    modelBuilder.Entity< Clinic >().HasData(
                        new Clinic{
                            Id=1,
                            Name="Ali s clinic ",
                            Number="3333333",
                            Address="b17",
                            City="Isb",
                            Fees="100",
                            DoctorId=1,
                            IsOnline=true

                        }
                    ); 
                    
                    modelBuilder.Entity< ClinicTiming >().HasData(
                        new ClinicTiming{
                            Id=1,
                            Day="Monday",
                            Session="Morning",
                            StartTime=new TimeSpan(2,0,0),
                            EndTime=new TimeSpan(3,0,0),
                            ClinicId=1
                        }
                    ); 

                    //vccine
                    modelBuilder.Entity< Vaccine >().HasData(
                        new Vaccine{
                            Id=1,
                            Name="BCG",
                            IsSpecial=false,
                            Infinite=false
                        },
                        new Vaccine{
                            Id=2,
                            Name="OPV",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=3,
                            Name="HBV",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=4,
                            Name="Hepatitis B",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=5,
                            Name="Pre-Exp RABIES",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=6,
                            Name="PO-Ex Rabies",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=7,
                            Name="Tetanus",
                            IsSpecial=false,
                            Infinite=true

                        },
                        new Vaccine{
                            Id=8,
                            Name="Rota Virus GE",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=9,
                            Name="OPV/IPV+HBV+DPT+Hib",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=10,
                            Name="Pneumococcal",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=11,
                            Name="HBV+DPT+Hib",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=12,
                            Name="MenB vaccines",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=13,
                            Name="Flu",
                            IsSpecial=false,
                            Infinite=true

                        },
                        new Vaccine{
                            Id=14,
                            Name="MEASLES",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=15,
                            Name="MenACWY",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=16,
                            Name="Yellow Fever",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=17,
                            Name="Typhoid",
                            IsSpecial=false,
                            Infinite=true

                        },
                        new Vaccine{
                            Id=18,
                            Name="MR",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=19,
                            Name="Chicken Pox",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=20,
                            Name="MMR",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=21,
                            Name="Hepatitis A",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=22,
                            Name="PPSV/PCV",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=23,
                            Name="DTaP",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=24,
                            Name="COVID 19",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=25,
                            Name="HPV",
                            IsSpecial=false,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=26,
                            Name="Dengue Fever",
                            IsSpecial=false,
                            Infinite=false

                        }
                    );

                   
                
        }
                           
}