using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate10 : Migration
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

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "PatientSchedules",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ValidUpto",
                value: new DateTime(2023, 10, 17, 12, 46, 49, 773, DateTimeKind.Utc).AddTicks(4290));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "PatientSchedules",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ValidUpto",
                value: new DateTime(2023, 10, 13, 17, 51, 25, 336, DateTimeKind.Utc).AddTicks(4440));

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
