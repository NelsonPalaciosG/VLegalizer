using Microsoft.EntityFrameworkCore.Migrations;

namespace VLegalizer.Web.Migrations
{
    public partial class deleteamounttodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Trips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "Trips",
                nullable: false,
                defaultValue: 0);
        }
    }
}
