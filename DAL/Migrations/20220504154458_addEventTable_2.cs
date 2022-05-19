using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addEventTable_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fee = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 5, 8, 18, 44, 57, 731, DateTimeKind.Local).AddTicks(2211), new DateTime(2022, 5, 5, 18, 44, 57, 729, DateTimeKind.Local).AddTicks(5105) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 5, 12, 18, 44, 57, 731, DateTimeKind.Local).AddTicks(2883), new DateTime(2022, 5, 8, 18, 44, 57, 731, DateTimeKind.Local).AddTicks(2876) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 6, 15, 18, 44, 57, 731, DateTimeKind.Local).AddTicks(2889), new DateTime(2022, 6, 11, 18, 44, 57, 731, DateTimeKind.Local).AddTicks(2887) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 5, 8, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(741), new DateTime(2022, 5, 5, 18, 41, 29, 456, DateTimeKind.Local).AddTicks(1285) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 5, 12, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(1698), new DateTime(2022, 5, 8, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(1687) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateEnd", "DateStart" },
                values: new object[] { new DateTime(2022, 6, 15, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(1706), new DateTime(2022, 6, 11, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(1703) });
        }
    }
}
