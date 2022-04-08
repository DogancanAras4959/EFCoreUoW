using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_urunRenks_renks_RenkID",
                table: "urunRenks");

            migrationBuilder.DropForeignKey(
                name: "FK_urunRenks_URUNLER_UrunID",
                table: "urunRenks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_urunRenks",
                table: "urunRenks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_renks",
                table: "renks");

            migrationBuilder.RenameTable(
                name: "urunRenks",
                newName: "URUN_RENK");

            migrationBuilder.RenameTable(
                name: "renks",
                newName: "RENK");

            migrationBuilder.RenameIndex(
                name: "IX_urunRenks_UrunID",
                table: "URUN_RENK",
                newName: "IX_URUN_RENK_UrunID");

            migrationBuilder.RenameIndex(
                name: "IX_urunRenks_RenkID",
                table: "URUN_RENK",
                newName: "IX_URUN_RENK_RenkID");

            migrationBuilder.AddColumn<int>(
                name: "Adet",
                table: "URUN_RENK",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Resim",
                table: "URUN_RENK",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_URUN_RENK",
                table: "URUN_RENK",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RENK",
                table: "RENK",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_URUN_RENK_RENK_RenkID",
                table: "URUN_RENK",
                column: "RenkID",
                principalTable: "RENK",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_URUN_RENK_URUNLER_UrunID",
                table: "URUN_RENK",
                column: "UrunID",
                principalTable: "URUNLER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_URUN_RENK_RENK_RenkID",
                table: "URUN_RENK");

            migrationBuilder.DropForeignKey(
                name: "FK_URUN_RENK_URUNLER_UrunID",
                table: "URUN_RENK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_URUN_RENK",
                table: "URUN_RENK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RENK",
                table: "RENK");

            migrationBuilder.DropColumn(
                name: "Adet",
                table: "URUN_RENK");

            migrationBuilder.DropColumn(
                name: "Resim",
                table: "URUN_RENK");

            migrationBuilder.RenameTable(
                name: "URUN_RENK",
                newName: "urunRenks");

            migrationBuilder.RenameTable(
                name: "RENK",
                newName: "renks");

            migrationBuilder.RenameIndex(
                name: "IX_URUN_RENK_UrunID",
                table: "urunRenks",
                newName: "IX_urunRenks_UrunID");

            migrationBuilder.RenameIndex(
                name: "IX_URUN_RENK_RenkID",
                table: "urunRenks",
                newName: "IX_urunRenks_RenkID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_urunRenks",
                table: "urunRenks",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_renks",
                table: "renks",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_urunRenks_renks_RenkID",
                table: "urunRenks",
                column: "RenkID",
                principalTable: "renks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_urunRenks_URUNLER_UrunID",
                table: "urunRenks",
                column: "UrunID",
                principalTable: "URUNLER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
