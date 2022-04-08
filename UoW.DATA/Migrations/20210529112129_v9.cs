using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "renks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenkAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RenkKodu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_renks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "urunRenks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenkID = table.Column<int>(type: "int", nullable: false),
                    UrunID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_urunRenks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_urunRenks_renks_RenkID",
                        column: x => x.RenkID,
                        principalTable: "renks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_urunRenks_URUNLER_UrunID",
                        column: x => x.UrunID,
                        principalTable: "URUNLER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_urunRenks_RenkID",
                table: "urunRenks",
                column: "RenkID");

            migrationBuilder.CreateIndex(
                name: "IX_urunRenks_UrunID",
                table: "urunRenks",
                column: "UrunID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "urunRenks");

            migrationBuilder.DropTable(
                name: "renks");
        }
    }
}
