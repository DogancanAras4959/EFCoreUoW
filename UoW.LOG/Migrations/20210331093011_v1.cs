using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.LOG.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "durumlars",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurumAdi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_durumlars", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "islemlers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IslemAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eklenme = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_islemlers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "yoneticilers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yoneticilers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YoneticiID = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IslemID = table.Column<int>(type: "int", nullable: false),
                    DurumID = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Saat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    islemlerID = table.Column<int>(type: "int", nullable: true),
                    durumlarID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_logs_durumlars_durumlarID",
                        column: x => x.durumlarID,
                        principalTable: "durumlars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_logs_islemlers_islemlerID",
                        column: x => x.islemlerID,
                        principalTable: "islemlers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_logs_yoneticilers_YoneticiID",
                        column: x => x.YoneticiID,
                        principalTable: "yoneticilers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_logs_durumlarID",
                table: "logs",
                column: "durumlarID");

            migrationBuilder.CreateIndex(
                name: "IX_logs_islemlerID",
                table: "logs",
                column: "islemlerID");

            migrationBuilder.CreateIndex(
                name: "IX_logs_YoneticiID",
                table: "logs",
                column: "YoneticiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "durumlars");

            migrationBuilder.DropTable(
                name: "islemlers");

            migrationBuilder.DropTable(
                name: "yoneticilers");
        }
    }
}
