using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class Initializeasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanliYayins",
                columns: table => new
                {
                    CanliYayinId = table.Column<Guid>(nullable: false),
                    CanliYayinAktifMi = table.Column<bool>(nullable: false),
                    CanliYayinBaslamaZamani = table.Column<DateTime>(nullable: true),
                    CanliYayinBitisZamani = table.Column<DateTime>(nullable: true),
                    CanliYayinSahibi = table.Column<Guid>(nullable: false),
                    CanliYayinDersId = table.Column<Guid>(nullable: false),
                    CanliYayinYayinId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanliYayins", x => x.CanliYayinId);
                });

            migrationBuilder.CreateTable(
                name: "CanliYayinaKatilanlars",
                columns: table => new
                {
                    CanliYayinaKatilanlarId = table.Column<Guid>(nullable: false),
                    CanliYayinId = table.Column<Guid>(nullable: false),
                    CanliYayinKatilmaZamani = table.Column<DateTime>(nullable: false),
                    CanliYayinAyrilmaZamani = table.Column<DateTime>(nullable: true),
                    CanliYayinaKatilanKisiId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanliYayinaKatilanlars", x => x.CanliYayinaKatilanlarId);
                    table.ForeignKey(
                        name: "FK_CanliYayinaKatilanlars_CanliYayins_CanliYayinId",
                        column: x => x.CanliYayinId,
                        principalTable: "CanliYayins",
                        principalColumn: "CanliYayinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CanliYayinDokumanlaris",
                columns: table => new
                {
                    CanliYayinDokumanlariId = table.Column<Guid>(nullable: false),
                    CanliYayinId = table.Column<Guid>(nullable: false),
                    DokumanEklenmeTarihi = table.Column<DateTime>(nullable: false),
                    DokumanAdi = table.Column<string>(nullable: true),
                    DokumanKayitPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanliYayinDokumanlaris", x => x.CanliYayinDokumanlariId);
                    table.ForeignKey(
                        name: "FK_CanliYayinDokumanlaris_CanliYayins_CanliYayinId",
                        column: x => x.CanliYayinId,
                        principalTable: "CanliYayins",
                        principalColumn: "CanliYayinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanliYayinaKatilanlars_CanliYayinId",
                table: "CanliYayinaKatilanlars",
                column: "CanliYayinId");

            migrationBuilder.CreateIndex(
                name: "IX_CanliYayinDokumanlaris_CanliYayinId",
                table: "CanliYayinDokumanlaris",
                column: "CanliYayinId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanliYayinaKatilanlars");

            migrationBuilder.DropTable(
                name: "CanliYayinDokumanlaris");

            migrationBuilder.DropTable(
                name: "CanliYayins");
        }
    }
}
