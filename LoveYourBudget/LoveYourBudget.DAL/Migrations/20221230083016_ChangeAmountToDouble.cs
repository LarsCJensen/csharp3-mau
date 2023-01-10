using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveYourBudget.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAmountToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "ExpenseRows",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "BudgetRow",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6502), new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6565) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6571), new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6574) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6578), new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6581) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6585), new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6589) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6593), new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6597) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6601), new DateTime(2022, 12, 30, 9, 30, 16, 238, DateTimeKind.Local).AddTicks(6604) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "ExpenseRows",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "BudgetRow",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4373), new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4409) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4415), new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4418) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4423), new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4426) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4430), new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4433) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4437), new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4465) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4472), new DateTime(2022, 12, 30, 9, 28, 58, 227, DateTimeKind.Local).AddTicks(4476) });
        }
    }
}
