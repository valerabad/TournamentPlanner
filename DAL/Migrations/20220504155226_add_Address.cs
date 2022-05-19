using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class add_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 5, 8, 18, 52, 25, 634, DateTimeKind.Local).AddTicks(3967), new DateTime(2022, 5, 5, 18, 52, 25, 630, DateTimeKind.Local).AddTicks(4822) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 5, 12, 18, 52, 25, 634, DateTimeKind.Local).AddTicks(5021), new DateTime(2022, 5, 8, 18, 52, 25, 634, DateTimeKind.Local).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 6, 15, 18, 52, 25, 634, DateTimeKind.Local).AddTicks(5106), new DateTime(2022, 6, 11, 18, 52, 25, 634, DateTimeKind.Local).AddTicks(5103) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 5, 8, 18, 51, 24, 772, DateTimeKind.Local).AddTicks(8132), new DateTime(2022, 5, 5, 18, 51, 24, 771, DateTimeKind.Local).AddTicks(963) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 5, 12, 18, 51, 24, 772, DateTimeKind.Local).AddTicks(9231), new DateTime(2022, 5, 8, 18, 51, 24, 772, DateTimeKind.Local).AddTicks(9223) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 6, 15, 18, 51, 24, 772, DateTimeKind.Local).AddTicks(9237), new DateTime(2022, 6, 11, 18, 51, 24, 772, DateTimeKind.Local).AddTicks(9235) });
        }
    }
}
