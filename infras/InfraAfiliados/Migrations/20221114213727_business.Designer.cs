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
    [Migration("20221114213727_business")]
    partial class business
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

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

                    b.Property<string>("Category")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<decimal>("Ean")
                        .HasColumnType("decimal(65,30)");

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

            modelBuilder.Entity("BusinessDomain.Products", b =>
                {
                    b.HasOne("BusinessDomain.Partiner", "Partiner_id")
                        .WithMany()
                        .HasForeignKey("Partiner_idId_partiner");

                    b.Navigation("Partiner_id");
                });
#pragma warning restore 612, 618
        }
    }
}