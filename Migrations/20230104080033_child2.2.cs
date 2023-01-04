using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class child22 : Migration
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
                name: "ClinicId",
                table: "Childs");

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "Clinics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_ChildId",
                table: "Clinics",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_Childs_ChildId",
                table: "Clinics",
                column: "ChildId",
                principalTable: "Childs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_Childs_ChildId",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_ChildId",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "Clinics");

            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "Childs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Childs_ClinicId",
                table: "Childs",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Childs_Clinics_ClinicId",
                table: "Childs",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
