using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoutineMaster.Data.Migrations
{
    public partial class ChangeExerciseRoutine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scale",
                table: "ExerciseRoutines");

            migrationBuilder.RenameColumn(
                name: "BaseAmount",
                table: "ExerciseRoutines",
                newName: "ScheduleType");

            migrationBuilder.AddColumn<int>(
                name: "RequiredOccurrences",
                table: "ExerciseRoutines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleAmount",
                table: "ExerciseRoutines",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredOccurrences",
                table: "ExerciseRoutines");

            migrationBuilder.DropColumn(
                name: "ScheduleAmount",
                table: "ExerciseRoutines");

            migrationBuilder.RenameColumn(
                name: "ScheduleType",
                table: "ExerciseRoutines",
                newName: "BaseAmount");

            migrationBuilder.AddColumn<double>(
                name: "Scale",
                table: "ExerciseRoutines",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
