using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraAfiliados.Migrations
{
    public partial class comission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date_create",
                table: "Order",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Comission",
                columns: table => new
                {
                    Id_comission = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GetOrdersId_order = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    GetProductsId_product = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Values = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Payed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Date_create = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Date_pay = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Blocked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Blocked_decription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comission", x => x.Id_comission);
                    table.ForeignKey(
                        name: "FK_Comission_Order_GetOrdersId_order",
                        column: x => x.GetOrdersId_order,
                        principalTable: "Order",
                        principalColumn: "Id_order",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comission_Product_GetProductsId_product",
                        column: x => x.GetProductsId_product,
                        principalTable: "Product",
                        principalColumn: "Id_product",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Comission_GetOrdersId_order",
                table: "Comission",
                column: "GetOrdersId_order");

            migrationBuilder.CreateIndex(
                name: "IX_Comission_GetProductsId_product",
                table: "Comission",
                column: "GetProductsId_product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comission");

            migrationBuilder.DropColumn(
                name: "Date_create",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "Order");
        }
    }
}
