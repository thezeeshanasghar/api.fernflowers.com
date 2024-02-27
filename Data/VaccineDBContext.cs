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
                            MobileNumber="3352920239",
                            Password="1234",
                            Email="ali.iiui1234@gmail.com",
                            PMDC="a1234",
                            ValidUpto =new DateOnly(2028, 7, 26)
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
                            IsSpecial=true,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=5,
                            Name="Pre-Exp RABIES",
                            IsSpecial=true,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=6,
                            Name="PO-Ex Rabies",
                            IsSpecial=true,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=7,
                            Name="Tetanus",
                            IsSpecial=true,
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
                            IsSpecial=true,
                            Infinite=false

                        },
                        new Vaccine{
                            Id=12,
                            Name="MenB vaccines",
                            IsSpecial=true,
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
                            IsSpecial=true,
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
                            IsSpecial=true,
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
                            IsSpecial=true,
                            Infinite=false

                        }
                    );

        
                    modelBuilder.Entity<Dose>().HasData(
                        new Dose
                        {
                            Id = 1,
                            Name = "BCG",
                            MinAge = 0,
                            MinAgeText="At Birth",
                            Special=false,
                            VaccineId = 1

                        },
                        new Dose
                        {
                            Id = 2,
                            Name = "OPV # 1",
                            MinAge = 0,
                            MinAgeText = "At Birth",
                            Special=false,
                            VaccineId = 2

                        },
                        new Dose
                        {
                            Id = 3,
                            Name = "HBV",
                            MinAge = 0,
                            MinAgeText = "At Birth",
                            Special=false,
                            VaccineId = 3

                        },
                        new Dose
                        {
                            Id = 4,
                            Name = "Hep B 1",
                            MinAge = 0,
                            MinAgeText = "At Birth",
                            Special=false,
                            VaccineId = 4

                        },
                        new Dose
                        {
                            Id = 5,
                            Name = "Hep B 2",
                            MinAge = 29,
                            MinAgeText = "4 Weeks",
                            Special=false,
                            VaccineId = 4

                        },
                        new Dose
                        {
                            Id = 6,
                            Name = "Hep B 3",
                            MinAge = 169,
                            MinAgeText = "6 Months",
                            Special=false,
                            VaccineId = 4

                        },

                        new Dose
                        {
                            Id = 7,
                            Name = "Pre-Exp Rabies-1",
                            MinAge = 0,
                            MinAgeText = "At Birth",
                            Special=false,
                            VaccineId = 5

                        },
                        new Dose
                        {
                            Id = 8,
                            Name = "Pre-Exp Rabies-2",
                            MinAge = 1,
                            MinAgeText = "1 Day",
                            Special=false,
                            VaccineId = 5

                        },
                        new Dose
                        {
                            Id = 9,
                            Name = "Pre-Exp Rabies-3",
                            MinAge = 29,
                            MinAgeText = "4 Weeks",
                            Special=false,
                            VaccineId = 5

                        },
                        new Dose
                        {
                            Id = 10,
                            Name = "RABIES#1",
                            MinAge = 0,
                            MinAgeText = "At Birth",
                            Special=false,
                            VaccineId = 6

                        },
                        new Dose
                        {
                            Id = 11,
                            Name = "RABIES#2",
                            MinAge = 4,
                            MinAgeText = "4 Days",
                            Special=false,
                            VaccineId = 6

                        },
                        new Dose
                        {
                            Id = 12,
                            Name = "RABIES#3",
                            MinAge = 8,
                            MinAgeText = "8 Days",
                            Special=false,
                            VaccineId = 6

                        },
                        new Dose
                        {
                            Id = 13,
                            Name = "RABIES#4",
                            MinAge = 29,
                            MinAgeText = "4 Weeks",
                            Special=false,
                            VaccineId = 6

                        },
                        new Dose
                        {
                            Id = 14,
                            Name = "Tetanus",
                            MinAge = 0,
                            MinAgeText = "At Birth",
                            Special=false,
                            VaccineId = 7

                        },
                        new Dose
                        {
                            Id = 15,
                            Name = "Rota Virus GE # 1",
                            MinAge = 43,
                            MinAgeText = "6 Weeks",
                            Special=false,
                            VaccineId = 8

                        },
                        new Dose
                        {
                            Id = 16,
                            Name = "Rota Virus GE # 2",
                            MinAge = 71,
                            MinAgeText = "10 Weeks",
                            Special=false,
                            VaccineId = 8

                        },
                        new Dose
                        {
                            Id = 17,
                            Name = "Rota Virus GE # 3",
                            MinAge = 99,
                            MinAgeText = "14 Weeks",
                            Special=false,
                            VaccineId = 8

                        },
                        new Dose
                        {
                            Id = 18,
                            Name = "OPV/IPV+HBV+DPT+Hib # 1",
                            MinAge = 43,
                            MinAgeText = "6 Weeks",
                            Special=false,
                            VaccineId = 9

                        },
                        new Dose
                        {
                            Id = 19,
                            Name = "OPV/IPV+HBV+DPT+Hib # 2",
                            MinAge = 71,
                            MinAgeText = "10 Weeks",
                            Special=false,
                            VaccineId = 9

                        },
                        new Dose
                        {
                            Id = 20,
                            Name = "OPV/IPV+HBV+DPT+Hib # 3",
                            MinAge = 99,
                            MinAgeText = "14 Weeks",
                            Special=false,
                            VaccineId = 9

                        },
                        new Dose
                        {
                            Id = 21,
                            Name = "OPV/IPV+HBV+DPT+Hib # 4",
                            MinAge = 365,
                            MinAgeText = "1 Year",
                            Special=false,
                            VaccineId = 9

                        },
                        new Dose
                        {
                            Id = 22,
                            Name = "Pneumococcal # 1",
                            MinAge = 43,
                            MinAgeText = "6 Weeks",
                            Special=false,
                            VaccineId = 10

                        },
                        new Dose
                        {
                            Id = 23,
                            Name = "Pneumococcal # 2",
                            MinAge = 71,
                            MinAgeText = "10 Weeks",
                            Special=false,
                            VaccineId = 10

                        },
                        new Dose
                        {
                            Id = 24,
                            Name = "Pneumococcal # 3",
                            MinAge = 99,
                            MinAgeText = "14 Weeks",
                            Special=false,
                            VaccineId = 10

                        },
                        new Dose
                        {
                            Id = 25,
                            Name = "Pneumococcal # 4",
                            MinAge = 365,
                            MinAgeText = "1 Year",
                            Special=false,
                            VaccineId = 10

                        },
                        new Dose
                        {
                            Id = 26,
                            Name = "HBV+DPT+Hib # 1",
                            MinAge = 43,
                            MinAgeText = "6 Weeks",
                            Special=false,
                            VaccineId = 11

                        },
                        new Dose
                        {
                            Id = 27,
                            Name = "HBV+DPT+Hib # 2",
                            MinAge = 71,
                            MinAgeText = "10 Weeks",
                            Special=false,
                            VaccineId = 11

                        },
                        new Dose
                        {
                            Id = 28,
                            Name = "HBV+DPT+Hib # 3",
                            MinAge = 99,
                            MinAgeText = "14 Weeks",
                            Special=false,
                            VaccineId = 11

                        },
                        new Dose
                        {
                            Id = 29,
                            Name = "MenB Vaccine # 1",
                            MinAge = 57,
                            MinAgeText = "8 Weeks",
                            Special=false,
                            VaccineId = 12

                        },
                        new Dose
                        {
                            Id = 30,
                            Name = "MenB Vaccine # 2",
                            MinAge = 113,
                            MinAgeText = "16 Weeks",
                            Special=false,
                            VaccineId = 12

                        },
                        new Dose
                        {
                            Id = 31,
                            Name = "Flu",
                            MinAge = 169,
                            MinAgeText = "6 Months",
                            Special=false,
                            VaccineId = 13

                        },
                        new Dose
                        {
                            Id = 32,
                            Name = "MEASLES # 1",
                            MinAge = 169,
                            MinAgeText = "6 Months",
                            Special=false,
                            VaccineId = 14

                        },
                        new Dose
                        {
                            Id = 33,
                            Name = "MEASLES # 2",
                            MinAge = 365,
                            MinAgeText = "1 Year",
                            Special=false,
                            VaccineId = 14

                        },
                        new Dose
                        {
                            Id = 34,
                            Name = "MenACWY # 1",
                            MinAge = 275,
                            MinAgeText = "9 Months",
                            Special=false,
                            VaccineId = 15

                        },
                        new Dose
                        {
                            Id = 35,
                            Name = "MenACWY # 2",
                            MinAge = 365,
                            MinAgeText = "1 Year",
                            Special=false,
                            VaccineId = 15

                        },
                        new Dose
                        {
                            Id = 36,
                            Name = "Yellow Fever # 1",
                            MinAge = 275,
                            MinAgeText = "9 Months",
                            Special=false,
                            VaccineId = 16

                        },
                        new Dose
                        {
                            Id = 37,
                            Name = "Typhoid",
                            MinAge = 275,
                            MinAgeText = "9 Months",
                            Special=false,
                            VaccineId = 17

                        },
                        new Dose
                        {
                            Id = 38,
                            Name = "MR#1",
                            MinAge = 275,
                            MinAgeText = "9 Months",
                            Special=false,
                            VaccineId = 18

                        },
                        new Dose
                        {
                            Id = 39,
                            Name = "MR#2",
                            MinAge = 365,
                            MinAgeText = "1 Year",
                            Special=false,
                            VaccineId = 18

                        },
                        new Dose
                        {
                            Id = 40,
                            Name = "Chicken Pox # 1",
                            MinAge = 365,
                            MinAgeText = "1 Year",
                            Special=false,
                            VaccineId = 19

                        },
                        new Dose
                        {
                            Id = 41,
                            Name = "Chicken Pox # 2",
                            MinAge = 457,
                            MinAgeText = "15 Months",
                            Special=false,
                            VaccineId = 19

                        },
                        new Dose
                        {
                            Id = 42,
                            Name = "Chicken Pox # 3",
                            MinAge = 548,
                            MinAgeText = "18 Months",
                            Special=false,
                            VaccineId = 19

                        },
                        new Dose
                        {
                            Id = 43,
                            Name = "MMR # 1",
                            MinAge = 365,
                            MinAgeText = "1 Year",
                            Special=false,
                            VaccineId = 20

                        },
                        new Dose
                        {
                            Id = 44,
                            Name = "MMR # 2",
                            MinAge = 548,
                            MinAgeText = "18 Months",
                            Special=false,
                            VaccineId = 20

                        },
                        new Dose
                        {
                            Id = 45,
                            Name = "MMR # 3",
                            MinAge = 4746,
                            MinAgeText = "13 Year",
                            Special=false,
                            VaccineId = 20

                        },
                        new Dose
                        {
                            Id = 46,
                            Name = "MMR # 4",
                            MinAge = 731,
                            MinAgeText = "2 Year",
                            Special=false,
                            VaccineId = 20

                        },
                        new Dose
                        {
                            Id = 47,
                            Name = "Hepatitis A #1",
                            MinAge = 365,
                            MinAgeText = "1 Year",
                            Special=false,
                            VaccineId = 21

                        },
                        new Dose
                        {
                            Id = 48,
                            Name = "Hepatitis A #2",
                            MinAge = 548,
                            MinAgeText = "18 Months",
                            Special=false,
                            VaccineId = 21

                        },
                        new Dose
                        {
                            Id = 49,
                            Name = "PPSV/PCV",
                            MinAge = 731,
                            MinAgeText = "2 Year",
                            Special=false,
                            VaccineId = 22

                        },
                        new Dose
                        {
                            Id = 50,
                            Name = "DTaP # 1",
                            MinAge = 1826,
                            MinAgeText = "5 Year",
                            Special=false,
                            VaccineId = 23

                        },
                        new Dose
                        {
                            Id = 51,
                            Name = "DTaP # 2",
                            MinAge = 3651,
                            MinAgeText = "10 Year",
                            Special=false,
                            VaccineId = 23

                        },
                        new Dose
                        {
                            Id = 52,
                            Name = "HPV # 1",
                            MinAge = 3286,
                            MinAgeText = "9 Year",
                            Special=false,
                            VaccineId = 25

                        },
                        new Dose
                        {
                            Id = 53,
                            Name = "HPV # 2",
                            MinAge = 3316,
                            MinAgeText = "1 Year 1 Month",
                            Special=false,
                            VaccineId = 25

                        },
                        new Dose
                        {
                            Id = 54,
                            Name = "Dengue Fever #1",
                            MinAge = 3651,
                            MinAgeText = "10 Year",
                            Special=false,
                            VaccineId = 26

                        },
                        new Dose
                        {
                            Id = 55,
                            Name = "Dengue Fever #2",
                            MinAge = 3834,
                            MinAgeText = "10 Year 6 Months",
                            Special=false,
                            VaccineId = 26

                        }
                        );
        modelBuilder.Entity<Brand>().HasData(
        new Brand
        {
            Id = 1,
            Name = "BCG",
            VaccineId = 1,
        },
        new Brand
        {
            Id = 2,
            Name = "OPV",
            VaccineId = 2,

        }, new Brand
        {
            Id = 3,
            Name = "Local",
            VaccineId = 3,

        }, new Brand
        {
            Id = 4,
            Name = "AMVAX B",
            VaccineId = 3,

        }, new Brand
        {
            Id = 5,
            Name = "ENGERIX B",
            VaccineId = 3,

        }, new Brand
        {
            Id = 6,
            Name = "ENGERIX B",
            VaccineId = 4,

        }, new Brand
        {
            Id = 7,
            Name = "INDIRAB",
            VaccineId = 5,

        }, new Brand
        {
            Id = 8,
            Name = "VERORAB",
            VaccineId = 5,

        }, new Brand
        {
            Id = 9,
            Name = "VERORAB",
            VaccineId = 6,

        }, new Brand
        {
            Id = 10,
            Name = "IMATET",
            VaccineId = 7,

        }, new Brand
        {
            Id = 11,
            Name = "TT",
            VaccineId = 7,

        }, new Brand
        {
            Id = 12,
            Name = "Tetanus",
            VaccineId = 7,

        }, new Brand
        {
            Id = 13,
            Name = "ROTARIX",
            VaccineId = 8,

        }, new Brand
        {
            Id = 14,
            Name = "Rota Teq",
            VaccineId = 8,

        }, new Brand
        {
            Id = 15,
            Name = "ROTARIX*",
            VaccineId = 8,

        }, new Brand
        {
            Id = 16,
            Name = "INFANRIX HEXA",
            VaccineId = 9,

        }, new Brand
        {
            Id = 17,
            Name = "HEXAXIM",
            VaccineId = 9,

        }, new Brand
        {
            Id = 18,
            Name = "QUINAVAXEM + OPV",
            VaccineId = 9,

        }, new Brand
        {
            Id = 19,
            Name = "EUPENTA + OPV",
            VaccineId = 9,

        }, new Brand
        {
            Id = 20,
            Name = "PENTA+OPV",
            VaccineId = 9,

        }, new Brand
        {
            Id = 21,
            Name = "PREVENAR*",
            VaccineId = 10,

        }, new Brand
        {
            Id = 22,
            Name = "SYNFLORIX",
            VaccineId = 10,

        }, new Brand
        {
            Id = 23,
            Name = "PREVENAR",
            VaccineId = 10,

        }, new Brand
        {
            Id = 24,
            Name = "PENTA ( amson)",
            VaccineId = 11,

        }, new Brand
        {
            Id = 25,
            Name = "Men B",
            VaccineId = 12,

        }, new Brand
        {
            Id = 26,
            Name = "INFLUVAC",
            VaccineId = 13,

        }, new Brand
        {
            Id = 27,
            Name = "FLUARIX",
            VaccineId = 13,

        }, new Brand
        {
            Id = 28,
            Name = "VAXIGRIP",
            VaccineId = 13,

        }, new Brand
        {
            Id = 29,
            Name = "ANFLU",
            VaccineId = 13,

        }, new Brand
        {
            Id = 30,
            Name = "FLU",
            VaccineId = 13,

        }, new Brand
        {
            Id = 31,
            Name = "MEASLES",
            VaccineId = 14,

        }, new Brand
        {
            Id = 32,
            Name = "MENACTRA",
            VaccineId = 15,

        }, new Brand
        {
            Id = 33,
            Name = "NIMENRIX",
            VaccineId = 15,

        }, new Brand
        {
            Id = 34,
            Name = "STAMARIL",
            VaccineId = 16,

        }, new Brand
        {
            Id = 35,
            Name = "TYPHIRIX",
            VaccineId = 17,

        }, new Brand
        {
            Id = 36,
            Name = "TYPBAR",
            VaccineId = 17,

        }, new Brand
        {
            Id = 37,
            Name = "TYPBAR TCV",
            VaccineId = 17,

        }, new Brand
        {
            Id = 38,
            Name = "TYPBAR*",
            VaccineId = 17,

        }, new Brand
        {
            Id = 39,
            Name = "TYPHOID",
            VaccineId = 17,

        }, new Brand
        {
            Id = 40,
            Name = "MR",
            VaccineId = 18,

        }, new Brand
        {
            Id = 41,
            Name = "VARIVAC",
            VaccineId = 19,

        }, new Brand
        {
            Id = 42,
            Name = "VARILRIX",
            VaccineId = 19,

        }, new Brand
        {
            Id = 43,
            Name = "VAXAPOX",
            VaccineId = 19,

        }, new Brand
        {
            Id = 44,
            Name = "PRIORIX TETRA",
            VaccineId = 19,

        },
        new Brand
        {
            Id = 45,
            Name = "VARICELLA",
            VaccineId = 19,

        },
        new Brand
        {
            Id = 46,
            Name = "TRIMOVAX",
            VaccineId = 20,

        },
        new Brand
        {
            Id = 47,
            Name = "PRIORIX",
            VaccineId = 20,

        },
        new Brand
        {
            Id = 48,
            Name = "MMR",
            VaccineId = 20,

        },
        new Brand
        {
            Id = 49,
            Name = "PRIORIX TETRA",
            VaccineId = 20,

        },
        new Brand
        {
            Id = 50,
            Name = "TRESIVAC",
            VaccineId = 20,

        },
        new Brand
        {
            Id = 51,
            Name = "HAVRIX",
            VaccineId = 21,

        },
        new Brand
        {
            Id = 52,
            Name = "TWINRIX",
            VaccineId = 21,

        },
        new Brand
        {
            Id = 53,
            Name = "AVAXIM",
            VaccineId = 21,

        },
        new Brand
        {
            Id = 54,
            Name = "PCV",
            VaccineId = 22,

        },
        new Brand
        {
            Id = 55,
            Name = "PNEUMOVAX",
            VaccineId = 22,

        },
        new Brand
        {
            Id = 56,
            Name = "PREVENAR",
            VaccineId = 22,

        },
        new Brand
        {
            Id = 57,
            Name = "BOOSTRIX",
            VaccineId = 23,

        },
        new Brand
        {
            Id = 58,
            Name = "BOOSTRIX",
            VaccineId = 23,

        },
        new Brand
        {
            Id = 59,
            Name = "Local",
            VaccineId = 24,

        },
        new Brand
        {
            Id = 60,
            Name = "Pfizer",
            VaccineId = 24,

        },
        new Brand
        {
            Id = 61,
            Name = "SinoVac",
            VaccineId = 24,

        },
        new Brand
        {
            Id = 62,
            Name = "Local",
            VaccineId = 25,

        },
        new Brand
        {
            Id = 63,
            Name = "CERVARIX",
            VaccineId = 25,

        },
        new Brand
        {
            Id = 64,
            Name = "Dengvaxia",
            VaccineId = 26,

        }

    );
    }
                           
}