using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class initialize12345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SinavSuresiDakika",
                table: "Sinavs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SinavSuresiDakika",
                table: "Sinavs");
        }
    }
}
