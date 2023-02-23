using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class doseschedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Childs_Clinics_ClinicId",
                table: "Childs");

            migrationBuilder.DropIndex(
                name: "IX_Childs_ClinicId",
                table: "Childs");

            migrationBuilder.DropColumn(
                name: "PreferredDayOfReminder",
                table: "Childs");

            migrationBuilder.DropColumn(
                name: "PreferredDayOfWeek",
                table: "Childs");

            migrationBuilder.AlterColumn<int>(
                name: "ClinicId",
                table: "Childs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DoseSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    DoseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoseSchedules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoseSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "ClinicId",
                table: "Childs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PreferredDayOfReminder",
                table: "Childs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PreferredDayOfWeek",
                table: "Childs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Childs_ClinicId",
                table: "Childs",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Childs_Clinics_ClinicId",
                table: "Childs",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id");
        }
    }
}
