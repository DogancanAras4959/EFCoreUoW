using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BilgilendirmeAlma",
                table: "BASVURU",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EpostaBilgiAlma",
                table: "BASVURU",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SMSAlma",
                table: "BASVURU",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UyelikSozlesmesiOkuduMu",
                table: "BASVURU",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BilgilendirmeAlma",
                table: "BASVURU");

            migrationBuilder.DropColumn(
                name: "EpostaBilgiAlma",
                table: "BASVURU");

            migrationBuilder.DropColumn(
                name: "SMSAlma",
                table: "BASVURU");

            migrationBuilder.DropColumn(
                name: "UyelikSozlesmesiOkuduMu",
                table: "BASVURU");
        }
    }
}
