using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoutineMaster.Data.Migrations
{
    public partial class DateRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MealRatings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Date",
                table: "MealRatings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
