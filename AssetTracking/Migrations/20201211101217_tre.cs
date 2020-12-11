using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetTracking.Migrations
{
    public partial class tre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OfficeId",
                table: "Assets",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Offices_OfficeId",
                table: "Assets",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Offices_OfficeId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_OfficeId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Assets");
        }
    }
}
