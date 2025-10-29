using Arm.Shop.Core.DTOs;
using Arm.Shop.Core.Services;
using Arm.Shop.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Arm.Shop.Data.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IDbContextFactory<ArmShopDbContext> _dbFactory;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "EmpresaMetadata";

        public EmpresaService(IDbContextFactory<ArmShopDbContext> dbFactory, IMemoryCache cache)
        {
            _dbFactory = dbFactory;
            _cache = cache;
        }

        public async Task<EmpresaMetadataDto?> GetMetadataAsync()
        {
            if (_cache.TryGetValue(CacheKey, out EmpresaMetadataDto? cached))
                return cached;

            await using var context = await _dbFactory.CreateDbContextAsync();

            var meta = await context.EmpresaMetadata
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

            _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(30));
            return dto;
        }
    }
}