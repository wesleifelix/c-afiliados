using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraAfiliados.Migrations
{
    public partial class products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dayscomission",
                table: "Partiners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ofer",
                columns: table => new
                {
                    Id_ofers = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PartinersId_partiner = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ProductId_product = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TypeComission = table.Column<int>(type: "int", nullable: false),
                    Comission = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Window = table.Column<int>(type: "int", nullable: false),
                    Date_start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Date_end = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofer", x => x.Id_ofers);
                    table.ForeignKey(
                        name: "FK_Ofer_Partiners_PartinersId_partiner",
                        column: x => x.PartinersId_partiner,
                        principalTable: "Partiners",
                        principalColumn: "Id_partiner",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ofer_Product_ProductId_product",
                        column: x => x.ProductId_product,
                        principalTable: "Product",
                        principalColumn: "Id_product",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id_order = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GetPartinerId_partiner = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    GetPublisherId_publisher = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Date_order = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Site = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order_id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Customer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reference = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalPay = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ProductsPay = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShippingPay = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Payment_type = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id_order);
                    table.ForeignKey(
                        name: "FK_Order_Partiners_GetPartinerId_partiner",
                        column: x => x.GetPartinerId_partiner,
                        principalTable: "Partiners",
                        principalColumn: "Id_partiner",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Publishers_GetPublisherId_publisher",
                        column: x => x.GetPublisherId_publisher,
                        principalTable: "Publishers",
                        principalColumn: "Id_publisher",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id_banner = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Url_image = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Format = table.Column<int>(type: "int", nullable: false),
                    GetOfersId_ofers = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id_banner);
                    table.ForeignKey(
                        name: "FK_Banner_Ofer_GetOfersId_ofers",
                        column: x => x.GetOfersId_ofers,
                        principalTable: "Ofer",
                        principalColumn: "Id_ofers",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ViewsProducts",
                columns: table => new
                {
                    Id_productview = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.AutoIncrement),
                    GetProductsId_product = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    GetOferId_ofers = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    GetPartinerId_partiner = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    GetPublisherId_publisher = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Date_access = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Refer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dispositive = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Checkout = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewsProducts", x => x.Id_productview);
                    table.ForeignKey(
                        name: "FK_ViewsProducts_Ofer_GetOferId_ofers",
                        column: x => x.GetOferId_ofers,
                        principalTable: "Ofer",
                        principalColumn: "Id_ofers",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViewsProducts_Partiners_GetPartinerId_partiner",
                        column: x => x.GetPartinerId_partiner,
                        principalTable: "Partiners",
                        principalColumn: "Id_partiner",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViewsProducts_Product_GetProductsId_product",
                        column: x => x.GetProductsId_product,
                        principalTable: "Product",
                        principalColumn: "Id_product",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViewsProducts_Publishers_GetPublisherId_publisher",
                        column: x => x.GetPublisherId_publisher,
                        principalTable: "Publishers",
                        principalColumn: "Id_publisher",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItemsOrder",
                columns: table => new
                {
                    Id_item = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GetProductId_product = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    GetPublisherId_publisher = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    GetOfersId_ofers = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    OrdersId_order = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsOrder", x => x.Id_item);
                    table.ForeignKey(
                        name: "FK_ItemsOrder_Ofer_GetOfersId_ofers",
                        column: x => x.GetOfersId_ofers,
                        principalTable: "Ofer",
                        principalColumn: "Id_ofers",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsOrder_Order_OrdersId_order",
                        column: x => x.OrdersId_order,
                        principalTable: "Order",
                        principalColumn: "Id_order",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsOrder_Product_GetProductId_product",
                        column: x => x.GetProductId_product,
                        principalTable: "Product",
                        principalColumn: "Id_product",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsOrder_Publishers_GetPublisherId_publisher",
                        column: x => x.GetPublisherId_publisher,
                        principalTable: "Publishers",
                        principalColumn: "Id_publisher",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_GetOfersId_ofers",
                table: "Banner",
                column: "GetOfersId_ofers");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsOrder_GetOfersId_ofers",
                table: "ItemsOrder",
                column: "GetOfersId_ofers");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsOrder_GetProductId_product",
                table: "ItemsOrder",
                column: "GetProductId_product");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsOrder_GetPublisherId_publisher",
                table: "ItemsOrder",
                column: "GetPublisherId_publisher");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsOrder_OrdersId_order",
                table: "ItemsOrder",
                column: "OrdersId_order");

            migrationBuilder.CreateIndex(
                name: "IX_Ofer_PartinersId_partiner",
                table: "Ofer",
                column: "PartinersId_partiner");

            migrationBuilder.CreateIndex(
                name: "IX_Ofer_ProductId_product",
                table: "Ofer",
                column: "ProductId_product");

            migrationBuilder.CreateIndex(
                name: "IX_Order_GetPartinerId_partiner",
                table: "Order",
                column: "GetPartinerId_partiner");

            migrationBuilder.CreateIndex(
                name: "IX_Order_GetPublisherId_publisher",
                table: "Order",
                column: "GetPublisherId_publisher");

            migrationBuilder.CreateIndex(
                name: "IX_ViewsProducts_GetOferId_ofers",
                table: "ViewsProducts",
                column: "GetOferId_ofers");

            migrationBuilder.CreateIndex(
                name: "IX_ViewsProducts_GetPartinerId_partiner",
                table: "ViewsProducts",
                column: "GetPartinerId_partiner");

            migrationBuilder.CreateIndex(
                name: "IX_ViewsProducts_GetProductsId_product",
                table: "ViewsProducts",
                column: "GetProductsId_product");

            migrationBuilder.CreateIndex(
                name: "IX_ViewsProducts_GetPublisherId_publisher",
                table: "ViewsProducts",
                column: "GetPublisherId_publisher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "ItemsOrder");

            migrationBuilder.DropTable(
                name: "ViewsProducts");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Ofer");

            migrationBuilder.DropColumn(
                name: "Dayscomission",
                table: "Partiners");
        }
    }
}
