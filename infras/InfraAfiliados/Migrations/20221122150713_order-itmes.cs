using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraAfiliados.Migrations
{
    public partial class orderitmes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrder_Order_OrdersId_order",
                table: "ItemsOrder");

            migrationBuilder.RenameColumn(
                name: "OrdersId_order",
                table: "ItemsOrder",
                newName: "GetOrdersId_order");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsOrder_OrdersId_order",
                table: "ItemsOrder",
                newName: "IX_ItemsOrder_GetOrdersId_order");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrder_Order_GetOrdersId_order",
                table: "ItemsOrder",
                column: "GetOrdersId_order",
                principalTable: "Order",
                principalColumn: "Id_order",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrder_Order_GetOrdersId_order",
                table: "ItemsOrder");

            migrationBuilder.RenameColumn(
                name: "GetOrdersId_order",
                table: "ItemsOrder",
                newName: "OrdersId_order");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsOrder_GetOrdersId_order",
                table: "ItemsOrder",
                newName: "IX_ItemsOrder_OrdersId_order");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrder_Order_OrdersId_order",
                table: "ItemsOrder",
                column: "OrdersId_order",
                principalTable: "Order",
                principalColumn: "Id_order",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
