using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class refNoRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ISLEM_REFERANS");

            migrationBuilder.AddColumn<string>(
                name: "ReferansNo",
                table: "SIPARIS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferansNo",
                table: "SIPARIS");

            migrationBuilder.CreateTable(
                name: "ISLEM_REFERANS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferansNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiparisID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISLEM_REFERANS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ISLEM_REFERANS_SIPARIS_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SIPARIS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ISLEM_REFERANS_SiparisID",
                table: "ISLEM_REFERANS",
                column: "SiparisID");
        }
    }
}
