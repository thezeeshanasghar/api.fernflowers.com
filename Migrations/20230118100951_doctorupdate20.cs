using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class doctorupdate20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandAmountId",
                table: "Brands",
                column: "BrandAmountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_BrandAmounts_BrandAmountId",
                table: "Brands",
                column: "BrandAmountId",
                principalTable: "BrandAmounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_BrandAmounts_BrandAmountId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BrandAmountId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "BrandAmounts");
        }
    }
}
