using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAutoService.Data.Migrations
{
    public partial class UpdatDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_ServiceHeaders_ServiceHeaderId",
                table: "ServiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_ServiceDetails_ServiceHeaderId",
                table: "ServiceDetails");

            migrationBuilder.AddColumn<int>(
                name: "ServiceHeader",
                table: "ServiceDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetails_ServiceHeader",
                table: "ServiceDetails",
                column: "ServiceHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_ServiceHeaders_ServiceHeader",
                table: "ServiceDetails",
                column: "ServiceHeader",
                principalTable: "ServiceHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_ServiceHeaders_ServiceHeader",
                table: "ServiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_ServiceDetails_ServiceHeader",
                table: "ServiceDetails");

            migrationBuilder.DropColumn(
                name: "ServiceHeader",
                table: "ServiceDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetails_ServiceHeaderId",
                table: "ServiceDetails",
                column: "ServiceHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_ServiceHeaders_ServiceHeaderId",
                table: "ServiceDetails",
                column: "ServiceHeaderId",
                principalTable: "ServiceHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
