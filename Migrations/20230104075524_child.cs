using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class child : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandAmounts_Brands_BrandId",
                table: "BrandAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandAmounts_Doctors_DoctorId",
                table: "BrandAmounts");

            migrationBuilder.DropIndex(
                name: "IX_BrandAmounts_BrandId",
                table: "BrandAmounts");

            migrationBuilder.DropIndex(
                name: "IX_BrandAmounts_DoctorId",
                table: "BrandAmounts");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

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
                    PreferredDayOfReminder = table.Column<int>(type: "int", nullable: false),
                    PreferredDayOfWeek = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PreferredSchedule = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEPIDone = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IsInactive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ClinicId = table.Column<long>(type: "bigint", nullable: false),
                    ClinicId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Childs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Childs_Clinics_ClinicId1",
                        column: x => x.ClinicId1,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_BrandAmountId",
                table: "Doctors",
                column: "BrandAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandAmountId",
                table: "Brands",
                column: "BrandAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_Childs_ClinicId1",
                table: "Childs",
                column: "ClinicId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_BrandAmounts_BrandAmountId",
                table: "Brands",
                column: "BrandAmountId",
                principalTable: "BrandAmounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId",
                table: "Doctors",
                column: "BrandAmountId",
                principalTable: "BrandAmounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_BrandAmounts_BrandAmountId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Childs");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BrandAmountId",
                table: "Brands");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "BrandAmounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_BrandId",
                table: "BrandAmounts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_DoctorId",
                table: "BrandAmounts",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandAmounts_Brands_BrandId",
                table: "BrandAmounts",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandAmounts_Doctors_DoctorId",
                table: "BrandAmounts",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
