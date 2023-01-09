using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoveYourBudget.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBudgetConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetRow_Budget_BudgetId",
                table: "BudgetRow");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseRows_Budget_BudgetId",
                table: "ExpenseRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Budget_BudgetId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BudgetId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseRows_BudgetId",
                table: "ExpenseRows");

            migrationBuilder.DropIndex(
                name: "IX_BudgetRow_BudgetId",
                table: "BudgetRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Budget",
                table: "Budget");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "ExpenseRows");

            migrationBuilder.AddColumn<string>(
                name: "Institute",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "BudgetRow",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BudgetMonth",
                table: "BudgetRow",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BudgetYear",
                table: "BudgetRow",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "Budget",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "Budget",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Budget",
                table: "Budget",
                columns: new[] { "Year", "Month" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6772), new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6775) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6781), new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6783) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6787), new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6789) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6793), new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6795) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6799), new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6801) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "Name", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6805), "Streaming", new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6807) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedTime", "Name", "UpdatedTime" },
                values: new object[,]
                {
                    { 7, new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6811), "Transportation", new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6813) },
                    { 8, new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6817), "Restaurants", new DateTime(2023, 1, 9, 19, 48, 50, 964, DateTimeKind.Local).AddTicks(6819) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRow_BudgetYear_BudgetMonth",
                table: "BudgetRow",
                columns: new[] { "BudgetYear", "BudgetMonth" });

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetRow_Budget_BudgetYear_BudgetMonth",
                table: "BudgetRow",
                columns: new[] { "BudgetYear", "BudgetMonth" },
                principalTable: "Budget",
                principalColumns: new[] { "Year", "Month" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetRow_Budget_BudgetYear_BudgetMonth",
                table: "BudgetRow");

            migrationBuilder.DropIndex(
                name: "IX_BudgetRow_BudgetYear_BudgetMonth",
                table: "BudgetRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Budget",
                table: "Budget");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Institute",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BudgetMonth",
                table: "BudgetRow");

            migrationBuilder.DropColumn(
                name: "BudgetYear",
                table: "BudgetRow");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "Loans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "ExpenseRows",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "BudgetRow",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "Budget",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "Budget",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Budget",
                table: "Budget",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7103), new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7105) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7111), new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7113) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7117), new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7119) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7123), new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7124) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7128), new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "Name", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7134), "TV", new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7136) });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BudgetId",
                table: "Loans",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseRows_BudgetId",
                table: "ExpenseRows",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRow_BudgetId",
                table: "BudgetRow",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetRow_Budget_BudgetId",
                table: "BudgetRow",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseRows_Budget_BudgetId",
                table: "ExpenseRows",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Budget_BudgetId",
                table: "Loans",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "Id");
        }
    }
}
