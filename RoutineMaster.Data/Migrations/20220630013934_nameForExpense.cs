using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoutineMaster.Data.Migrations
{
    public partial class nameForExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ExpenseEntries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MundaneListItems_MundaneListId",
                table: "MundaneListItems",
                column: "MundaneListId");

            migrationBuilder.CreateIndex(
                name: "IX_CreativeProjectEntries_CreativeProjectId",
                table: "CreativeProjectEntries",
                column: "CreativeProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreativeProjectEntries_CreativeProjects_CreativeProjectId",
                table: "CreativeProjectEntries",
                column: "CreativeProjectId",
                principalTable: "CreativeProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MundaneListItems_MundaneLists_MundaneListId",
                table: "MundaneListItems",
                column: "MundaneListId",
                principalTable: "MundaneLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreativeProjectEntries_CreativeProjects_CreativeProjectId",
                table: "CreativeProjectEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MundaneListItems_MundaneLists_MundaneListId",
                table: "MundaneListItems");

            migrationBuilder.DropIndex(
                name: "IX_MundaneListItems_MundaneListId",
                table: "MundaneListItems");

            migrationBuilder.DropIndex(
                name: "IX_CreativeProjectEntries_CreativeProjectId",
                table: "CreativeProjectEntries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ExpenseEntries");
        }
    }
}
