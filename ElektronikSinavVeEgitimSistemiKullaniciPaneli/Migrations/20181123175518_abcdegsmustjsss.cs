using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class abcdegsmustjsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestSinavSoruSirasi",
                table: "TestSinavSorulars",
                newName: "SoruCevabi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoruCevabi",
                table: "TestSinavSorulars",
                newName: "TestSinavSoruSirasi");
        }
    }
}
