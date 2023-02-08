using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraAfiliados.Migrations
{
    public partial class publishersocial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Publishers",
                type: "varchar(350)",
                maxLength: 350,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Publishers",
                type: "varchar(350)",
                maxLength: 350,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "Publishers",
                type: "varchar(350)",
                maxLength: 350,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "Publishers",
                type: "varchar(350)",
                maxLength: 350,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Tiktok",
                table: "Publishers",
                type: "varchar(350)",
                maxLength: 350,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Publishers",
                type: "varchar(350)",
                maxLength: 350,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Youtube",
                table: "Publishers",
                type: "varchar(350)",
                maxLength: 350,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Tiktok",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Youtube",
                table: "Publishers");
        }
    }
}
