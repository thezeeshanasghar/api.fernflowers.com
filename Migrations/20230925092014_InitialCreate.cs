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
                name: "BrandAmounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    DoctorId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandAmounts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BrandInventories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    DoctorId = table.Column<long>(type: "bigint", nullable: true)
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FatherName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNIC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNumber = table.Column<string>(type: "longtext", nullable: false)
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
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PMDC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValidUpto = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
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
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fees = table.Column<string>(type: "longtext", nullable: false)
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
                    VaccineId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    VaccineId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doses_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClinicTimings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Session = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                name: "AdminSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DoseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminSchedules_Doses_DoseId",
                        column: x => x.DoseId,
                        principalTable: "Doses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DoseId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Doses_DoseId",
                        column: x => x.DoseId,
                        principalTable: "Doses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PatientSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DoseId = table.Column<long>(type: "bigint", nullable: true),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    ChildId = table.Column<long>(type: "bigint", nullable: false),
                    IsSkip = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDone = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientSchedules_Doses_DoseId",
                        column: x => x.DoseId,
                        principalTable: "Doses",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Email", "MobileNumber", "Name", "PMDC", "Password", "ValidUpto" },
                values: new object[] { 1L, "ali.iiui1234@gmail.com", "03352920239", "Ali", "a1234", "123", new DateOnly(2023, 7, 26) });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Infinite", "IsSpecial", "Name" },
                values: new object[,]
                {
                    { 1L, false, false, "BCG" },
                    { 2L, false, false, "OPV" },
                    { 3L, false, false, "HBV" },
                    { 4L, false, false, "Hepatitis B" },
                    { 5L, false, false, "Pre-Exp RABIES" },
                    { 6L, false, false, "PO-Ex Rabies" },
                    { 7L, true, false, "Tetanus" },
                    { 8L, false, false, "Rota Virus GE" },
                    { 9L, false, false, "OPV/IPV+HBV+DPT+Hib" },
                    { 10L, false, false, "Pneumococcal" },
                    { 11L, false, false, "HBV+DPT+Hib" },
                    { 12L, false, false, "MenB vaccines" },
                    { 13L, true, false, "Flu" },
                    { 14L, false, false, "MEASLES" },
                    { 15L, false, false, "MenACWY" },
                    { 16L, false, false, "Yellow Fever" },
                    { 17L, true, false, "Typhoid" },
                    { 18L, false, false, "MR" },
                    { 19L, false, false, "Chicken Pox" },
                    { 20L, false, false, "MMR" },
                    { 21L, false, false, "Hepatitis A" },
                    { 22L, false, false, "PPSV/PCV" },
                    { 23L, false, false, "DTaP" },
                    { 24L, false, false, "COVID 19" },
                    { 25L, false, false, "HPV" },
                    { 26L, false, false, "Dengue Fever" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name", "VaccineId" },
                values: new object[,]
                {
                    { 1L, "BCG", 1L },
                    { 2L, "OPV", 2L },
                    { 3L, "Local", 3L },
                    { 4L, "AMVAX B", 3L },
                    { 5L, "ENGERIX B", 3L },
                    { 6L, "ENGERIX B", 4L },
                    { 7L, "INDIRAB", 5L },
                    { 8L, "VERORAB", 5L },
                    { 9L, "VERORAB", 6L },
                    { 10L, "IMATET", 7L },
                    { 11L, "TT", 7L },
                    { 12L, "Tetanus", 7L },
                    { 13L, "ROTARIX", 8L },
                    { 14L, "Rota Teq", 8L },
                    { 15L, "ROTARIX*", 8L },
                    { 16L, "INFANRIX HEXA", 9L },
                    { 17L, "HEXAXIM", 9L },
                    { 18L, "QUINAVAXEM + OPV", 9L },
                    { 19L, "EUPENTA + OPV", 9L },
                    { 20L, "PENTA+OPV", 9L },
                    { 21L, "PREVENAR*", 10L },
                    { 22L, "SYNFLORIX", 10L },
                    { 23L, "PREVENAR", 10L },
                    { 24L, "PENTA ( amson)", 11L },
                    { 25L, "Men B", 12L },
                    { 26L, "INFLUVAC", 13L },
                    { 27L, "FLUARIX", 13L },
                    { 28L, "VAXIGRIP", 13L },
                    { 29L, "ANFLU", 13L },
                    { 30L, "FLU", 13L },
                    { 31L, "MEASLES", 14L },
                    { 32L, "MENACTRA", 15L },
                    { 33L, "NIMENRIX", 15L },
                    { 34L, "STAMARIL", 16L },
                    { 35L, "TYPHIRIX", 17L },
                    { 36L, "TYPBAR", 17L },
                    { 37L, "TYPBAR TCV", 17L },
                    { 38L, "TYPBAR*", 17L },
                    { 39L, "TYPHOID", 17L },
                    { 40L, "MR", 18L },
                    { 41L, "VARIVAC", 19L },
                    { 42L, "VARILRIX", 19L },
                    { 43L, "VAXAPOX", 19L },
                    { 44L, "PRIORIX TETRA", 19L },
                    { 45L, "VARICELLA", 19L },
                    { 46L, "TRIMOVAX", 20L },
                    { 47L, "PRIORIX", 20L },
                    { 48L, "MMR", 20L },
                    { 49L, "PRIORIX TETRA", 20L },
                    { 50L, "TRESIVAC", 20L },
                    { 51L, "HAVRIX", 21L },
                    { 52L, "TWINRIX", 21L },
                    { 53L, "AVAXIM", 21L },
                    { 54L, "PCV", 22L },
                    { 55L, "PNEUMOVAX", 22L },
                    { 56L, "PREVENAR", 22L },
                    { 57L, "BOOSTRIX", 23L },
                    { 58L, "BOOSTRIX", 23L },
                    { 59L, "Local", 24L },
                    { 60L, "Pfizer", 24L },
                    { 61L, "SinoVac", 24L },
                    { 62L, "Local", 25L },
                    { 63L, "CERVARIX", 25L },
                    { 64L, "Dengvaxia", 26L }
                });

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Address", "City", "DoctorId", "Fees", "Name", "Number" },
                values: new object[] { 1L, "b17", "Isb", 1L, "100", "Ali s clinic ", "3333333" });

            migrationBuilder.InsertData(
                table: "Doses",
                columns: new[] { "Id", "MinAge", "Name", "VaccineId" },
                values: new object[,]
                {
                    { 1L, 0, "BCG", 1L },
                    { 2L, 0, "OPV # 1", 2L },
                    { 3L, 0, "HBV", 3L },
                    { 4L, 0, "Hep B 1", 4L },
                    { 5L, 28, "Hep B 2", 4L },
                    { 6L, 168, "Hep B 3", 4L },
                    { 7L, 0, "Pre-Exp Rabies-1", 5L },
                    { 8L, 1, "Pre-Exp Rabies-2", 5L },
                    { 9L, 28, "Pre-Exp Rabies-3", 5L },
                    { 10L, 0, "RABIES#1", 6L },
                    { 11L, 4, "RABIES#2", 6L },
                    { 12L, 8, "RABIES#3", 6L },
                    { 13L, 28, "RABIES#4", 6L },
                    { 14L, 0, "Tetanus", 7L },
                    { 15L, 42, "Rota Virus GE # 1", 8L },
                    { 16L, 70, "Rota Virus GE # 2", 8L },
                    { 17L, 98, "Rota Virus GE # 3", 8L },
                    { 18L, 42, "OPV/IPV+HBV+DPT+Hib # 1", 9L },
                    { 19L, 70, "OPV/IPV+HBV+DPT+Hib # 2", 9L },
                    { 20L, 98, "OPV/IPV+HBV+DPT+Hib # 3", 9L },
                    { 21L, 365, "OPV/IPV+HBV+DPT+Hib # 4", 9L },
                    { 22L, 42, "Pneumococcal # 1", 10L },
                    { 23L, 70, "Pneumococcal # 2", 10L },
                    { 24L, 98, "Pneumococcal # 3", 10L },
                    { 25L, 365, "Pneumococcal # 4", 10L },
                    { 26L, 42, "HBV+DPT+Hib # 1", 11L },
                    { 27L, 70, "HBV+DPT+Hib # 2", 11L },
                    { 28L, 98, "HBV+DPT+Hib # 3", 11L },
                    { 29L, 56, "MenB Vaccine # 1", 12L },
                    { 30L, 112, "MenB Vaccine # 2", 12L },
                    { 31L, 168, "Flu", 13L },
                    { 32L, 168, "MEASLES # 1", 14L },
                    { 33L, 365, "MEASLES # 2", 14L },
                    { 34L, 274, "MenACWY # 1", 15L },
                    { 35L, 365, "MenACWY # 2", 15L },
                    { 36L, 274, "Yellow Fever # 1", 16L },
                    { 37L, 274, "Typhoid", 17L },
                    { 38L, 274, "MR#1", 18L },
                    { 39L, 365, "MR#2", 18L },
                    { 40L, 365, "Chicken Pox # 1", 19L },
                    { 41L, 456, "Chicken Pox # 2", 19L },
                    { 42L, 547, "Chicken Pox # 3", 19L },
                    { 43L, 365, "MMR # 1", 20L },
                    { 44L, 547, "MMR # 2", 20L },
                    { 45L, 4745, "MMR # 3", 20L },
                    { 46L, 730, "MMR # 4", 20L },
                    { 47L, 365, "Hepatitis A #1", 21L },
                    { 48L, 547, "Hepatitis A #2", 21L },
                    { 49L, 730, "PPSV/PCV", 22L },
                    { 50L, 1825, "DTaP # 1", 23L },
                    { 51L, 3650, "DTaP # 2", 23L },
                    { 52L, 3285, "HPV # 1", 25L },
                    { 53L, 3315, "HPV # 2", 25L },
                    { 54L, 3650, "Dengue Fever #1", 26L },
                    { 55L, 3833, "Dengue Fever #2", 26L }
                });

            migrationBuilder.InsertData(
                table: "ClinicTimings",
                columns: new[] { "Id", "ClinicId", "Day", "EndTime", "Session", "StartTime" },
                values: new object[] { 1L, 1L, "Monday", new TimeSpan(0, 3, 0, 0, 0), "Morning", new TimeSpan(0, 2, 0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_AdminSchedules_DoseId",
                table: "AdminSchedules",
                column: "DoseId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_VaccineId",
                table: "Brands",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_DoctorId",
                table: "Clinics",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicTimings_ClinicId",
                table: "ClinicTimings",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoseId",
                table: "DoctorSchedules",
                column: "DoseId");

            migrationBuilder.CreateIndex(
                name: "IX_Doses_VaccineId",
                table: "Doses",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSchedules_DoseId",
                table: "PatientSchedules",
                column: "DoseId");
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
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Childs");

            migrationBuilder.DropTable(
                name: "ClinicTimings");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "PatientSchedules");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Doses");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Vaccines");
        }
    }
}
