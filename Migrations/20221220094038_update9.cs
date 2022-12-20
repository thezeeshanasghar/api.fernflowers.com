using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class update9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Infinite",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "IsSpecial",
                table: "Vaccines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Infinite",
                table: "Vaccines",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecial",
                table: "Vaccines",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
