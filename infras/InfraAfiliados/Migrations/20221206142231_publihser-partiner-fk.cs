using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraAfiliados.Migrations
{
    public partial class publihserpartinerfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GetPartinerId_partiner",
                table: "Publishers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_GetPartinerId_partiner",
                table: "Publishers",
                column: "GetPartinerId_partiner");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Partiners_GetPartinerId_partiner",
                table: "Publishers",
                column: "GetPartinerId_partiner",
                principalTable: "Partiners",
                principalColumn: "Id_partiner",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Partiners_GetPartinerId_partiner",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_GetPartinerId_partiner",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "GetPartinerId_partiner",
                table: "Publishers");
        }
    }
}
