using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ExcelToSQLDemo.Model
{
    public partial class sqlDemoContext : DbContext
    {
        public sqlDemoContext()
        {
        }

        public sqlDemoContext(DbContextOptions<sqlDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Circle> Circles { get; set; }
        public virtual DbSet<ProdCat> ProdCats { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product1> Product1s { get; set; }
        public virtual DbSet<Product2> Product2s { get; set; }
        public virtual DbSet<ProductDatum> ProductData { get; set; }
        public virtual DbSet<Productcategory> Productcategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-VNCEAR0\\SQLEXPRESS;Initial Catalog=sqlDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.Name, "UQ__Category__72E12F1B13E8F231")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripriction)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Circle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("circle");

                entity.Property(e => e.Radius)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("radius");
            });

            modelBuilder.Entity<ProdCat>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProdCat");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Productname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("productname");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('2099-12-31')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK__Product__categor__47DBAE45");
            });

            modelBuilder.Entity<Product1>(entity =>
            {
                entity.ToTable("Product1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('2099-12-31')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product1s)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK__Product1__catego__5EBF139D");
            });

            modelBuilder.Entity<Product2>(entity =>
            {
                entity.ToTable("Product2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('2099-12-31')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product2s)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK__Product2__catego__6383C8BA");
            });

            modelBuilder.Entity<ProductDatum>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Category)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mrp)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("mrp");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Productcategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("productcategory");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Descripriction)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
