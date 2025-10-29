using Arm.Shop.Core.DTOs;
using Arm.Shop.Core.Services;
using Arm.Shop.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Arm.Shop.Data.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IDbContextFactory<ArmShopDbContext> _dbFactory;

        public CategoriaService(IDbContextFactory<ArmShopDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<CategoriaDto>> GetDestacadasAsync(int cantidad = 3)
        {
            await using var context = await _dbFactory.CreateDbContextAsync();

            // Traemos las categorías más recientes o con algún criterio de "destacadas"
            var categorias = await context.Categorias
                .AsNoTracking()
                .OrderBy(c => c.Nombre) // Podés cambiar el criterio (ej: más usadas, con flag "Destacada", etc.)
                .Take(cantidad)
                .Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    ImagenUrl = !string.IsNullOrEmpty(c.ImagenNombreArchivo)
                        ? $"/imagenes/categorias/{c.Id}/{c.ImagenNombreArchivo}"
                        : "/imagenes/categorias/sample.jpg"
                })
                .ToListAsync();

            return categorias;
        }
    }
}