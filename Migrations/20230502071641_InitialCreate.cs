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
                name: "AdminDoseSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DoseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminDoseSchedules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BrandAmounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
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
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
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
                    Guardian = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FatherName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNIC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PreferredSchedule = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEPIDone = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IsInactive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Childs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clinictimings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Session = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinictimings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Isapproved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DoctorType = table.Column<string>(type: "longtext", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DoseId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DoseSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DoseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoseSchedules", x => x.Id);
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
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VaccineId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MinAge = table.Column<int>(type: "int", nullable: false),
                    MinGap = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Address", "DoctorId", "Name", "Number" },
                values: new object[] { 1, "b17", 1, "Ali s clinic ", "3333333" });

            migrationBuilder.InsertData(
                table: "Clinictimings",
                columns: new[] { "Id", "ClinicId", "Day", "EndTime", "Session", "StartTime" },
                values: new object[] { 1, 1, "Monday", new TimeSpan(0, 3, 0, 0, 0), "one", new TimeSpan(0, 2, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "DoctorType", "Email", "IsEnabled", "Isapproved", "MobileNumber", "Name", "PMDC", "Password", "ValidUpto" },
                values: new object[] { 1, "Child Specialist", "ali.iiui1234@gmail.com", true, true, "03352920239", "Ali", "a1234", "123", new DateTime(2023, 8, 2, 12, 16, 40, 824, DateTimeKind.Utc).AddTicks(4503) });

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
                table: "Brands",
                columns: new[] { "Id", "Name", "VaccineId" },
                values: new object[,]
                {
                    { 1, "BCG", 1 },
                    { 2, "OPV", 2 },
                    { 3, "Local", 3 },
                    { 4, "AMVAX B", 3 },
                    { 5, "ENGERIX B", 3 },
                    { 6, "ENGERIX B", 4 },
                    { 7, "INDIRAB", 5 },
                    { 8, "VERORAB", 5 },
                    { 9, "VERORAB", 6 },
                    { 10, "IMATET", 7 },
                    { 11, "TT", 7 },
                    { 12, "Tetanus", 7 },
                    { 13, "ROTARIX", 8 },
                    { 14, "Rota Teq", 8 },
                    { 15, "ROTARIX*", 8 },
                    { 16, "INFANRIX HEXA", 9 },
                    { 17, "HEXAXIM", 9 },
                    { 18, "QUINAVAXEM + OPV", 9 },
                    { 19, "EUPENTA + OPV", 9 },
                    { 20, "PENTA+OPV", 9 },
                    { 21, "PREVENAR*", 10 },
                    { 22, "SYNFLORIX", 10 },
                    { 23, "PREVENAR", 10 },
                    { 24, "PENTA ( amson)", 11 },
                    { 25, "Men B", 12 },
                    { 26, "INFLUVAC", 13 },
                    { 27, "FLUARIX", 13 },
                    { 28, "VAXIGRIP", 13 },
                    { 29, "ANFLU", 13 },
                    { 30, "FLU", 13 },
                    { 31, "MEASLES", 14 },
                    { 32, "MENACTRA", 15 },
                    { 33, "NIMENRIX", 15 },
                    { 34, "STAMARIL", 16 },
                    { 35, "TYPHIRIX", 17 },
                    { 36, "TYPBAR", 17 },
                    { 37, "TYPBAR TCV", 17 },
                    { 38, "TYPBAR*", 17 },
                    { 39, "TYPHOID", 17 },
                    { 40, "MR", 18 },
                    { 41, "VARIVAC", 19 },
                    { 42, "VARILRIX", 19 },
                    { 43, "VAXAPOX", 19 },
                    { 44, "PRIORIX TETRA", 19 },
                    { 45, "VARICELLA", 19 },
                    { 46, "TRIMOVAX", 20 },
                    { 47, "PRIORIX", 20 },
                    { 48, "MMR", 20 },
                    { 49, "PRIORIX TETRA", 20 },
                    { 50, "TRESIVAC", 20 },
                    { 51, "HAVRIX", 21 },
                    { 52, "TWINRIX", 21 },
                    { 53, "AVAXIM", 21 },
                    { 54, "PCV", 22 },
                    { 55, "PNEUMOVAX", 22 },
                    { 56, "PREVENAR", 22 },
                    { 57, "BOOSTRIX", 23 },
                    { 58, "BOOSTRIX", 23 },
                    { 59, "Local", 24 },
                    { 60, "Pfizer", 24 },
                    { 61, "SinoVac", 24 },
                    { 62, "Local", 25 },
                    { 63, "CERVARIX", 25 },
                    { 64, "Dengvaxia", 26 }
                });

            migrationBuilder.InsertData(
                table: "Doses",
                columns: new[] { "Id", "MinAge", "MinGap", "Name", "VaccineId" },
                values: new object[,]
                {
                    { 1, 0, 0, "BCG", 1 },
                    { 2, 0, 0, "OPV # 1", 2 },
                    { 3, 0, 0, "HBV", 3 },
                    { 4, 0, 0, "Hep B 1", 4 },
                    { 5, 28, 28, "Hep B 2", 4 },
                    { 6, 168, 140, "Hep B 3", 4 },
                    { 7, 0, 0, "Pre-Exp Rabies-1", 5 },
                    { 8, 1, 1, "Pre-Exp Rabies-2", 5 },
                    { 9, 28, 21, "Pre-Exp Rabies-3", 5 },
                    { 10, 0, 0, "RABIES#1", 6 },
                    { 11, 4, 3, "RABIES#2", 6 },
                    { 12, 8, 4, "RABIES#3", 6 },
                    { 13, 28, 21, "RABIES#4", 6 },
                    { 14, 0, 0, "Tetanus", 7 },
                    { 15, 42, 0, "Rota Virus GE # 1", 8 },
                    { 16, 70, 28, "Rota Virus GE # 2", 8 },
                    { 17, 98, 28, "Rota Virus GE # 3", 8 },
                    { 18, 42, 0, "OPV/IPV+HBV+DPT+Hib # 1", 9 },
                    { 19, 70, 28, "OPV/IPV+HBV+DPT+Hib # 2", 9 },
                    { 20, 98, 28, "OPV/IPV+HBV+DPT+Hib # 3", 9 },
                    { 21, 365, 168, "OPV/IPV+HBV+DPT+Hib # 4", 9 },
                    { 22, 42, 0, "Pneumococcal # 1", 10 },
                    { 23, 70, 28, "Pneumococcal # 2", 10 },
                    { 24, 98, 28, "Pneumococcal # 3", 10 },
                    { 25, 365, 168, "Pneumococcal # 4", 10 },
                    { 26, 42, 0, "HBV+DPT+Hib # 1", 11 },
                    { 27, 70, 28, "HBV+DPT+Hib # 2", 11 },
                    { 28, 98, 28, "HBV+DPT+Hib # 3", 11 },
                    { 29, 56, 0, "MenB Vaccine # 1", 12 },
                    { 30, 112, 56, "MenB Vaccine # 2", 12 },
                    { 31, 168, 0, "Flu", 13 },
                    { 32, 168, 0, "MEASLES # 1", 14 },
                    { 33, 365, 168, "MEASLES # 2", 14 },
                    { 34, 274, 0, "MenACWY # 1", 15 },
                    { 35, 365, 84, "MenACWY # 2", 15 },
                    { 36, 274, 0, "Yellow Fever # 1", 16 },
                    { 37, 274, 0, "Typhoid", 17 },
                    { 38, 274, 0, "MR#1", 18 },
                    { 39, 365, 84, "MR#2", 18 },
                    { 40, 365, 0, "Chicken Pox # 1", 19 },
                    { 41, 456, 84, "Chicken Pox # 2", 19 },
                    { 42, 547, 0, "Chicken Pox # 3", 19 },
                    { 43, 365, 0, "MMR # 1", 20 },
                    { 44, 547, 168, "MMR # 2", 20 },
                    { 45, 4745, 0, "MMR # 3", 20 },
                    { 46, 730, 0, "MMR # 4", 20 },
                    { 47, 365, 0, "Hepatitis A #1", 21 },
                    { 48, 547, 168, "Hepatitis A #2", 21 },
                    { 49, 730, 0, "PPSV/PCV", 22 },
                    { 50, 1825, 0, "DTaP # 1", 23 },
                    { 51, 3650, 1825, "DTaP # 2", 23 },
                    { 52, 3285, 0, "HPV # 1", 25 },
                    { 53, 3315, 28, "HPV # 2", 25 },
                    { 54, 3650, 0, "Dengue Fever #1", 26 },
                    { 55, 3833, 168, "Dengue Fever #2", 26 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_VaccineId",
                table: "Brands",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Doses_VaccineId",
                table: "Doses",
                column: "VaccineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminDoseSchedules");

            migrationBuilder.DropTable(
                name: "BrandAmounts");

            migrationBuilder.DropTable(
                name: "BrandInventories");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Childs");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Clinictimings");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "Doses");

            migrationBuilder.DropTable(
                name: "DoseSchedules");

            migrationBuilder.DropTable(
                name: "Vaccines");
        }
    }
}
