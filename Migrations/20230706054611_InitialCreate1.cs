using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Vaccines_VaccineId1",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Doses_Vaccines_VaccineId1",
                table: "Doses");

            migrationBuilder.DropIndex(
                name: "IX_Doses_VaccineId1",
                table: "Doses");

            migrationBuilder.DropIndex(
                name: "IX_Brands_VaccineId1",
                table: "Brands");

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DropColumn(
                name: "VaccineId1",
                table: "Doses");

            migrationBuilder.DropColumn(
                name: "VaccineId1",
                table: "Brands");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Vaccines",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Childs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ValidUpto",
                value: new DateTime(2023, 10, 6, 10, 46, 11, 332, DateTimeKind.Utc).AddTicks(4690));

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doses_Vaccines_VaccineId",
                table: "Doses",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Vaccines",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "VaccineId1",
                table: "Doses",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Childs",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "VaccineId1",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 21L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 22L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 23L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 24L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 25L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 26L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 27L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 28L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 29L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 30L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 31L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 32L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 33L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 34L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 35L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 36L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 37L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 38L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 39L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 40L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 41L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 42L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 43L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 44L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 45L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 46L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 47L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 48L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 49L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 50L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 51L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 52L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 53L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 54L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 55L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 56L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 57L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 58L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 59L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 60L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 61L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 62L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 63L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 64L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ValidUpto",
                value: new DateTime(2023, 10, 6, 10, 0, 57, 584, DateTimeKind.Utc).AddTicks(389));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 4L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 5L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 6L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 7L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 8L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 9L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 10L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 11L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 12L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 13L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 14L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 15L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 16L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 17L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 18L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 19L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 20L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 21L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 22L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 23L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 24L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 25L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 26L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 27L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 28L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 29L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 30L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 31L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 32L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 33L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 34L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 35L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 36L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 37L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 38L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 39L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 40L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 41L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 42L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 43L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 44L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 45L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 46L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 47L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 48L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 49L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 50L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 51L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 52L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 53L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 54L,
                column: "VaccineId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 55L,
                column: "VaccineId1",
                value: null);

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

            migrationBuilder.CreateIndex(
                name: "IX_Doses_VaccineId1",
                table: "Doses",
                column: "VaccineId1");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_VaccineId1",
                table: "Brands",
                column: "VaccineId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Vaccines_VaccineId1",
                table: "Brands",
                column: "VaccineId1",
                principalTable: "Vaccines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doses_Vaccines_VaccineId1",
                table: "Doses",
                column: "VaccineId1",
                principalTable: "Vaccines",
                principalColumn: "Id");
        }
    }
}
