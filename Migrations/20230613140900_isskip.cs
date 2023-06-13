using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class isskip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSkip",
                table: "PatientSchedules",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ValidUpto",
                value: new DateTime(2023, 9, 13, 19, 9, 0, 258, DateTimeKind.Utc).AddTicks(5533));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSkip",
                table: "PatientSchedules");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ValidUpto",
                value: new DateTime(2023, 9, 9, 17, 7, 7, 411, DateTimeKind.Utc).AddTicks(164));
        }
    }
}
