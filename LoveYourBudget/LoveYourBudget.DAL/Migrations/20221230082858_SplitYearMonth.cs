using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoveYourBudget.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SplitYearMonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Income = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Mortgage = table.Column<int>(type: "int", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    LockInPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BudgetId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budget",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BudgetId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetRow_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budget",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BudgetRow_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BudgetId = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseRows_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budget",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpenseRows_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedTime", "Name", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4373), "Groceries", new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4409) },
                    { 2, new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4415), "Phone", new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4418) },
                    { 3, new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4423), "Electricity", new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4426) },
                    { 4, new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4430), "Gas", new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4433) },
                    { 5, new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4437), "Broadband", new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4465) },
                    { 6, new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4472), "TV", new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4476) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRow_BudgetId",
                table: "BudgetRow",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRow_CategoryId",
                table: "BudgetRow",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseRows_BudgetId",
                table: "ExpenseRows",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseRows_CategoryId",
                table: "ExpenseRows",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BudgetId",
                table: "Loans",
                column: "BudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetRow");

            migrationBuilder.DropTable(
                name: "ExpenseRows");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Budget");
        }
    }
}
