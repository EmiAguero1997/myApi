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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(producto => producto.Id);
            entity.Property(producto => producto.Nombre).HasMaxLength(120).IsRequired();
            entity.Property(producto => producto.UnidadDeMedida).HasMaxLength(40).IsRequired();
            entity.Property(producto => producto.Cantidad).HasPrecision(18, 2);
        });
        modelBuilder.Entity<Almacen>(entity =>
        {
            entity.HasKey(almacen => almacen.Id);
            entity.Property(almacen => almacen.Nombre).HasMaxLength(120).IsRequired();
            entity.Property(almacen => almacen.Direccion).HasMaxLength(200).IsRequired();
        });
    }
}
