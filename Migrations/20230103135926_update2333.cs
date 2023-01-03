using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.fernflowers.com.Migrations
{
    /// <inheritdoc />
    public partial class update2333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandAmounts_Brands_BrandId1",
                table: "BrandAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandAmounts_Doctors_DoctorId1",
                table: "BrandAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandInventories_Brands_BrandId1",
                table: "BrandInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandInventories_Doctors_DoctorId1",
                table: "BrandInventories");

            migrationBuilder.DropIndex(
                name: "IX_BrandInventories_BrandId1",
                table: "BrandInventories");

            migrationBuilder.DropIndex(
                name: "IX_BrandInventories_DoctorId1",
                table: "BrandInventories");

            migrationBuilder.DropIndex(
                name: "IX_BrandAmounts_BrandId1",
                table: "BrandAmounts");

            migrationBuilder.DropIndex(
                name: "IX_BrandAmounts_DoctorId1",
                table: "BrandAmounts");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "BrandInventories");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "BrandInventories");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "BrandAmounts");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "BrandAmounts");

            migrationBuilder.AddColumn<int>(
                name: "BrandAmountId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrandAmountId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "BrandInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "BrandInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_BrandInventories_BrandId",
                table: "BrandInventories",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandInventories_DoctorId",
                table: "BrandInventories",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_BrandId",
                table: "BrandAmounts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_DoctorId",
                table: "BrandAmounts",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandAmounts_Brands_BrandId",
                table: "BrandAmounts",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandAmounts_Doctors_DoctorId",
                table: "BrandAmounts",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandInventories_Brands_BrandId",
                table: "BrandInventories",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandInventories_Doctors_DoctorId",
                table: "BrandInventories",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandAmounts_Brands_BrandId",
                table: "BrandAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandAmounts_Doctors_DoctorId",
                table: "BrandAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandInventories_Brands_BrandId",
                table: "BrandInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandInventories_Doctors_DoctorId",
                table: "BrandInventories");

            migrationBuilder.DropIndex(
                name: "IX_BrandInventories_BrandId",
                table: "BrandInventories");

            migrationBuilder.DropIndex(
                name: "IX_BrandInventories_DoctorId",
                table: "BrandInventories");

            migrationBuilder.DropIndex(
                name: "IX_BrandAmounts_BrandId",
                table: "BrandAmounts");

            migrationBuilder.DropIndex(
                name: "IX_BrandAmounts_DoctorId",
                table: "BrandAmounts");

            migrationBuilder.DropColumn(
                name: "BrandAmountId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BrandAmountId",
                table: "Brands");

            migrationBuilder.AlterColumn<long>(
                name: "DoctorId",
                table: "BrandInventories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "BrandId",
                table: "BrandInventories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BrandId1",
                table: "BrandInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId1",
                table: "BrandInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "DoctorId",
                table: "BrandAmounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "BrandId",
                table: "BrandAmounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BrandId1",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId1",
                table: "BrandAmounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BrandInventories_BrandId1",
                table: "BrandInventories",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_BrandInventories_DoctorId1",
                table: "BrandInventories",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_BrandId1",
                table: "BrandAmounts",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_BrandAmounts_DoctorId1",
                table: "BrandAmounts",
                column: "DoctorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandAmounts_Brands_BrandId1",
                table: "BrandAmounts",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandAmounts_Doctors_DoctorId1",
                table: "BrandAmounts",
                column: "DoctorId1",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandInventories_Brands_BrandId1",
                table: "BrandInventories",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandInventories_Doctors_DoctorId1",
                table: "BrandInventories",
                column: "DoctorId1",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
