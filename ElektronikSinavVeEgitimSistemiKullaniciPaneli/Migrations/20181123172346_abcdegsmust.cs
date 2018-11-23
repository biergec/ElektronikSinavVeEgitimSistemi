using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class abcdegsmust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GirilenTestSinavSonuclaris",
                columns: table => new
                {
                    GirilenTestSinavSonuclariId = table.Column<Guid>(nullable: false),
                    SuresiBaslamisSinavlarId = table.Column<Guid>(nullable: false),
                    SinavPuani = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GirilenTestSinavSonuclaris", x => x.GirilenTestSinavSonuclariId);
                    table.ForeignKey(
                        name: "FK_GirilenTestSinavSonuclaris_SuresiBaslamisSinavlars_SuresiBaslamisSinavlarId",
                        column: x => x.SuresiBaslamisSinavlarId,
                        principalTable: "SuresiBaslamisSinavlars",
                        principalColumn: "SuresiBaslamisSinavlarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GirilenTestSinavSonuclaris_SuresiBaslamisSinavlarId",
                table: "GirilenTestSinavSonuclaris",
                column: "SuresiBaslamisSinavlarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GirilenTestSinavSonuclaris");
        }
    }
}
