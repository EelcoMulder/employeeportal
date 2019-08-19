using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeePortal.TimeRegistration.Infrastructure.Migrations
{
    public partial class ChangeUserIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "TimeSheets",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TimeSheets",
                newName: "UserName");
        }
    }
}
