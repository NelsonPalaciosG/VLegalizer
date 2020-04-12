using Microsoft.EntityFrameworkCore.Migrations;

namespace VLegalizer.Web.Migrations
{
    public partial class modifyrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseTypes_TripDetails_TripDetailId",
                table: "ExpenseTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseTypes_TripDetailId",
                table: "ExpenseTypes");

            migrationBuilder.DropColumn(
                name: "TripDetailId",
                table: "ExpenseTypes");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseTypeId",
                table: "TripDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripDetails_ExpenseTypeId",
                table: "TripDetails",
                column: "ExpenseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_ExpenseTypes_ExpenseTypeId",
                table: "TripDetails",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_ExpenseTypes_ExpenseTypeId",
                table: "TripDetails");

            migrationBuilder.DropIndex(
                name: "IX_TripDetails_ExpenseTypeId",
                table: "TripDetails");

            migrationBuilder.DropColumn(
                name: "ExpenseTypeId",
                table: "TripDetails");

            migrationBuilder.AddColumn<int>(
                name: "TripDetailId",
                table: "ExpenseTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_TripDetailId",
                table: "ExpenseTypes",
                column: "TripDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseTypes_TripDetails_TripDetailId",
                table: "ExpenseTypes",
                column: "TripDetailId",
                principalTable: "TripDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
