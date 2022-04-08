using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HABER_KATEGORI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaberKategoriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false),
                    AtkifMi = table.Column<bool>(type: "bit", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HABER_KATEGORI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HABER_KATEGORI_KULLANICI_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SITE_INFO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    YoneticiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SITE_INFO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SITE_INFO_KULLANICI_YoneticiID",
                        column: x => x.YoneticiID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HABER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaberAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaberSpot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaberFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaberOnizlemeFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KategoriID = table.Column<int>(type: "int", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HABER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HABER_HABER_KATEGORI_KategoriID",
                        column: x => x.KategoriID,
                        principalTable: "HABER_KATEGORI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HABER_KULLANICI_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HABER_KategoriID",
                table: "HABER",
                column: "KategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_HABER_KullaniciID",
                table: "HABER",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_HABER_KATEGORI_KullaniciID",
                table: "HABER_KATEGORI",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_SITE_INFO_YoneticiID",
                table: "SITE_INFO",
                column: "YoneticiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HABER");

            migrationBuilder.DropTable(
                name: "SITE_INFO");

            migrationBuilder.DropTable(
                name: "HABER_KATEGORI");
        }
    }
}
