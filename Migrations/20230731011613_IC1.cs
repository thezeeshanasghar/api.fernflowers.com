using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class IC1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AdminSchedules_DoseId",
                table: "AdminSchedules",
                column: "DoseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminSchedules_Doses_DoseId",
                table: "AdminSchedules",
                column: "DoseId",
                principalTable: "Doses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminSchedules_Doses_DoseId",
                table: "AdminSchedules");

            migrationBuilder.DropIndex(
                name: "IX_AdminSchedules_DoseId",
                table: "AdminSchedules");
        }
    }
}
