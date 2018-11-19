using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class initialize1234567 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlasikSinavSorular_KlasikSinavs_KlasikSinavId",
                table: "KlasikSinavSorular");

            migrationBuilder.DropForeignKey(
                name: "FK_Sinavs_Dersler_DerslerId",
                table: "Sinavs");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSinavSorular_TestSinavs_TestSinavId",
                table: "TestSinavSorular");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSinavSoruSiklari_TestSinavSorular_TestSinavSorularId",
                table: "TestSinavSoruSiklari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSinavSoruSiklari",
                table: "TestSinavSoruSiklari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSinavSorular",
                table: "TestSinavSorular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KlasikSinavSorular",
                table: "KlasikSinavSorular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dersler",
                table: "Dersler");

            migrationBuilder.RenameTable(
                name: "TestSinavSoruSiklari",
                newName: "TestSinavSoruSiklaris");

            migrationBuilder.RenameTable(
                name: "TestSinavSorular",
                newName: "TestSinavSorulars");

            migrationBuilder.RenameTable(
                name: "KlasikSinavSorular",
                newName: "KlasikSinavSorulars");

            migrationBuilder.RenameTable(
                name: "Dersler",
                newName: "Derslers");

            migrationBuilder.RenameIndex(
                name: "IX_TestSinavSoruSiklari_TestSinavSorularId",
                table: "TestSinavSoruSiklaris",
                newName: "IX_TestSinavSoruSiklaris_TestSinavSorularId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSinavSorular_TestSinavId",
                table: "TestSinavSorulars",
                newName: "IX_TestSinavSorulars_TestSinavId");

            migrationBuilder.RenameIndex(
                name: "IX_KlasikSinavSorular_KlasikSinavId",
                table: "KlasikSinavSorulars",
                newName: "IX_KlasikSinavSorulars_KlasikSinavId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSinavSoruSiklaris",
                table: "TestSinavSoruSiklaris",
                column: "TestSinavSoruSiklariId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSinavSorulars",
                table: "TestSinavSorulars",
                column: "TestSinavSorularId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KlasikSinavSorulars",
                table: "KlasikSinavSorulars",
                column: "KlasikSinavSorularId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Derslers",
                table: "Derslers",
                column: "DerslerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KlasikSinavSorulars_KlasikSinavs_KlasikSinavId",
                table: "KlasikSinavSorulars",
                column: "KlasikSinavId",
                principalTable: "KlasikSinavs",
                principalColumn: "KlasikSinavId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sinavs_Derslers_DerslerId",
                table: "Sinavs",
                column: "DerslerId",
                principalTable: "Derslers",
                principalColumn: "DerslerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSinavSorulars_TestSinavs_TestSinavId",
                table: "TestSinavSorulars",
                column: "TestSinavId",
                principalTable: "TestSinavs",
                principalColumn: "TestSinavId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSinavSoruSiklaris_TestSinavSorulars_TestSinavSorularId",
                table: "TestSinavSoruSiklaris",
                column: "TestSinavSorularId",
                principalTable: "TestSinavSorulars",
                principalColumn: "TestSinavSorularId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlasikSinavSorulars_KlasikSinavs_KlasikSinavId",
                table: "KlasikSinavSorulars");

            migrationBuilder.DropForeignKey(
                name: "FK_Sinavs_Derslers_DerslerId",
                table: "Sinavs");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSinavSorulars_TestSinavs_TestSinavId",
                table: "TestSinavSorulars");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSinavSoruSiklaris_TestSinavSorulars_TestSinavSorularId",
                table: "TestSinavSoruSiklaris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSinavSoruSiklaris",
                table: "TestSinavSoruSiklaris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSinavSorulars",
                table: "TestSinavSorulars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KlasikSinavSorulars",
                table: "KlasikSinavSorulars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Derslers",
                table: "Derslers");

            migrationBuilder.RenameTable(
                name: "TestSinavSoruSiklaris",
                newName: "TestSinavSoruSiklari");

            migrationBuilder.RenameTable(
                name: "TestSinavSorulars",
                newName: "TestSinavSorular");

            migrationBuilder.RenameTable(
                name: "KlasikSinavSorulars",
                newName: "KlasikSinavSorular");

            migrationBuilder.RenameTable(
                name: "Derslers",
                newName: "Dersler");

            migrationBuilder.RenameIndex(
                name: "IX_TestSinavSoruSiklaris_TestSinavSorularId",
                table: "TestSinavSoruSiklari",
                newName: "IX_TestSinavSoruSiklari_TestSinavSorularId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSinavSorulars_TestSinavId",
                table: "TestSinavSorular",
                newName: "IX_TestSinavSorular_TestSinavId");

            migrationBuilder.RenameIndex(
                name: "IX_KlasikSinavSorulars_KlasikSinavId",
                table: "KlasikSinavSorular",
                newName: "IX_KlasikSinavSorular_KlasikSinavId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSinavSoruSiklari",
                table: "TestSinavSoruSiklari",
                column: "TestSinavSoruSiklariId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSinavSorular",
                table: "TestSinavSorular",
                column: "TestSinavSorularId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KlasikSinavSorular",
                table: "KlasikSinavSorular",
                column: "KlasikSinavSorularId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dersler",
                table: "Dersler",
                column: "DerslerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KlasikSinavSorular_KlasikSinavs_KlasikSinavId",
                table: "KlasikSinavSorular",
                column: "KlasikSinavId",
                principalTable: "KlasikSinavs",
                principalColumn: "KlasikSinavId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sinavs_Dersler_DerslerId",
                table: "Sinavs",
                column: "DerslerId",
                principalTable: "Dersler",
                principalColumn: "DerslerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSinavSorular_TestSinavs_TestSinavId",
                table: "TestSinavSorular",
                column: "TestSinavId",
                principalTable: "TestSinavs",
                principalColumn: "TestSinavId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSinavSoruSiklari_TestSinavSorular_TestSinavSorularId",
                table: "TestSinavSoruSiklari",
                column: "TestSinavSorularId",
                principalTable: "TestSinavSorular",
                principalColumn: "TestSinavSorularId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
