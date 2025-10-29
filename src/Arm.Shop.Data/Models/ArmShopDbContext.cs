using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Arm.Shop.Data.Models;

public partial class ArmShopDbContext : DbContext
{
    public ArmShopDbContext()
    {
    }

    public ArmShopDbContext(DbContextOptions<ArmShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atributo> Atributos { get; set; }

    public virtual DbSet<AtributoValore> AtributoValores { get; set; }

    public virtual DbSet<CarritoItem> CarritoItems { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<EmpresaMetadatum> EmpresaMetadata { get; set; }

    public virtual DbSet<OrdenItem> OrdenItems { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoImagene> ProductoImagenes { get; set; }

    public virtual DbSet<ProductoVariacione> ProductoVariaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LOCALHOST\\SQLEXPRESS;Database=ArmShopDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atributo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Atributo__3214EC075F5F1967");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<AtributoValore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Atributo__3214EC07CE743008");

            entity.Property(e => e.Valor).HasMaxLength(100);

            entity.HasOne(d => d.Atributo).WithMany(p => p.AtributoValores)
                .HasForeignKey(d => d.AtributoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AtributoV__Atrib__5535A963");
        });

        modelBuilder.Entity<CarritoItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarritoI__3214EC0713693407");

            entity.Property(e => e.FechaAgregado)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CarritoItems)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarritoIt__Usuar__693CA210");

            entity.HasOne(d => d.Variacion).WithMany(p => p.CarritoItems)
                .HasForeignKey(d => d.VariacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarritoIt__Varia__6A30C649");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC078B5E1E03");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.ImagenNombreArchivo)
                .HasMaxLength(255)
                .HasDefaultValue("sample.jpg");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.CategoriaPadre).WithMany(p => p.InverseCategoriaPadre)
                .HasForeignKey(d => d.CategoriaPadreId)
                .HasConstraintName("FK_Categorias_CategoriasPadre");
        });

        modelBuilder.Entity<EmpresaMetadatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmpresaM__3214EC0779A0104D");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Direccion).HasMaxLength(300);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FacebookUrl).HasMaxLength(200);
            entity.Property(e => e.InstagramUrl).HasMaxLength(200);
            entity.Property(e => e.LinkedinUrl).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.SitioWeb).HasMaxLength(150);
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.TwitterUrl).HasMaxLength(200);
        });

        modelBuilder.Entity<OrdenItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdenIte__3214EC079DEB7ED9");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Orden).WithMany(p => p.OrdenItems)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenItem__Orden__6477ECF3");

            entity.HasOne(d => d.Variacion).WithMany(p => p.OrdenItems)
                .HasForeignKey(d => d.VariacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenItem__Varia__656C112C");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ordenes__3214EC07AEDDE1F6");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ordenes__Usuario__619B8048");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07976B2516");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(150);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK_Productos_Categorias");
        });

        modelBuilder.Entity<ProductoImagene>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC078B98D5CC");

            entity.Property(e => e.NombreArchivo).HasMaxLength(200);

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductoImagenes)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductoI__Produ__17036CC0");
        });

        modelBuilder.Entity<ProductoVariacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07E529AF6B");

            entity.HasIndex(e => e.Sku, "UQ__Producto__CA1ECF0DCDF19BE1").IsUnique();

            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("SKU");

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductoVariaciones)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductoV__Produ__59FA5E80");

            entity.HasMany(d => d.AtributoValors).WithMany(p => p.Variacions)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductoVariacionValore",
                    r => r.HasOne<AtributoValore>().WithMany()
                        .HasForeignKey("AtributoValorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductoV__Atrib__5DCAEF64"),
                    l => l.HasOne<ProductoVariacione>().WithMany()
                        .HasForeignKey("VariacionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductoV__Varia__5CD6CB2B"),
                    j =>
                    {
                        j.HasKey("VariacionId", "AtributoValorId").HasName("PK__Producto__83051555A151992B");
                        j.ToTable("ProductoVariacionValores");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07515945F8");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534936FE200").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
