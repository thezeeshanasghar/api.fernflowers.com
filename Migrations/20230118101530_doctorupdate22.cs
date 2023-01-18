using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class doctorupdate22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandAmountId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VaccineName",
                table: "BrandAmounts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_BrandAmountId",
                table: "Doctors",
                column: "BrandAmountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId",
                table: "Doctors",
                column: "BrandAmountId",
                principalTable: "BrandAmounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "BrandAmounts");

            migrationBuilder.DropColumn(
                name: "VaccineName",
                table: "BrandAmounts");
        }
    }
}
