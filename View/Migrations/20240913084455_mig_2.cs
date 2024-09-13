using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace View.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@" 
                     CREATE VIEW OrderCount 
                        AS
	                        SELECT TOP 100 Name, COUNT(OrderId) as Adet FROM Persons
	                        INNER JOIN Orders ON Persons.PersonId = Orders.PersonId
	                        GROUP BY Name
                            ORDER By Adet asc
                    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP VIEW OrderCount");
        }
    }
}
