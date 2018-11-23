using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class abcde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GirilenKlasikSinavKayits",
                columns: table => new
                {
                    GirilenKlasikSinavKayitId = table.Column<Guid>(nullable: false),
                    SuresiBaslamisSinavlarId = table.Column<Guid>(nullable: false),
                    OgrenciSinavPuani = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GirilenKlasikSinavKayits", x => x.GirilenKlasikSinavKayitId);
                    table.ForeignKey(
                        name: "FK_GirilenKlasikSinavKayits_SuresiBaslamisSinavlars_SuresiBaslamisSinavlarId",
                        column: x => x.SuresiBaslamisSinavlarId,
                        principalTable: "SuresiBaslamisSinavlars",
                        principalColumn: "SuresiBaslamisSinavlarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlasikSinavSinavSoruCevaps",
                columns: table => new
                {
                    KlasikSinavSinavSoruCevapId = table.Column<Guid>(nullable: false),
                    GirilenKlasikSinavKayitId = table.Column<Guid>(nullable: false),
                    SoruText = table.Column<string>(nullable: true),
                    SoruCevapText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlasikSinavSinavSoruCevaps", x => x.KlasikSinavSinavSoruCevapId);
                    table.ForeignKey(
                        name: "FK_KlasikSinavSinavSoruCevaps_GirilenKlasikSinavKayits_GirilenKlasikSinavKayitId",
                        column: x => x.GirilenKlasikSinavKayitId,
                        principalTable: "GirilenKlasikSinavKayits",
                        principalColumn: "GirilenKlasikSinavKayitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GirilenKlasikSinavKayits_SuresiBaslamisSinavlarId",
                table: "GirilenKlasikSinavKayits",
                column: "SuresiBaslamisSinavlarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KlasikSinavSinavSoruCevaps_GirilenKlasikSinavKayitId",
                table: "KlasikSinavSinavSoruCevaps",
                column: "GirilenKlasikSinavKayitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KlasikSinavSinavSoruCevaps");

            migrationBuilder.DropTable(
                name: "GirilenKlasikSinavKayits");
        }
    }
}
