using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoutineMaster.Data.Migrations
{
    public partial class ChangeExerciseRoutine2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleAmount",
                table: "ExerciseRoutines");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleAmount",
                table: "ExerciseRoutines",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
