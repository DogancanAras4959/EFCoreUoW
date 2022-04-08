using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KULLANICI_YETKI_YetkiID",
                table: "KULLANICI");

            migrationBuilder.DropColumn(
                name: "Durum",
                table: "YETKI");

            migrationBuilder.DropColumn(
                name: "EklenmeTarih",
                table: "YETKI");

            migrationBuilder.DropColumn(
                name: "GuncellenmeTarih",
                table: "YETKI");

            migrationBuilder.RenameColumn(
                name: "YetkiID",
                table: "KULLANICI",
                newName: "RolID");

            migrationBuilder.RenameIndex(
                name: "IX_KULLANICI_YetkiID",
                table: "KULLANICI",
                newName: "IX_KULLANICI_RolID");

            migrationBuilder.AlterColumn<string>(
                name: "YetkiAdi",
                table: "YETKI",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "YetkiKodu",
                table: "YETKI",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ROL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROL_YETKI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolID = table.Column<int>(type: "int", nullable: false),
                    YetkiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROL_YETKI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ROL_YETKI_ROL_RolID",
                        column: x => x.RolID,
                        principalTable: "ROL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROL_YETKI_YETKI_YetkiID",
                        column: x => x.YetkiID,
                        principalTable: "YETKI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ROL_YETKI_RolID",
                table: "ROL_YETKI",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_ROL_YETKI_YetkiID",
                table: "ROL_YETKI",
                column: "YetkiID");

            migrationBuilder.AddForeignKey(
                name: "FK_KULLANICI_ROL_RolID",
                table: "KULLANICI",
                column: "RolID",
                principalTable: "ROL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KULLANICI_ROL_RolID",
                table: "KULLANICI");

            migrationBuilder.DropTable(
                name: "ROL_YETKI");

            migrationBuilder.DropTable(
                name: "ROL");

            migrationBuilder.DropColumn(
                name: "YetkiKodu",
                table: "YETKI");

            migrationBuilder.RenameColumn(
                name: "RolID",
                table: "KULLANICI",
                newName: "YetkiID");

            migrationBuilder.RenameIndex(
                name: "IX_KULLANICI_RolID",
                table: "KULLANICI",
                newName: "IX_KULLANICI_YetkiID");

            migrationBuilder.AlterColumn<string>(
                name: "YetkiAdi",
                table: "YETKI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Durum",
                table: "YETKI",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EklenmeTarih",
                table: "YETKI",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellenmeTarih",
                table: "YETKI",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_KULLANICI_YETKI_YetkiID",
                table: "KULLANICI",
                column: "YetkiID",
                principalTable: "YETKI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
