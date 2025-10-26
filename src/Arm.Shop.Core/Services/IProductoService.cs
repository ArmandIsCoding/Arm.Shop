using Arm.Shop.Core.DTOs;

namespace Arm.Shop.Core.Services
{
    public interface IProductoService
    {
        public Task<List<ProductoDto>> GetProductosAsync();
    }
}