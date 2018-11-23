using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class abcdegsmustjsssxs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GirilenTestSinavSonuclaris_SuresiBaslamisSinavlarId",
                table: "GirilenTestSinavSonuclaris");

            migrationBuilder.CreateIndex(
                name: "IX_GirilenTestSinavSonuclaris_SuresiBaslamisSinavlarId",
                table: "GirilenTestSinavSonuclaris",
                column: "SuresiBaslamisSinavlarId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GirilenTestSinavSonuclaris_SuresiBaslamisSinavlarId",
                table: "GirilenTestSinavSonuclaris");

            migrationBuilder.CreateIndex(
                name: "IX_GirilenTestSinavSonuclaris_SuresiBaslamisSinavlarId",
                table: "GirilenTestSinavSonuclaris",
                column: "SuresiBaslamisSinavlarId");
        }
    }
}
