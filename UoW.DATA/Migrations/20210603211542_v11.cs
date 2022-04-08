using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOKUMAN_TIPI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    Pozisyon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOKUMAN_TIPI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DOKUMAN",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KatalogAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    Onizleme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DosyaYolu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    DokumanTipi = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOKUMAN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DOKUMAN_DOKUMAN_TIPI_DokumanTipi",
                        column: x => x.DokumanTipi,
                        principalTable: "DOKUMAN_TIPI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DOKUMAN_KULLANICI_YoneticiID",
                        column: x => x.YoneticiID,
                        principalTable: "KULLANICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DOKUMAN_DokumanTipi",
                table: "DOKUMAN",
                column: "DokumanTipi");

            migrationBuilder.CreateIndex(
                name: "IX_DOKUMAN_YoneticiID",
                table: "DOKUMAN",
                column: "YoneticiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOKUMAN");

            migrationBuilder.DropTable(
                name: "DOKUMAN_TIPI");
        }
    }
}
