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
    [Migration("20230104075801_child2.1")]
    partial class child21
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BrandAmountId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("VaccineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandAmountId");

                    b.HasIndex("VaccineId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.BrandAmount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BrandAmounts");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.BrandInventory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("DoctorId");

                    b.ToTable("BrandInventories");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CNIC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ClinicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Guardian")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsEPIDone")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsInactive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PreferredDayOfReminder")
                        .HasColumnType("int");

                    b.Property<string>("PreferredDayOfWeek")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PreferredSchedule")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.ToTable("Childs");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BrandAmountId")
                        .HasColumnType("int");

                    b.Property<string>("DoctorType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Isapproved")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MobileNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PMDC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BrandAmountId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Dose", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MinAge")
                        .HasColumnType("int");

                    b.Property<int?>("MinGap")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("VaccineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VaccineId");

                    b.ToTable("Doses");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Vaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

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

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Brand", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.BrandAmount", null)
                        .WithMany("Brands")
                        .HasForeignKey("BrandAmountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.fernflowers.com.Data.Entities.Vaccine", null)
                        .WithMany("Brands")
                        .HasForeignKey("VaccineId");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.BrandInventory", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.fernflowers.com.Data.Entities.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Child", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Doctor", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.BrandAmount", null)
                        .WithMany("Doctors")
                        .HasForeignKey("BrandAmountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.Dose", b =>
                {
                    b.HasOne("api.fernflowers.com.Data.Entities.Vaccine", null)
                        .WithMany("Doses")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.fernflowers.com.Data.Entities.BrandAmount", b =>
                {
                    b.Navigation("Brands");

                    b.Navigation("Doctors");
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
