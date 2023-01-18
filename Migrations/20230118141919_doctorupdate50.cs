using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class doctorupdate50 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId1",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_BrandAmountId1",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BrandAmountId1",
                table: "Doctors");

            migrationBuilder.CreateTable(
                name: "BrandAmountDoctor",
                columns: table => new
                {
                    BrandAmountsId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandAmountDoctor", x => new { x.BrandAmountsId, x.DoctorsId });
                    table.ForeignKey(
                        name: "FK_BrandAmountDoctor_BrandAmounts_BrandAmountsId",
                        column: x => x.BrandAmountsId,
                        principalTable: "BrandAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandAmountDoctor_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmountDoctor_DoctorsId",
                table: "BrandAmountDoctor",
                column: "DoctorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandAmountDoctor");

            migrationBuilder.AddColumn<long>(
                name: "BrandAmountId1",
                table: "Doctors",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_BrandAmountId1",
                table: "Doctors",
                column: "BrandAmountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_BrandAmounts_BrandAmountId1",
                table: "Doctors",
                column: "BrandAmountId1",
                principalTable: "BrandAmounts",
                principalColumn: "Id");
        }
    }
}
