using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class doctorupdate30 : Migration
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
                name: "BrandAmountId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "VaccineName",
                table: "BrandAmounts");

            migrationBuilder.AlterColumn<long>(
                name: "DoctorId",
                table: "BrandAmounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "BrandId",
                table: "BrandAmounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BrandId1",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId1",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_BrandId1",
                table: "BrandAmounts",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_DoctorId1",
                table: "BrandAmounts",
                column: "DoctorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandAmounts_Brands_BrandId1",
                table: "BrandAmounts",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandAmounts_Doctors_DoctorId1",
                table: "BrandAmounts",
                column: "DoctorId1",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandAmounts_Brands_BrandId1",
                table: "BrandAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandAmounts_Doctors_DoctorId1",
                table: "BrandAmounts");

            migrationBuilder.DropIndex(
                name: "IX_BrandAmounts_BrandId1",
                table: "BrandAmounts");

            migrationBuilder.DropIndex(
                name: "IX_BrandAmounts_DoctorId1",
                table: "BrandAmounts");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "BrandAmounts");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "BrandAmounts");

            migrationBuilder.AddColumn<int>(
                name: "BrandAmountId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandAmountId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
                principalColumn: "Id");
        }
    }
}
