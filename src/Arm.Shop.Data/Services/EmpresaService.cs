using Arm.Shop.Core.DTOs;
using Arm.Shop.Core.Services;
using Arm.Shop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Arm.Shop.Data.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly ArmShopDbContext _context;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "EmpresaMetadata";

        public EmpresaService(ArmShopDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<EmpresaMetadataDto?> GetMetadataAsync()
        {
            // Si ya está en cache, devolverlo
            if (_cache.TryGetValue(CacheKey, out EmpresaMetadataDto? cached))
            {
                return cached;
            }

            // Sino, cargar de la BD
            var meta = await _context.EmpresaMetadata
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync();

            if (meta == null) return null;

            var dto = new EmpresaMetadataDto
            {
                Nombre = meta.Nombre,
                Direccion = meta.Direccion,
                Telefono = meta.Telefono,
                Email = meta.Email,
                SitioWeb = meta.SitioWeb,
                FacebookUrl = meta.FacebookUrl,
                InstagramUrl = meta.InstagramUrl,
                TwitterUrl = meta.TwitterUrl,
                LinkedinUrl = meta.LinkedinUrl,
                Descripcion = meta.Descripcion
            };

            // Guardar en cache por 30 minutos (configurable)
            _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(30));

            return dto;
        }
    }
}