using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinGap",
                table: "Doses");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ValidUpto",
                value: new DateTime(2023, 10, 8, 10, 38, 15, 181, DateTimeKind.Utc).AddTicks(8674));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinGap",
                table: "Doses",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ValidUpto",
                value: new DateTime(2023, 10, 6, 10, 46, 11, 332, DateTimeKind.Utc).AddTicks(4690));

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 4L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 5L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 6L,
                column: "MinGap",
                value: 140);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 7L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 8L,
                column: "MinGap",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 9L,
                column: "MinGap",
                value: 21);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 10L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 11L,
                column: "MinGap",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 12L,
                column: "MinGap",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 13L,
                column: "MinGap",
                value: 21);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 14L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 15L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 16L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 17L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 18L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 19L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 20L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 21L,
                column: "MinGap",
                value: 168);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 22L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 23L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 24L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 25L,
                column: "MinGap",
                value: 168);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 26L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 27L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 28L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 29L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 30L,
                column: "MinGap",
                value: 56);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 31L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 32L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 33L,
                column: "MinGap",
                value: 168);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 34L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 35L,
                column: "MinGap",
                value: 84);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 36L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 37L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 38L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 39L,
                column: "MinGap",
                value: 84);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 40L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 41L,
                column: "MinGap",
                value: 84);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 42L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 43L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 44L,
                column: "MinGap",
                value: 168);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 45L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 46L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 47L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 48L,
                column: "MinGap",
                value: 168);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 49L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 50L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 51L,
                column: "MinGap",
                value: 1825);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 52L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 53L,
                column: "MinGap",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 54L,
                column: "MinGap",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Doses",
                keyColumn: "Id",
                keyValue: 55L,
                column: "MinGap",
                value: 168);
        }
    }
}
