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
            return await _context.Productos
                .Include(p => p.ProductoVariaciones)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.ProductoVariaciones.FirstOrDefault().Precio,
                    Stock = p.ProductoVariaciones.Sum(v => v.Stock),
                    Variaciones = p.ProductoVariaciones.Select(v => new ProductoVariacionDto
                    {
                        Id = v.Id,
                        Sku = v.Sku,
                        Precio = v.Precio,
                        Stock = v.Stock,
                        Descripcion = $"{v.Sku}"
                    }).ToList()
                })
                .ToListAsync();
        }

    }
}