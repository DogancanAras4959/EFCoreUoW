using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class LastBinCardMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "krediKartBins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BIN = table.Column<int>(type: "int", nullable: false),
                    BankaKodu = table.Column<int>(type: "int", nullable: false),
                    BankaAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kademe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sanal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    onOdeme = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_krediKartBins", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "krediKartBins");
        }
    }
}
