using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Init_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    EntryMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClubId = table.Column<int>(type: "int", nullable: true),
                    TestField = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestField2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "Description", "Logo", "Title" },
                values: new object[] { 1, null, null, "Meteor" });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "Description", "Logo", "Title" },
                values: new object[] { 2, null, null, "Dynamo" });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "Description", "Logo", "Title" },
                values: new object[] { 3, null, null, "Wave" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "AddressId", "Birthday", "ClubId", "EntryMethod", "FirstName", "Gender", "LastName", "Notes", "TestField", "TestField2" },
                values: new object[,]
                {
                    { 1, 0, null, 1, null, "Valeriy", null, null, null, null, null },
                    { 2, 0, null, 1, null, "Anton", null, null, null, null, null },
                    { 3, 0, null, 2, null, "Elena", null, null, null, null, null },
                    { 4, 0, null, 3, null, "Kateryna", null, null, null, null, null },
                    { 5, 0, null, 3, null, "Sergey", null, null, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_ClubId",
                table: "Players",
                column: "ClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
