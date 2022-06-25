using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RoutineMaster.Data.Migrations
{
    public partial class modelTweaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MundaneRoutineEntries_MundaneRoutine_RoutineId",
                table: "MundaneRoutineEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MundaneRoutine",
                table: "MundaneRoutine");

            migrationBuilder.RenameTable(
                name: "MundaneRoutine",
                newName: "MundaneRoutines");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ReadingEntries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExpenseEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MundaneRoutines",
                table: "MundaneRoutines",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    IncidentalBonus = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIncomes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MundaneRoutineEntries_MundaneRoutines_RoutineId",
                table: "MundaneRoutineEntries",
                column: "RoutineId",
                principalTable: "MundaneRoutines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MundaneRoutineEntries_MundaneRoutines_RoutineId",
                table: "MundaneRoutineEntries");

            migrationBuilder.DropTable(
                name: "UserIncomes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MundaneRoutines",
                table: "MundaneRoutines");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ReadingEntries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExpenseEntries");

            migrationBuilder.RenameTable(
                name: "MundaneRoutines",
                newName: "MundaneRoutine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MundaneRoutine",
                table: "MundaneRoutine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MundaneRoutineEntries_MundaneRoutine_RoutineId",
                table: "MundaneRoutineEntries",
                column: "RoutineId",
                principalTable: "MundaneRoutine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
