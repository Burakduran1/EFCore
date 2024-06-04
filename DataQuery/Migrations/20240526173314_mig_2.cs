using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataQuery.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UrunId",
                table: "Parcalar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcalar_UrunId",
                table: "Parcalar",
                column: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcalar_Urunler_UrunId",
                table: "Parcalar",
                column: "UrunId",
                principalTable: "Urunler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcalar_Urunler_UrunId",
                table: "Parcalar");

            migrationBuilder.DropIndex(
                name: "IX_Parcalar_UrunId",
                table: "Parcalar");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Parcalar");
        }
    }
}
