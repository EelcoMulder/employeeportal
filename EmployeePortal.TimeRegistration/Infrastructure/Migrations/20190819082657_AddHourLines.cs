using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeePortal.TimeRegistration.Infrastructure.Migrations
{
    public partial class AddHourLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HourLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeSheetId = table.Column<int>(nullable: false),
                    DayOfMonth = table.Column<byte>(nullable: false),
                    Hours = table.Column<byte>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourLines_TimeSheets_TimeSheetId",
                        column: x => x.TimeSheetId,
                        principalTable: "TimeSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HourLines_TimeSheetId",
                table: "HourLines",
                column: "TimeSheetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourLines");
        }
    }
}
