﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.fernflowers.com.Data;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    [DbContext(typeof(VaccineDBContext))]
    [Migration("20230809145845_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.AdminSchedule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<long>("DoseId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DoseId");

                    b.ToTable("AdminSchedules");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Brand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("VaccineId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("VaccineId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.BrandAmount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<long?>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DoctorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("BrandAmounts");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.BrandInventory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<long?>("DoctorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("BrandInventories");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Child", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CNIC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("ClinicId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("DOB")
                        .HasColumnType("date");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsEPIDone")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsInactive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Childs");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Clinic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.ClinicTiming", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ClinicId")
                        .HasColumnType("bigint");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("Session")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time(6)");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.ToTable("ClinicTimings");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Doctor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PMDC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("ValidUpto")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.DoctorSchedule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint");

                    b.Property<long>("DoseId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DoseId");

                    b.ToTable("DoctorSchedules");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Dose", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("MinAge")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("VaccineId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("VaccineId");

                    b.ToTable("Doses");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.PatientSchedule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long>("ChildId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DoseId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDone")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSkip")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("DoseId");

                    b.ToTable("PatientSchedules");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Vaccine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Infinite")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSpecial")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.AdminSchedule", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Dose", "Dose")
                        .WithMany()
                        .HasForeignKey("DoseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dose");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Brand", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Vaccine", null)
                        .WithMany("Brands")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Clinic", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Doctor", null)
                        .WithMany("Clinics")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.ClinicTiming", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Clinic", null)
                        .WithMany("ClinicTimings")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.DoctorSchedule", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Dose", "Dose")
                        .WithMany()
                        .HasForeignKey("DoseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dose");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Dose", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Vaccine", null)
                        .WithMany("Doses")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.PatientSchedule", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Dose", "Dose")
                        .WithMany()
                        .HasForeignKey("DoseId");

                    b.Navigation("Dose");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Clinic", b =>
                {
                    b.Navigation("ClinicTimings");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Doctor", b =>
                {
                    b.Navigation("Clinics");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Vaccine", b =>
                {
                    b.Navigation("Brands");

                    b.Navigation("Doses");
                });
#pragma warning restore 612, 618
        }
    }
}
