using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoseBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VaccineId",
                table: "Doses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VaccineId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doses_VaccineId",
                table: "Doses",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_VaccineId",
                table: "Brands",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Vaccines_VaccineId",
                table: "Brands",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doses_Vaccines_VaccineId",
                table: "Doses",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Vaccines_VaccineId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Doses_Vaccines_VaccineId",
                table: "Doses");

            migrationBuilder.DropIndex(
                name: "IX_Doses_VaccineId",
                table: "Doses");

            migrationBuilder.DropIndex(
                name: "IX_Brands_VaccineId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "Doses");

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "Brands");
        }
    }
}
