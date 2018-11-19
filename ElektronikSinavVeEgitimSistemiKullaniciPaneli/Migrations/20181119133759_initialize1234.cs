using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class initialize1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SinavAktiflikDurumu",
                table: "Sinavs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinavAktiflikDurumu",
                table: "Sinavs");
        }
    }
}
