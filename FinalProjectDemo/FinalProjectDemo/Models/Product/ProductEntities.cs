using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalProjectDemo.Models.Product
{
    public partial class ProductEntities : DbContext
    {
        public virtual DbSet<HddFinal> HddFinal { get; set; }
        public virtual DbSet<MoboFinal> MoboFinal { get; set; }
        public virtual DbSet<ProcessorFinal> ProcessorFinal { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Review> Review { get; set; }


        public ProductEntities(DbContextOptions<ProductEntities> options)
       : base(options) { }



        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MVUN8U4\SQLEXPRESS;Database=final_test_database;Integrated Security=True");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HddFinal>(entity =>
            {
                entity.HasKey(e => e.HddId);

                entity.ToTable("hdd_final");

                entity.Property(e => e.HddId).HasColumnName("hdd_id");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasColumnName("brand")
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.PicLink)
                    .IsRequired()
                    .HasColumnName("pic_link")
                    .HasMaxLength(255);

                entity.Property(e => e.RyansLink)
                    .IsRequired()
                    .HasColumnName("ryans_link")
                    .HasMaxLength(255);

                entity.Property(e => e.RyansPrice)
                    .IsRequired()
                    .HasColumnName("ryans_price")
                    .HasMaxLength(255);

                entity.Property(e => e.StarLink)
                    .IsRequired()
                    .HasColumnName("star_link")
                    .HasMaxLength(255);

                entity.Property(e => e.StarPrice).HasColumnName("star_price");

                entity.Property(e => e.Storage)
                    .IsRequired()
                    .HasColumnName("storage")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MoboFinal>(entity =>
            {
                entity.HasKey(e => e.MoboId);

                entity.ToTable("mobo_final");

                entity.Property(e => e.MoboId).HasColumnName("mobo_id");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasColumnName("brand")
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.PicLink)
                    .IsRequired()
                    .HasColumnName("pic_link")
                    .HasMaxLength(255);

                entity.Property(e => e.RyansLink)
                    .IsRequired()
                    .HasColumnName("ryans_link")
                    .HasMaxLength(255);

                entity.Property(e => e.RyansPrice).HasColumnName("ryans_price");

                entity.Property(e => e.StarLink)
                    .IsRequired()
                    .HasColumnName("star_link")
                    .HasMaxLength(255);

                entity.Property(e => e.StarPrice).HasColumnName("star_price");
            });

            modelBuilder.Entity<ProcessorFinal>(entity =>
            {
                entity.HasKey(e => e.ProId);

                entity.ToTable("processor_final");

                entity.Property(e => e.ProId).HasColumnName("pro_id");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasColumnName("brand")
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.PicLink)
                    .IsRequired()
                    .HasColumnName("pic_link")
                    .HasMaxLength(255);

                entity.Property(e => e.RyansLink)
                    .IsRequired()
                    .HasColumnName("ryans_link")
                    .HasMaxLength(255);

                entity.Property(e => e.RyansPrice).HasColumnName("ryans_price");

                entity.Property(e => e.StarLink)
                    .IsRequired()
                    .HasColumnName("star_link")
                    .HasMaxLength(255);

                entity.Property(e => e.StarPrice).HasColumnName("star_price");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.RateId);

                entity.ToTable("rating");

                entity.Property(e => e.RateId)
                    .HasColumnName("rate_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(50);

                entity.Property(e => e.RatingValue).HasColumnName("rating_value");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");

                entity.Property(e => e.ReviewId)
                    .HasColumnName("review_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewText)
                    .IsRequired()
                    .HasColumnName("review_text")
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
