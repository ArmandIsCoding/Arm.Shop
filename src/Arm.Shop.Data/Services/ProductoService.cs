using Arm.Shop.Core.DTOs;
using Arm.Shop.Core.Services;
using Arm.Shop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Arm.Shop.Data.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ArmShopDbContext _context;

        public ProductoService(ArmShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDto>> GetProductosAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.ProductoVariaciones)
                .Include(p => p.Categoria)
                    .ThenInclude(c => c.CategoriaPadre) // para poder armar la ruta
                .ToListAsync();

            return [.. productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.ProductoVariaciones.FirstOrDefault()?.Precio,
                Stock = p.ProductoVariaciones.Sum(v => v.Stock),
                CategoriaRuta = ConstruirRutaCategoria(p.Categoria),
                Variaciones = [.. p.ProductoVariaciones.Select(v => new ProductoVariacionDto
                {
                    Id = v.Id,
                    Sku = v.Sku,
                    Precio = v.Precio,
                    Stock = v.Stock,
                    Descripcion = $"{v.Sku}"
                })]
            })];
        }

        // 🔁 Helper para construir la ruta completa de categorías
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