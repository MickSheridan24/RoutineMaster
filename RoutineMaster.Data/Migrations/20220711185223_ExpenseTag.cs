using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RoutineMaster.Data.Migrations
{
    public partial class ExpenseTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OverflowBudgetId",
                table: "Budgets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTags",
                columns: table => new
                {
                    ExpenseEntryId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTags", x => new { x.TagId, x.ExpenseEntryId });
                    table.ForeignKey(
                        name: "FK_ExpenseTags_ExpenseEntries_ExpenseEntryId",
                        column: x => x.ExpenseEntryId,
                        principalTable: "ExpenseEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserIncomes_UserId",
                table: "UserIncomes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseEntries_UserId",
                table: "ExpenseEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_OverflowBudgetId",
                table: "Budgets",
                column: "OverflowBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_SavingsAccountId",
                table: "Budgets",
                column: "SavingsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTags_ExpenseEntryId",
                table: "ExpenseTags",
                column: "ExpenseEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Budgets_OverflowBudgetId",
                table: "Budgets",
                column: "OverflowBudgetId",
                principalTable: "Budgets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_SavingsAccounts_SavingsAccountId",
                table: "Budgets",
                column: "SavingsAccountId",
                principalTable: "SavingsAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_UserId",
                table: "Budgets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseEntries_Users_UserId",
                table: "ExpenseEntries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserIncomes_Users_UserId",
                table: "UserIncomes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Budgets_OverflowBudgetId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_SavingsAccounts_SavingsAccountId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_UserId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseEntries_Users_UserId",
                table: "ExpenseEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_UserIncomes_Users_UserId",
                table: "UserIncomes");

            migrationBuilder.DropTable(
                name: "ExpenseTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_UserIncomes_UserId",
                table: "UserIncomes");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseEntries_UserId",
                table: "ExpenseEntries");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_OverflowBudgetId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_SavingsAccountId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "OverflowBudgetId",
                table: "Budgets");
        }
    }
}
