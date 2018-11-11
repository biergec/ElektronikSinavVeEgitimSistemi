using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElektronikSinavVeEgitimSistemiKullaniciPaneli.Migrations
{
    public partial class Initializee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlasikSinavSorular_KlasikSinavs_KlasikSinavId",
                table: "KlasikSinavSorular");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSinavSorular_TestSinavs_TestSinavId",
                table: "TestSinavSorular");

            migrationBuilder.DropIndex(
                name: "IX_TestSinavs_SinavId",
                table: "TestSinavs");

            migrationBuilder.DropIndex(
                name: "IX_KlasikSinavs_SinavId",
                table: "KlasikSinavs");

            migrationBuilder.DropColumn(
                name: "SinavId",
                table: "TestSinavSorular");

            migrationBuilder.DropColumn(
                name: "SinavId",
                table: "KlasikSinavSorular");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestSinavId",
                table: "TestSinavSorular",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SinavEklenmeTarihi",
                table: "Sinavs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "KlasikSinavId",
                table: "KlasikSinavSorular",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestSinavs_SinavId",
                table: "TestSinavs",
                column: "SinavId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KlasikSinavs_SinavId",
                table: "KlasikSinavs",
                column: "SinavId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KlasikSinavSorular_KlasikSinavs_KlasikSinavId",
                table: "KlasikSinavSorular",
                column: "KlasikSinavId",
                principalTable: "KlasikSinavs",
                principalColumn: "KlasikSinavId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSinavSorular_TestSinavs_TestSinavId",
                table: "TestSinavSorular",
                column: "TestSinavId",
                principalTable: "TestSinavs",
                principalColumn: "TestSinavId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlasikSinavSorular_KlasikSinavs_KlasikSinavId",
                table: "KlasikSinavSorular");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSinavSorular_TestSinavs_TestSinavId",
                table: "TestSinavSorular");

            migrationBuilder.DropIndex(
                name: "IX_TestSinavs_SinavId",
                table: "TestSinavs");

            migrationBuilder.DropIndex(
                name: "IX_KlasikSinavs_SinavId",
                table: "KlasikSinavs");

            migrationBuilder.DropColumn(
                name: "SinavEklenmeTarihi",
                table: "Sinavs");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestSinavId",
                table: "TestSinavSorular",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "SinavId",
                table: "TestSinavSorular",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "KlasikSinavId",
                table: "KlasikSinavSorular",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "SinavId",
                table: "KlasikSinavSorular",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TestSinavs_SinavId",
                table: "TestSinavs",
                column: "SinavId");

            migrationBuilder.CreateIndex(
                name: "IX_KlasikSinavs_SinavId",
                table: "KlasikSinavs",
                column: "SinavId");

            migrationBuilder.AddForeignKey(
                name: "FK_KlasikSinavSorular_KlasikSinavs_KlasikSinavId",
                table: "KlasikSinavSorular",
                column: "KlasikSinavId",
                principalTable: "KlasikSinavs",
                principalColumn: "KlasikSinavId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSinavSorular_TestSinavs_TestSinavId",
                table: "TestSinavSorular",
                column: "TestSinavId",
                principalTable: "TestSinavs",
                principalColumn: "TestSinavId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
