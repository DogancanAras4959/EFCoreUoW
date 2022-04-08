using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SIPARIS_DURUM_SIPARIS_SiparisID",
                table: "SIPARIS_DURUM");

            migrationBuilder.DropIndex(
                name: "IX_SIPARIS_DURUM_SiparisID",
                table: "SIPARIS_DURUM");

            migrationBuilder.DropColumn(
                name: "SiparisID",
                table: "SIPARIS_DURUM");

            migrationBuilder.AddColumn<bool>(
                name: "AktifMi",
                table: "SIPARIS_DURUM",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SiparisDurumID",
                table: "SIPARIS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_SiparisDurumID",
                table: "SIPARIS",
                column: "SiparisDurumID");

            migrationBuilder.AddForeignKey(
                name: "FK_SIPARIS_SIPARIS_DURUM_SiparisDurumID",
                table: "SIPARIS",
                column: "SiparisDurumID",
                principalTable: "SIPARIS_DURUM",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SIPARIS_SIPARIS_DURUM_SiparisDurumID",
                table: "SIPARIS");

            migrationBuilder.DropIndex(
                name: "IX_SIPARIS_SiparisDurumID",
                table: "SIPARIS");

            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "SIPARIS_DURUM");

            migrationBuilder.DropColumn(
                name: "SiparisDurumID",
                table: "SIPARIS");

            migrationBuilder.AddColumn<int>(
                name: "SiparisID",
                table: "SIPARIS_DURUM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_DURUM_SiparisID",
                table: "SIPARIS_DURUM",
                column: "SiparisID");

            migrationBuilder.AddForeignKey(
                name: "FK_SIPARIS_DURUM_SIPARIS_SiparisID",
                table: "SIPARIS_DURUM",
                column: "SiparisID",
                principalTable: "SIPARIS",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
