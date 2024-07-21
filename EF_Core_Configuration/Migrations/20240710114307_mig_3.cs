using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core_Configuration.Migrations
{
    public partial class mig_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kisiler_Departments_DepartmentId",
                table: "kisiler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kisiler",
                table: "kisiler");

            migrationBuilder.RenameTable(
                name: "kisiler",
                newName: "ToTablee");

            migrationBuilder.RenameIndex(
                name: "IX_kisiler_DepartmentId",
                table: "ToTablee",
                newName: "IX_ToTablee_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToTablee",
                table: "ToTablee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToTablee_Departments_DepartmentId",
                table: "ToTablee",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToTablee_Departments_DepartmentId",
                table: "ToTablee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToTablee",
                table: "ToTablee");

            migrationBuilder.RenameTable(
                name: "ToTablee",
                newName: "kisiler");

            migrationBuilder.RenameIndex(
                name: "IX_ToTablee_DepartmentId",
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
    }
}
