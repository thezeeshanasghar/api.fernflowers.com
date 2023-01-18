using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class doctorupdate19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_BrandAmounts_BrandAmountId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BrandAmountId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "BrandAmounts");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "BrandAmounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandAmountId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_BrandAmountId",
                table: "Doctors",
                column: "BrandAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandAmountId",
                table: "Brands",
                column: "BrandAmountId");

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
    }
}
