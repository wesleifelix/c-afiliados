using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraAfiliados.Migrations
{
    public partial class partinerhubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HubsPartinerSite",
                columns: table => new
                {
                    Id_hubs = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GetPartinerId_partiner = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Auth = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TokenBearer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    App_key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Secret_key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Client_key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubsPartinerSite", x => x.Id_hubs);
                    table.ForeignKey(
                        name: "FK_HubsPartinerSite_Partiners_GetPartinerId_partiner",
                        column: x => x.GetPartinerId_partiner,
                        principalTable: "Partiners",
                        principalColumn: "Id_partiner",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HubsPartinerSite_GetPartinerId_partiner",
                table: "HubsPartinerSite",
                column: "GetPartinerId_partiner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HubsPartinerSite");
        }
    }
}
