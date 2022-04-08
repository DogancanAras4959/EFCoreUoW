using Microsoft.EntityFrameworkCore.Migrations;

namespace UoW.DATA.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BayiID",
                table: "SEPET",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SEPET_BayiID",
                table: "SEPET",
                column: "BayiID");

            migrationBuilder.AddForeignKey(
                name: "FK_SEPET_BAYI_BayiID",
                table: "SEPET",
                column: "BayiID",
                principalTable: "BAYI",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SEPET_BAYI_BayiID",
                table: "SEPET");

            migrationBuilder.DropIndex(
                name: "IX_SEPET_BayiID",
                table: "SEPET");

            migrationBuilder.DropColumn(
                name: "BayiID",
                table: "SEPET");
        }
    }
}
