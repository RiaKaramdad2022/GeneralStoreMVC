using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GeneralStoreMVC.Models.Customer;
using GeneralStoreMVC.Models.Product;

namespace GeneralStoreMVC.Data
{
    public partial class GeneralStoreDbContext : DbContext
    {
        public GeneralStoreDbContext()
        {
        }

        public GeneralStoreDbContext(DbContextOptions<GeneralStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:GSLocal");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.DateOfTransaction).HasColumnType("datetime");
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("fk_trans_customer_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_trans_product_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<GeneralStoreMVC.Models.Product.ProductEdit>? ProductEdit { get; set; }


    }
}