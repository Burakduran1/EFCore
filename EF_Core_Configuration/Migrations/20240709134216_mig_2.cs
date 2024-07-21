using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core_Configuration.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Departments_DepartmentId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "kisiler");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_DepartmentId",
                table: "kisiler",
                newName: "IX_kisiler_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_kisiler",
                table: "kisiler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_kisiler_Departments_DepartmentId",
                table: "kisiler",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kisiler_Departments_DepartmentId",
                table: "kisiler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kisiler",
                table: "kisiler");

            migrationBuilder.RenameTable(
                name: "kisiler",
                newName: "Persons");

            migrationBuilder.RenameIndex(
                name: "IX_kisiler_DepartmentId",
                table: "Persons",
                newName: "IX_Persons_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Departments_DepartmentId",
                table: "Persons",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
