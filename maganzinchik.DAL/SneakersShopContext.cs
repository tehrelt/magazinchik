using System;
using System.Collections.Generic;
using maganzinchik.DAL.domain;
using Microsoft.EntityFrameworkCore;

namespace maganzinchik.DAL;

public partial class SneakersShopContext : DbContext
{
    public SneakersShopContext()
    {
    }

    public SneakersShopContext(DbContextOptions<SneakersShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cloth> Cloths { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Sneaker> Sneakers { get; set; }

    public virtual DbSet<SneakerSize> SneakerSizes { get; set; }

    public virtual DbSet<SneakersPhoto> SneakersPhotos { get; set; }

    public virtual DbSet<ZipType> ZipTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=sneakers_shop;Uid=root;Pwd=1234;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("brand");

            entity.HasIndex(e => e.ManufacturerId, "manufacturer_id");

            entity.HasIndex(e => e.Name, "name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Brands)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("brand_ibfk_1");
        });

        modelBuilder.Entity<Cloth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cloth");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("manufacturer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("photo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Sneaker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sneaker");

            entity.HasIndex(e => e.BrandId, "brand_id");

            entity.HasIndex(e => e.ClothId, "cloth_id");

            entity.HasIndex(e => e.SneakerSizeId, "sn_size_type");

            entity.HasIndex(e => e.ZipTypeId, "zip_type_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.ClothId).HasColumnName("cloth_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("datetime")
                .HasColumnName("release_date");
            entity.Property(e => e.SneakerSizeId).HasColumnName("sn_size_type");
            entity.Property(e => e.Weight)
                .HasColumnType("double(8,2)")
                .HasColumnName("weight");
            entity.Property(e => e.ZipTypeId).HasColumnName("zip_type_id");

            entity.HasOne(d => d.Brand).WithMany(p => p.Sneakers)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("sneaker_ibfk_1");

            entity.HasOne(d => d.Cloth).WithMany(p => p.Sneakers)
                .HasForeignKey(d => d.ClothId)
                .HasConstraintName("sneaker_ibfk_2");

            entity.HasOne(d => d.SneakerSize).WithMany(p => p.Sneakers)
                .HasForeignKey(d => d.SneakerSizeId)
                .HasConstraintName("sneaker_ibfk_4");

            entity.HasOne(d => d.ZipType).WithMany(p => p.Sneakers)
                .HasForeignKey(d => d.ZipTypeId)
                .HasConstraintName("sneaker_ibfk_3");
        });

        modelBuilder.Entity<SneakerSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sneaker_size");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CmSize)
                .HasColumnType("double(3,1)")
                .HasColumnName("cm_size");
            entity.Property(e => e.EuSize)
                .HasColumnType("double(3,1)")
                .HasColumnName("eu_size");
            entity.Property(e => e.UsSize)
                .HasColumnType("double(3,1)")
                .HasColumnName("us_size");
        });

        modelBuilder.Entity<SneakersPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sneakers_photo");

            entity.HasIndex(e => e.PhotoId, "photo_id");

            entity.HasIndex(e => e.SneakerId, "sneaker_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PhotoId).HasColumnName("photo_id");
            entity.Property(e => e.SneakerId).HasColumnName("sneaker_id");

            entity.HasOne(d => d.Photo).WithMany(p => p.SneakersPhotos)
                .HasForeignKey(d => d.PhotoId)
                .HasConstraintName("sneakers_photo_ibfk_2");

            entity.HasOne(d => d.Sneaker).WithMany(p => p.SneakersPhotos)
                .HasForeignKey(d => d.SneakerId)
                .HasConstraintName("sneakers_photo_ibfk_1");
        });

        modelBuilder.Entity<ZipType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("zip_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
