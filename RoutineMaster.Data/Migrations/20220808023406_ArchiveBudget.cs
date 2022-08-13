using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoutineMaster.Data.Migrations
{
    public partial class ArchiveBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Budgets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Budgets");
        }
    }
}
