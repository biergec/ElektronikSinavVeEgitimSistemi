using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class initialize2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoruMetni",
                table: "TestSinavs");

            migrationBuilder.RenameColumn(
                name: "SoruSikki",
                table: "TestSinavSorular",
                newName: "TestSinavSoruSirasi");

            migrationBuilder.RenameColumn(
                name: "SoruSikMetni",
                table: "TestSinavSorular",
                newName: "TestSinavSorusuMetni");

            migrationBuilder.CreateTable(
                name: "TestSinavSoruSiklari",
                columns: table => new
                {
                    TestSinavSoruSiklariId = table.Column<Guid>(nullable: false),
                    TestSinavSorularId = table.Column<Guid>(nullable: false),
                    SoruSikki = table.Column<int>(nullable: false),
                    SoruSikMetni = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSinavSoruSiklari", x => x.TestSinavSoruSiklariId);
                    table.ForeignKey(
                        name: "FK_TestSinavSoruSiklari_TestSinavSorular_TestSinavSorularId",
                        column: x => x.TestSinavSorularId,
                        principalTable: "TestSinavSorular",
                        principalColumn: "TestSinavSorularId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestSinavSoruSiklari_TestSinavSorularId",
                table: "TestSinavSoruSiklari",
                column: "TestSinavSorularId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestSinavSoruSiklari");

            migrationBuilder.RenameColumn(
                name: "TestSinavSorusuMetni",
                table: "TestSinavSorular",
                newName: "SoruSikMetni");

            migrationBuilder.RenameColumn(
                name: "TestSinavSoruSirasi",
                table: "TestSinavSorular",
                newName: "SoruSikki");

            migrationBuilder.AddColumn<string>(
                name: "SoruMetni",
                table: "TestSinavs",
                nullable: true);
        }
    }
}
