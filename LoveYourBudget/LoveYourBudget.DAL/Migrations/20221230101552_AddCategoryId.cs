using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoveYourBudget.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7134), new DateTime(2022, 12, 30, 11, 15, 52, 489, DateTimeKind.Local).AddTicks(7136) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
