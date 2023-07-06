using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DoseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminSchedules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BrandInventories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandInventories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Childs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FatherName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNIC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEPIDone = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsInactive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ClinicId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Childs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsApproved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PMDC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValidUpto = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DoseId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PatientSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DoseId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    ChildId = table.Column<long>(type: "bigint", nullable: false),
                    IsSkip = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDone = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSchedules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSpecial = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Infinite = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinics_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VaccineId = table.Column<long>(type: "bigint", nullable: false),
                    VaccineId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Vaccines_VaccineId1",
                        column: x => x.VaccineId1,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MinAge = table.Column<int>(type: "int", nullable: false),
                    MinGap = table.Column<int>(type: "int", nullable: true),
                    VaccineId = table.Column<long>(type: "bigint", nullable: false),
                    VaccineId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doses_Vaccines_VaccineId1",
                        column: x => x.VaccineId1,
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClinicTimings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Session = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ClinicId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicTimings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicTimings_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BrandAmounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandAmounts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandAmounts_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name", "VaccineId", "VaccineId1" },
                values: new object[,]
                {
                    { 1L, "BCG", 1L, null },
                    { 2L, "OPV", 2L, null },
                    { 3L, "Local", 3L, null },
                    { 4L, "AMVAX B", 3L, null },
                    { 5L, "ENGERIX B", 3L, null },
                    { 6L, "ENGERIX B", 4L, null },
                    { 7L, "INDIRAB", 5L, null },
                    { 8L, "VERORAB", 5L, null },
                    { 9L, "VERORAB", 6L, null },
                    { 10L, "IMATET", 7L, null },
                    { 11L, "TT", 7L, null },
                    { 12L, "Tetanus", 7L, null },
                    { 13L, "ROTARIX", 8L, null },
                    { 14L, "Rota Teq", 8L, null },
                    { 15L, "ROTARIX*", 8L, null },
                    { 16L, "INFANRIX HEXA", 9L, null },
                    { 17L, "HEXAXIM", 9L, null },
                    { 18L, "QUINAVAXEM + OPV", 9L, null },
                    { 19L, "EUPENTA + OPV", 9L, null },
                    { 20L, "PENTA+OPV", 9L, null },
                    { 21L, "PREVENAR*", 10L, null },
                    { 22L, "SYNFLORIX", 10L, null },
                    { 23L, "PREVENAR", 10L, null },
                    { 24L, "PENTA ( amson)", 11L, null },
                    { 25L, "Men B", 12L, null },
                    { 26L, "INFLUVAC", 13L, null },
                    { 27L, "FLUARIX", 13L, null },
                    { 28L, "VAXIGRIP", 13L, null },
                    { 29L, "ANFLU", 13L, null },
                    { 30L, "FLU", 13L, null },
                    { 31L, "MEASLES", 14L, null },
                    { 32L, "MENACTRA", 15L, null },
                    { 33L, "NIMENRIX", 15L, null },
                    { 34L, "STAMARIL", 16L, null },
                    { 35L, "TYPHIRIX", 17L, null },
                    { 36L, "TYPBAR", 17L, null },
                    { 37L, "TYPBAR TCV", 17L, null },
                    { 38L, "TYPBAR*", 17L, null },
                    { 39L, "TYPHOID", 17L, null },
                    { 40L, "MR", 18L, null },
                    { 41L, "VARIVAC", 19L, null },
                    { 42L, "VARILRIX", 19L, null },
                    { 43L, "VAXAPOX", 19L, null },
                    { 44L, "PRIORIX TETRA", 19L, null },
                    { 45L, "VARICELLA", 19L, null },
                    { 46L, "TRIMOVAX", 20L, null },
                    { 47L, "PRIORIX", 20L, null },
                    { 48L, "MMR", 20L, null },
                    { 49L, "PRIORIX TETRA", 20L, null },
                    { 50L, "TRESIVAC", 20L, null },
                    { 51L, "HAVRIX", 21L, null },
                    { 52L, "TWINRIX", 21L, null },
                    { 53L, "AVAXIM", 21L, null },
                    { 54L, "PCV", 22L, null },
                    { 55L, "PNEUMOVAX", 22L, null },
                    { 56L, "PREVENAR", 22L, null },
                    { 57L, "BOOSTRIX", 23L, null },
                    { 58L, "BOOSTRIX", 23L, null },
                    { 59L, "Local", 24L, null },
                    { 60L, "Pfizer", 24L, null },
                    { 61L, "SinoVac", 24L, null },
                    { 62L, "Local", 25L, null },
                    { 63L, "CERVARIX", 25L, null },
                    { 64L, "Dengvaxia", 26L, null }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Email", "IsApproved", "IsEnabled", "MobileNumber", "Name", "PMDC", "Password", "ValidUpto" },
                values: new object[] { 1L, "ali.iiui1234@gmail.com", true, true, "03352920239", "Ali", "a1234", "123", new DateTime(2023, 10, 6, 10, 0, 57, 584, DateTimeKind.Utc).AddTicks(389) });

            migrationBuilder.InsertData(
                table: "Doses",
                columns: new[] { "Id", "MinAge", "MinGap", "Name", "VaccineId", "VaccineId1" },
                values: new object[,]
                {
                    { 1L, 0, 0, "BCG", 1L, null },
                    { 2L, 0, 0, "OPV # 1", 2L, null },
                    { 3L, 0, 0, "HBV", 3L, null },
                    { 4L, 0, 0, "Hep B 1", 4L, null },
                    { 5L, 28, 28, "Hep B 2", 4L, null },
                    { 6L, 168, 140, "Hep B 3", 4L, null },
                    { 7L, 0, 0, "Pre-Exp Rabies-1", 5L, null },
                    { 8L, 1, 1, "Pre-Exp Rabies-2", 5L, null },
                    { 9L, 28, 21, "Pre-Exp Rabies-3", 5L, null },
                    { 10L, 0, 0, "RABIES#1", 6L, null },
                    { 11L, 4, 3, "RABIES#2", 6L, null },
                    { 12L, 8, 4, "RABIES#3", 6L, null },
                    { 13L, 28, 21, "RABIES#4", 6L, null },
                    { 14L, 0, 0, "Tetanus", 7L, null },
                    { 15L, 42, 0, "Rota Virus GE # 1", 8L, null },
                    { 16L, 70, 28, "Rota Virus GE # 2", 8L, null },
                    { 17L, 98, 28, "Rota Virus GE # 3", 8L, null },
                    { 18L, 42, 0, "OPV/IPV+HBV+DPT+Hib # 1", 9L, null },
                    { 19L, 70, 28, "OPV/IPV+HBV+DPT+Hib # 2", 9L, null },
                    { 20L, 98, 28, "OPV/IPV+HBV+DPT+Hib # 3", 9L, null },
                    { 21L, 365, 168, "OPV/IPV+HBV+DPT+Hib # 4", 9L, null },
                    { 22L, 42, 0, "Pneumococcal # 1", 10L, null },
                    { 23L, 70, 28, "Pneumococcal # 2", 10L, null },
                    { 24L, 98, 28, "Pneumococcal # 3", 10L, null },
                    { 25L, 365, 168, "Pneumococcal # 4", 10L, null },
                    { 26L, 42, 0, "HBV+DPT+Hib # 1", 11L, null },
                    { 27L, 70, 28, "HBV+DPT+Hib # 2", 11L, null },
                    { 28L, 98, 28, "HBV+DPT+Hib # 3", 11L, null },
                    { 29L, 56, 0, "MenB Vaccine # 1", 12L, null },
                    { 30L, 112, 56, "MenB Vaccine # 2", 12L, null },
                    { 31L, 168, 0, "Flu", 13L, null },
                    { 32L, 168, 0, "MEASLES # 1", 14L, null },
                    { 33L, 365, 168, "MEASLES # 2", 14L, null },
                    { 34L, 274, 0, "MenACWY # 1", 15L, null },
                    { 35L, 365, 84, "MenACWY # 2", 15L, null },
                    { 36L, 274, 0, "Yellow Fever # 1", 16L, null },
                    { 37L, 274, 0, "Typhoid", 17L, null },
                    { 38L, 274, 0, "MR#1", 18L, null },
                    { 39L, 365, 84, "MR#2", 18L, null },
                    { 40L, 365, 0, "Chicken Pox # 1", 19L, null },
                    { 41L, 456, 84, "Chicken Pox # 2", 19L, null },
                    { 42L, 547, 0, "Chicken Pox # 3", 19L, null },
                    { 43L, 365, 0, "MMR # 1", 20L, null },
                    { 44L, 547, 168, "MMR # 2", 20L, null },
                    { 45L, 4745, 0, "MMR # 3", 20L, null },
                    { 46L, 730, 0, "MMR # 4", 20L, null },
                    { 47L, 365, 0, "Hepatitis A #1", 21L, null },
                    { 48L, 547, 168, "Hepatitis A #2", 21L, null },
                    { 49L, 730, 0, "PPSV/PCV", 22L, null },
                    { 50L, 1825, 0, "DTaP # 1", 23L, null },
                    { 51L, 3650, 1825, "DTaP # 2", 23L, null },
                    { 52L, 3285, 0, "HPV # 1", 25L, null },
                    { 53L, 3315, 28, "HPV # 2", 25L, null },
                    { 54L, 3650, 0, "Dengue Fever #1", 26L, null },
                    { 55L, 3833, 168, "Dengue Fever #2", 26L, null }
                });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Infinite", "IsSpecial", "Name" },
                values: new object[,]
                {
                    { 1, false, false, "BCG" },
                    { 2, false, false, "OPV" },
                    { 3, false, false, "HBV" },
                    { 4, false, false, "Hepatitis B" },
                    { 5, false, false, "Pre-Exp RABIES" },
                    { 6, false, false, "PO-Ex Rabies" },
                    { 7, true, false, "Tetanus" },
                    { 8, false, false, "Rota Virus GE" },
                    { 9, false, false, "OPV/IPV+HBV+DPT+Hib" },
                    { 10, false, false, "Pneumococcal" },
                    { 11, false, false, "HBV+DPT+Hib" },
                    { 12, false, false, "MenB vaccines" },
                    { 13, true, false, "Flu" },
                    { 14, false, false, "MEASLES" },
                    { 15, false, false, "MenACWY" },
                    { 16, false, false, "Yellow Fever" },
                    { 17, true, false, "Typhoid" },
                    { 18, false, false, "MR" },
                    { 19, false, false, "Chicken Pox" },
                    { 20, false, false, "MMR" },
                    { 21, false, false, "Hepatitis A" },
                    { 22, false, false, "PPSV/PCV" },
                    { 23, false, false, "DTaP" },
                    { 24, false, false, "COVID 19" },
                    { 25, false, false, "HPV" },
                    { 26, false, false, "Dengue Fever" }
                });

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Address", "DoctorId", "Name", "Number" },
                values: new object[] { 1L, "b17", 1L, "Ali s clinic ", "3333333" });

            migrationBuilder.InsertData(
                table: "ClinicTimings",
                columns: new[] { "Id", "ClinicId", "Day", "EndTime", "Session", "StartTime" },
                values: new object[] { 1L, 1L, 0, new TimeSpan(0, 3, 0, 0, 0), 0, new TimeSpan(0, 2, 0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_BrandId",
                table: "BrandAmounts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_DoctorId",
                table: "BrandAmounts",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_VaccineId1",
                table: "Brands",
                column: "VaccineId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_DoctorId",
                table: "Clinics",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicTimings_ClinicId",
                table: "ClinicTimings",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Doses_VaccineId1",
                table: "Doses",
                column: "VaccineId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminSchedules");

            migrationBuilder.DropTable(
                name: "BrandAmounts");

            migrationBuilder.DropTable(
                name: "BrandInventories");

            migrationBuilder.DropTable(
                name: "Childs");

            migrationBuilder.DropTable(
                name: "ClinicTimings");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "Doses");

            migrationBuilder.DropTable(
                name: "PatientSchedules");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
