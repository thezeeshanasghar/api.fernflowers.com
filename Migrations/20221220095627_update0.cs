using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class update0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpecial",
                table: "Vaccines",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecial",
                table: "Vaccines");
        }
    }
}
