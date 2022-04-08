using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrunEklendiMi",
                table: "KATEGORİLER");

            migrationBuilder.DropColumn(
                name: "UrunSayi",
                table: "KATEGORİLER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UrunEklendiMi",
                table: "KATEGORİLER",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UrunSayi",
                table: "KATEGORİLER",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
