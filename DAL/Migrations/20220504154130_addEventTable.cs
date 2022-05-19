using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Tournament_TournamentsId",
                table: "PlayerTournament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tournament",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Tournament",
                newName: "Tournaments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "EntryMethod",
                value: "System");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                column: "EntryMethod",
                value: "System");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                column: "EntryMethod",
                value: "System");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4,
                column: "EntryMethod",
                value: "System");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5,
                column: "EntryMethod",
                value: "System");

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "CourtsCount", "DateEnd", "DateStart", "Description", "Email", "EntryMethod", "Events", "Logo", "Name", "WebSite" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2022, 5, 8, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(741), new DateTime(2022, 5, 5, 18, 41, 29, 456, DateTimeKind.Local).AddTicks(1285), "Test", "tournamentTest@gmail.com", 0, "ms ws md wd xd ms30+", null, "Dnipro Open 2022", "testWebSite" },
                    { 2, 8, new DateTime(2022, 5, 12, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(1698), new DateTime(2022, 5, 8, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(1687), "Test", "tournamentTest@gmail.com", 0, "ms ws md wd xd ms30+", null, "Kyiv Open 2022", "testWebSite" },
                    { 3, 8, new DateTime(2022, 6, 15, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(1706), new DateTime(2022, 6, 11, 18, 41, 29, 458, DateTimeKind.Local).AddTicks(1703), "Test", "tournamentTest@gmail.com", 0, "ms ws md wd xd ms30+", null, "Kharkiv Open 2022", "testWebSite" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Tournaments_TournamentsId",
                table: "PlayerTournament",
                column: "TournamentsId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerTournament_Tournaments_TournamentsId",
                table: "PlayerTournament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments");

            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Tournaments",
                newName: "Tournament");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tournament",
                table: "Tournament",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "EntryMethod",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                column: "EntryMethod",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3,
                column: "EntryMethod",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4,
                column: "EntryMethod",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5,
                column: "EntryMethod",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerTournament_Tournament_TournamentsId",
                table: "PlayerTournament",
                column: "TournamentsId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
