using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class initialize123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DersKayitAnahtari",
                table: "Dersler",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DersKayitAnahtari",
                table: "Dersler");
        }
    }
}
