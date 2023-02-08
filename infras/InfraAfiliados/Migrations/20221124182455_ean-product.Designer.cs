﻿// <auto-generated />
using System;
using InfraAfiliados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InfraAfiliados.Migrations
{
    [DbContext(typeof(AfiliadosContext))]
    [Migration("20221124182455_ean-product")]
    partial class eanproduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("BusinessDomain.Banners", b =>
                {
                    b.Property<Guid>("Id_banner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Format")
                        .HasColumnType("int");

                    b.Property<Guid?>("GetOfersId_ofers")
                        .HasColumnType("char(36)");

                    b.Property<string>("Url_image")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id_banner");

                    b.HasIndex("GetOfersId_ofers");

                    b.ToTable("Banner");
                });

            modelBuilder.Entity("BusinessDomain.Ofers", b =>
                {
                    b.Property<Guid>("Id_ofers")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Comission")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date_end")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date_start")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("PartinersId_partiner")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ProductId_product")
                        .HasColumnType("char(36)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("TypeComission")
                        .HasColumnType("int");

                    b.Property<int>("Window")
                        .HasColumnType("int");

                    b.HasKey("Id_ofers");

                    b.HasIndex("PartinersId_partiner");

                    b.HasIndex("ProductId_product");

                    b.ToTable("Ofer");
                });

            modelBuilder.Entity("BusinessDomain.Partiner", b =>
                {
                    b.Property<Guid>("Id_partiner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Complement")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Dayscomission")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("varchar(600)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Facebook")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Instagram")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Linkedin")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Neighbor")
                        .HasColumnType("longtext");

                    b.Property<string>("Numaddress")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Platform")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Postcode")
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)");

                    b.Property<string>("Site")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("State")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("Terms")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Tiktok")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Twitter")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Youtube")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.HasKey("Id_partiner");

                    b.ToTable("Partiners");
                });

            modelBuilder.Entity("BusinessDomain.Products", b =>
                {
                    b.Property<Guid>("Id_product")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Category")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<string>("Ean")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Id")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Link")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<Guid?>("Partiner_idId_partiner")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("PriceOriginal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PriceSale")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Sku")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<decimal>("Stock")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("UrlImage")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id_product");

                    b.HasIndex("Partiner_idId_partiner");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("OrdersDomain.Comissions", b =>
                {
                    b.Property<Guid>("Id_comission")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Blocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Blocked_decription")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date_create")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date_pay")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("GetOrdersId_order")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetProductsId_product")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Payed")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Values")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id_comission");

                    b.HasIndex("GetOrdersId_order");

                    b.HasIndex("GetProductsId_product");

                    b.ToTable("Comission");
                });

            modelBuilder.Entity("OrdersDomain.OrderItem", b =>
                {
                    b.Property<Guid>("Id_item")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetOfersId_ofers")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetOrdersId_order")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetProductId_product")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetPublisherId_publisher")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id_item");

                    b.HasIndex("GetOfersId_ofers");

                    b.HasIndex("GetOrdersId_order");

                    b.HasIndex("GetProductId_product");

                    b.HasIndex("GetPublisherId_publisher");

                    b.ToTable("ItemsOrder");
                });

            modelBuilder.Entity("OrdersDomain.Orders", b =>
                {
                    b.Property<Guid>("Id_order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Customer")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date_create")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date_order")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("GetPartinerId_partiner")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetPublisherId_publisher")
                        .HasColumnType("char(36)");

                    b.Property<string>("Order_id")
                        .HasColumnType("longtext");

                    b.Property<string>("Payment_type")
                        .HasColumnType("longtext");

                    b.Property<decimal>("ProductsPay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Reference")
                        .HasColumnType("longtext");

                    b.Property<decimal>("ShippingPay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Site")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalPay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("Valid")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id_order");

                    b.HasIndex("GetPartinerId_partiner");

                    b.HasIndex("GetPublisherId_publisher");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("PublisherDomain.Publisher", b =>
                {
                    b.Property<Guid>("Id_publisher")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("City")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Complement")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Facebook")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Instagram")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Linkedin")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Neighbor")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Numaddress")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Postcode")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Site")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("State")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Terms")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Tiktok")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Twitter")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Youtube")
                        .HasMaxLength(350)
                        .HasColumnType("varchar(350)");

                    b.HasKey("Id_publisher");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("StatisticsSales.ProductsViews", b =>
                {
                    b.Property<int>("Id_productview")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Checkout")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Date_access")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Dispositive")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("GetOferId_ofers")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetPartinerId_partiner")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetProductsId_product")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GetPublisherId_publisher")
                        .HasColumnType("char(36)");

                    b.Property<string>("Refer")
                        .HasColumnType("longtext");

                    b.HasKey("Id_productview");

                    b.HasIndex("GetOferId_ofers");

                    b.HasIndex("GetPartinerId_partiner");

                    b.HasIndex("GetProductsId_product");

                    b.HasIndex("GetPublisherId_publisher");

                    b.ToTable("ViewsProducts");
                });

            modelBuilder.Entity("BusinessDomain.Banners", b =>
                {
                    b.HasOne("BusinessDomain.Ofers", "GetOfers")
                        .WithMany("Banner")
                        .HasForeignKey("GetOfersId_ofers");

                    b.Navigation("GetOfers");
                });

            modelBuilder.Entity("BusinessDomain.Ofers", b =>
                {
                    b.HasOne("BusinessDomain.Partiner", "Partiners")
                        .WithMany()
                        .HasForeignKey("PartinersId_partiner");

                    b.HasOne("BusinessDomain.Products", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId_product");

                    b.Navigation("Partiners");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BusinessDomain.Products", b =>
                {
                    b.HasOne("BusinessDomain.Partiner", "Partiner_id")
                        .WithMany()
                        .HasForeignKey("Partiner_idId_partiner");

                    b.Navigation("Partiner_id");
                });

            modelBuilder.Entity("OrdersDomain.Comissions", b =>
                {
                    b.HasOne("OrdersDomain.Orders", "GetOrders")
                        .WithMany()
                        .HasForeignKey("GetOrdersId_order");

                    b.HasOne("BusinessDomain.Products", "GetProducts")
                        .WithMany()
                        .HasForeignKey("GetProductsId_product");

                    b.Navigation("GetOrders");

                    b.Navigation("GetProducts");
                });

            modelBuilder.Entity("OrdersDomain.OrderItem", b =>
                {
                    b.HasOne("BusinessDomain.Ofers", "GetOfers")
                        .WithMany()
                        .HasForeignKey("GetOfersId_ofers");

                    b.HasOne("OrdersDomain.Orders", "GetOrders")
                        .WithMany("GetItems")
                        .HasForeignKey("GetOrdersId_order");

                    b.HasOne("BusinessDomain.Products", "GetProduct")
                        .WithMany()
                        .HasForeignKey("GetProductId_product");

                    b.HasOne("PublisherDomain.Publisher", "GetPublisher")
                        .WithMany()
                        .HasForeignKey("GetPublisherId_publisher");

                    b.Navigation("GetOfers");

                    b.Navigation("GetOrders");

                    b.Navigation("GetProduct");

                    b.Navigation("GetPublisher");
                });

            modelBuilder.Entity("OrdersDomain.Orders", b =>
                {
                    b.HasOne("BusinessDomain.Partiner", "GetPartiner")
                        .WithMany()
                        .HasForeignKey("GetPartinerId_partiner");

                    b.HasOne("PublisherDomain.Publisher", "GetPublisher")
                        .WithMany()
                        .HasForeignKey("GetPublisherId_publisher");

                    b.Navigation("GetPartiner");

                    b.Navigation("GetPublisher");
                });

            modelBuilder.Entity("StatisticsSales.ProductsViews", b =>
                {
                    b.HasOne("BusinessDomain.Ofers", "GetOfer")
                        .WithMany()
                        .HasForeignKey("GetOferId_ofers");

                    b.HasOne("BusinessDomain.Partiner", "GetPartiner")
                        .WithMany()
                        .HasForeignKey("GetPartinerId_partiner");

                    b.HasOne("BusinessDomain.Products", "GetProducts")
                        .WithMany()
                        .HasForeignKey("GetProductsId_product");

                    b.HasOne("PublisherDomain.Publisher", "GetPublisher")
                        .WithMany()
                        .HasForeignKey("GetPublisherId_publisher");

                    b.Navigation("GetOfer");

                    b.Navigation("GetPartiner");

                    b.Navigation("GetProducts");

                    b.Navigation("GetPublisher");
                });

            modelBuilder.Entity("BusinessDomain.Ofers", b =>
                {
                    b.Navigation("Banner");
                });

            modelBuilder.Entity("OrdersDomain.Orders", b =>
                {
                    b.Navigation("GetItems");
                });
#pragma warning restore 612, 618
        }
    }
}
