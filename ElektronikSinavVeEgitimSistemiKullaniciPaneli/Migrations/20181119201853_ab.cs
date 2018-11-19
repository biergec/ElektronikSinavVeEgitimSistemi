using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class ab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KayitliDerslerims",
                columns: table => new
                {
                    KayitliDerslerimId = table.Column<Guid>(nullable: false),
                    DerslerId = table.Column<Guid>(nullable: false),
                    OgrenciId = table.Column<Guid>(nullable: false),
                    DersKayitTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KayitliDerslerims", x => x.KayitliDerslerimId);
                    table.ForeignKey(
                        name: "FK_KayitliDerslerims_Derslers_DerslerId",
                        column: x => x.DerslerId,
                        principalTable: "Derslers",
                        principalColumn: "DerslerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuresiBaslamisSinavlars",
                columns: table => new
                {
                    SuresiBaslamisSinavlarId = table.Column<Guid>(nullable: false),
                    SinavId = table.Column<Guid>(nullable: false),
                    OgrenciId = table.Column<Guid>(nullable: false),
                    SinavBaslamaZamani = table.Column<DateTime>(nullable: false),
                    SinavBitisZamani = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuresiBaslamisSinavlars", x => x.SuresiBaslamisSinavlarId);
                    table.ForeignKey(
                        name: "FK_SuresiBaslamisSinavlars_Sinavs_SinavId",
                        column: x => x.SinavId,
                        principalTable: "Sinavs",
                        principalColumn: "SinavId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KayitliDerslerims_DerslerId",
                table: "KayitliDerslerims",
                column: "DerslerId");

            migrationBuilder.CreateIndex(
                name: "IX_SuresiBaslamisSinavlars_SinavId",
                table: "SuresiBaslamisSinavlars",
                column: "SinavId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KayitliDerslerims");

            migrationBuilder.DropTable(
                name: "SuresiBaslamisSinavlars");
        }
    }
}
