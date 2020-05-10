using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEventPresentations.Data.Sqlite.Migrations
{
    public partial class NewFieldsForSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "ScheduledPresentations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "ScheduledPresentations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "ScheduledPresentations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ScheduledPresentations");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "ScheduledPresentations");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "ScheduledPresentations");
        }
    }
}
