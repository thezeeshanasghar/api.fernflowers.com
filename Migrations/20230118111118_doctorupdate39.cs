using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class doctorupdate39 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "BrandAmountId1",
                table: "Doctors",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandAmountId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "BrandAmountId1",
                table: "Brands",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_BrandAmountId1",
                table: "Doctors",
                column: "BrandAmountId1");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandAmountId1",
                table: "Brands",
                column: "BrandAmountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_BrandAmounts_BrandAmountId1",
                table: "Brands",
                column: "BrandAmountId1",
                principalTable: "BrandAmounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId1",
                table: "Doctors",
                column: "BrandAmountId1",
                principalTable: "BrandAmounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_BrandAmounts_BrandAmountId1",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId1",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_BrandAmountId1",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BrandAmountId1",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BrandAmountId1",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BrandAmountId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BrandAmountId1",
                table: "Brands");

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
    }
}
