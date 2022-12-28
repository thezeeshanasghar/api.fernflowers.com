using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoseBrand2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doses_Vaccines_VaccineId",
                table: "Doses");

            migrationBuilder.AlterColumn<int>(
                name: "VaccineId",
                table: "Doses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_Doses_Vaccines_VaccineId",
                table: "Doses");

            migrationBuilder.AlterColumn<int>(
                name: "VaccineId",
                table: "Doses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doses_Vaccines_VaccineId",
                table: "Doses",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id");
        }
    }
}
