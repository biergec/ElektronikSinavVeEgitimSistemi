using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class abcd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SinavBitisZamani",
                table: "SuresiBaslamisSinavlars",
                newName: "OgrenciSinaviBitirmeZamani");

            migrationBuilder.RenameColumn(
                name: "SinavBaslamaZamani",
                table: "SuresiBaslamisSinavlars",
                newName: "OgrenciSinavaBaslamaZamani");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OgrenciSinaviBitirmeZamani",
                table: "SuresiBaslamisSinavlars",
                newName: "SinavBitisZamani");

            migrationBuilder.RenameColumn(
                name: "OgrenciSinavaBaslamaZamani",
                table: "SuresiBaslamisSinavlars",
                newName: "SinavBaslamaZamani");
        }
    }
}
