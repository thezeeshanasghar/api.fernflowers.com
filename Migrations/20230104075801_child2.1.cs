using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class child21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Childs_Clinics_ClinicId1",
                table: "Childs");

            migrationBuilder.DropIndex(
                name: "IX_Childs_ClinicId1",
                table: "Childs");

            migrationBuilder.DropColumn(
                name: "ClinicId1",
                table: "Childs");

            migrationBuilder.AlterColumn<int>(
                name: "ClinicId",
                table: "Childs",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Childs_Clinics_ClinicId",
                table: "Childs");

            migrationBuilder.DropIndex(
                name: "IX_Childs_ClinicId",
                table: "Childs");

            migrationBuilder.AlterColumn<long>(
                name: "ClinicId",
                table: "Childs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClinicId1",
                table: "Childs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Childs_ClinicId1",
                table: "Childs",
                column: "ClinicId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Childs_Clinics_ClinicId1",
                table: "Childs",
                column: "ClinicId1",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
