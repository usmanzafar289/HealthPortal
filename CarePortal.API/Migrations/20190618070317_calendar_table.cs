using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarePortal.API.Migrations
{
    public partial class calendar_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentTime",
                table: "Calendar",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndTime",
                table: "Calendar",
                type: "datetimeoffset(7)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Calendar",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Calendar");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Calendar",
                newName: "AppointmentTime");
        }
    }
}
