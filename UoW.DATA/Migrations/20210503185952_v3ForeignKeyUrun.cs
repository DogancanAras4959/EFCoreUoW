using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v3ForeignKeyUrun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                     name: "IX_URUNLER_kullaniciID",
                     table: "URUNLER");

            migrationBuilder.DropIndex(
                name: "IX_URUNLER_stokbirimiID",
                table: "URUNLER");

            migrationBuilder.DropColumn(
                name: "kullaniciID",
                table: "URUNLER");

            migrationBuilder.DropColumn(
                name: "stokbirimiID",
                table: "URUNLER");

            migrationBuilder.CreateIndex(
                name: "IX_URUNLER_StokBirimID",
                table: "URUNLER",
                column: "StokBirimID");

            migrationBuilder.CreateIndex(
                name: "IX_URUNLER_YoneticiID",
                table: "URUNLER",
                column: "YoneticiID");

            migrationBuilder.AddForeignKey(
                name: "FK_URUNLER_KULLANICI_YoneticiID",
                table: "URUNLER",
                column: "YoneticiID",
                principalTable: "KULLANICI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_URUNLER_STOK_BIRIMI_StokBirimID",
                table: "URUNLER",
                column: "StokBirimID",
                principalTable: "STOK_BIRIMI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_URUNLER_KULLANICI_YoneticiID",
                table: "URUNLER");

            migrationBuilder.DropForeignKey(
                name: "FK_URUNLER_STOK_BIRIMI_StokBirimID",
                table: "URUNLER");

            migrationBuilder.DropIndex(
                name: "IX_URUNLER_StokBirimID",
                table: "URUNLER");

            migrationBuilder.DropIndex(
                name: "IX_URUNLER_YoneticiID",
                table: "URUNLER");

            migrationBuilder.AddColumn<int>(
                name: "kullaniciID",
                table: "URUNLER",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stokbirimiID",
                table: "URUNLER",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_URUNLER_kullaniciID",
                table: "URUNLER",
                column: "kullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_URUNLER_stokbirimiID",
                table: "URUNLER",
                column: "stokbirimiID");

            migrationBuilder.AddForeignKey(
                name: "FK_URUNLER_KULLANICI_kullaniciID",
                table: "URUNLER",
                column: "kullaniciID",
                principalTable: "KULLANICI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_URUNLER_STOK_BIRIMI_stokbirimiID",
                table: "URUNLER",
                column: "stokbirimiID",
                principalTable: "STOK_BIRIMI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
