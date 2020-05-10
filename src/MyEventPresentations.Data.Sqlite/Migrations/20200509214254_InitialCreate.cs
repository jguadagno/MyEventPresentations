using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEventPresentations.Data.Sqlite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Presentations",
                columns: table => new
                {
                    PresentationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Abstract = table.Column<string>(nullable: true),
                    MoreInfoUri = table.Column<string>(nullable: true),
                    SourceCodeRepositoryUri = table.Column<string>(nullable: true),
                    PowerpointUri = table.Column<string>(nullable: true),
                    VideoUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentations", x => x.PresentationId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledPresentations",
                columns: table => new
                {
                    ScheduledPresentationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PresentationId = table.Column<int>(nullable: true),
                    PresentationUri = table.Column<string>(nullable: true),
                    VideoStorageUri = table.Column<string>(nullable: true),
                    VideoUri = table.Column<string>(nullable: true),
                    AttendeeCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledPresentations", x => x.ScheduledPresentationId);
                    table.ForeignKey(
                        name: "FK_ScheduledPresentations_Presentations_PresentationId",
                        column: x => x.PresentationId,
                        principalTable: "Presentations",
                        principalColumn: "PresentationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPresentations_PresentationId",
                table: "ScheduledPresentations",
                column: "PresentationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledPresentations");

            migrationBuilder.DropTable(
                name: "Presentations");
        }
    }
}
