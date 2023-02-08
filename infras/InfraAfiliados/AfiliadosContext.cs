using BusinessDomain;
using Microsoft.EntityFrameworkCore;
using OrdersDomain;
using PublisherDomain;
using StatisticsSales;
using System;

namespace InfraAfiliados
{
    public class AfiliadosContext : DbContext
    {
        public AfiliadosContext(DbContextOptions<AfiliadosContext> options)
            : base(options)
        {
            Database.AutoTransactionsEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PublisherMapping(modelBuilder);
        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Partiner> Partiners { get; set; }
        public DbSet<Products> Product { get; set; }
        public DbSet<Ofers> Ofer { get; set; }
        public DbSet<Banners> Banner { get; set; }
        public DbSet<ProductsViews> ViewsProducts { get; set; }
        public DbSet<Orders> Order { get; set; }
        public DbSet<OrderItem> ItemsOrder { get; set; }
        public DbSet<Comissions> Comission { get; set; }
        public DbSet<HubsPartiners> HubsPartinerSite { get; set; }




        protected ModelBuilder PublisherMapping(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Tabela de usários
            modelBuilder.Entity<Publisher>(b =>
            {
                b.HasKey(t => t.Id_publisher);

                b.Property(t => t.Id_publisher)
                    .HasConversion<Guid>()
                    .IsRequired();

                b.Property(t => t.Name)
                     .HasMaxLength(500)
                     .IsRequired();
            
                b.Property(t => t.Email)
                     .HasMaxLength(500)
                     .IsRequired();

                b.Property(t => t.Document)
                     .HasMaxLength(500)
                     .IsRequired();

                b.Property(t => t.City)
                    .HasMaxLength(500);

                b.Property(t => t.Complement)
                    .HasMaxLength(500);

                b.Property(t => t.Neighbor)
                    .HasMaxLength(500);

                b.Property(t => t.Numaddress)
                    .HasMaxLength(100);

                b.Property(t => t.Postcode)
                    .HasMaxLength(100);

                b.Property(t => t.State)
                    .HasMaxLength(100);

                b.Property(t => t.Address)
                    .HasMaxLength(500);

            });

            return modelBuilder;
        }

        protected ModelBuilder PartinerMapping(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Tabela de usários
            modelBuilder.Entity<Partiner>(b =>
            {
                b.HasKey(t => t.Id_partiner);

                b.Property(t => t.Id_partiner)
                    .HasConversion<Guid>()
                    .IsRequired();

                b.Property(t => t.Name)
                     .HasMaxLength(500)
                     .IsRequired();

                b.Property(t => t.Email)
                     .HasMaxLength(500)
                     .IsRequired();

                b.Property(t => t.Document)
                     .HasMaxLength(500)
                     .IsRequired();

                b.Property(t => t.City)
                    .HasMaxLength(500);

                b.Property(t => t.Complement)
                    .HasMaxLength(500);

                b.Property(t => t.Neighbor)
                    .HasMaxLength(500);

                b.Property(t => t.Numaddress)
                    .HasMaxLength(100);

                b.Property(t => t.Postcode)
                    .HasMaxLength(100);

                b.Property(t => t.State)
                    .HasMaxLength(100);

                b.Property(t => t.Address)
                    .HasMaxLength(500);

            });

            return modelBuilder;
        }
    }
}
