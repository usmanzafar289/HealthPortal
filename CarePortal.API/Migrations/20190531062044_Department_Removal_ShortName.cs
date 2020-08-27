using Microsoft.EntityFrameworkCore.Migrations;

namespace CarePortal.API.Migrations
{
    public partial class Department_Removal_ShortName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Department");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Department",
                type: "varchar(100)",
                nullable: true);
        }
    }
}
