using Arm.Shop.Core.DTOs;
using Arm.Shop.Core.Services;
using Arm.Shop.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Arm.Shop.Data.Services
{
    public class ImagenesOptions
    {
        public string RutaBase { get; set; } = "wwwroot/imagenes/productos";
    }

    public class ProductoService : IProductoService
    {
        private readonly IDbContextFactory<ArmShopDbContext> _dbFactory;
        private readonly string _rutaBasePublica;

        public ProductoService(IDbContextFactory<ArmShopDbContext> dbFactory, IOptions<ImagenesOptions> options)
        {
            _dbFactory = dbFactory;
            // "wwwroot/imagenes/productos" -> "/imagenes/productos"
            _rutaBasePublica = options.Value.RutaBase.Replace("wwwroot", "");
        }

        // Devuelve lista materializada para simplificar consumo
        public async Task<List<ProductoDestacadoDto>> GetDestacadosAsync(int cantidad = 3)
        {
            await using var context = await _dbFactory.CreateDbContextAsync();

            var productos = await context.Productos
                .AsNoTracking()
                .Include(p => p.ProductoVariaciones)
                .Include(p => p.Imagenes)
                .OrderByDescending(p => p.FechaAlta)
                .Take(cantidad)
                .Select(p => new ProductoDestacadoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion ?? "",
                    // Precio 0 si no hay variaciones
                    Precio = p.ProductoVariaciones
                              .Select(v => (decimal?)v.Precio)
                              .FirstOrDefault() ?? 0m,
                    // Aseguramos incluir el Id en la URL si guardás por carpeta de producto
                    ImagenUrl = p.Imagenes
                        .Where(i => i.EsPrincipal)
                        .Select(i => $"{_rutaBasePublica}/{i.NombreArchivo}")
                        .FirstOrDefault() ?? $"{_rutaBasePublica}/sample.jpg",
                    Reviews = 0
                })
                .ToListAsync();

            return productos;
        }

        public async Task<List<ProductoDto>> GetProductosAsync()
        {
            await using var context = await _dbFactory.CreateDbContextAsync();

            var productos = await context.Productos
                .AsNoTracking()
                .Include(p => p.ProductoVariaciones)
                .Include(p => p.Categoria)
                    .ThenInclude(c => c.CategoriaPadre)
                .ToListAsync();

            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.ProductoVariaciones.FirstOrDefault()?.Precio,
                Stock = p.ProductoVariaciones.Sum(v => v.Stock),
                CategoriaRuta = ConstruirRutaCategoria(p.Categoria),
                Variaciones = p.ProductoVariaciones.Select(v => new ProductoVariacionDto
                {
                    Id = v.Id,
                    Sku = v.Sku,
                    Precio = v.Precio,
                    Stock = v.Stock,
                    Descripcion = $"{v.Sku}"
                }).ToList()
            }).ToList();
        }

        private string ConstruirRutaCategoria(Categoria? categoria)
        {
            if (categoria == null) return string.Empty;

            var partes = new List<string>();
            var actual = categoria;

            while (actual != null)
            {
                partes.Insert(0, actual.Nombre);
                actual = actual.CategoriaPadre;
            }

            return string.Join(" > ", partes);
        }
    }
}