using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UPD_Init_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Test = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
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
    }
}
