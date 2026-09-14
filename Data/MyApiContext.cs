using Microsoft.EntityFrameworkCore;
using myApi.Models;

namespace myApi.Data;

public class MyApiContext : DbContext
{
    public MyApiContext(DbContextOptions<MyApiContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Almacen> Almacen => Set<Almacen>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<AlmacenProducto> AlmacenProducto => Set<AlmacenProducto>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(producto => producto.Id);
            entity.Property(producto => producto.Nombre).HasMaxLength(120).IsRequired();
            entity.Property(producto => producto.UnidadDeMedida).HasMaxLength(40).IsRequired();
        });
        modelBuilder.Entity<Almacen>(entity =>
        {
            entity.HasKey(almacen => almacen.Id);
            entity.Property(almacen => almacen.Nombre).HasMaxLength(120).IsRequired();
            entity.Property(almacen => almacen.Direccion).HasMaxLength(200).IsRequired();
        });
        modelBuilder.Entity<AlmacenProducto>( entity =>
        {
            entity.HasKey(ap => new { ap.AlmacenId, ap.ProductoId });
            entity.HasOne(ap => ap.Almacen)
          .WithMany(a => a.AlmacenProductos)
          .HasForeignKey(ap => ap.AlmacenId);
          entity.HasOne(ap => ap.Producto)
          .WithMany(p => p.AlmacenProductos)
          .HasForeignKey(ap => ap.ProductoId);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(usuario => usuario.Id);
            entity.Property(usuario => usuario.Nombre).HasMaxLength(120).IsRequired();
            entity.Property(usuario => usuario.Correo).HasMaxLength(200).IsRequired();
            entity.Property(usuario => usuario.Contrasena).HasMaxLength(200).IsRequired();
            entity.Property(usuario => usuario.Rol).HasMaxLength(40).IsRequired();
        });
        
    }
}
