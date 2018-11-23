using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class abcdegsmus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GirilenKlasikSinavKayits_SuresiBaslamisSinavlarId",
                table: "GirilenKlasikSinavKayits");

            migrationBuilder.CreateIndex(
                name: "IX_GirilenKlasikSinavKayits_SuresiBaslamisSinavlarId",
                table: "GirilenKlasikSinavKayits",
                column: "SuresiBaslamisSinavlarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GirilenKlasikSinavKayits_SuresiBaslamisSinavlarId",
                table: "GirilenKlasikSinavKayits");

            migrationBuilder.CreateIndex(
                name: "IX_GirilenKlasikSinavKayits_SuresiBaslamisSinavlarId",
                table: "GirilenKlasikSinavKayits",
                column: "SuresiBaslamisSinavlarId",
                unique: true);
        }
    }
}
