using Arm.Shop.Core.DTOs;

namespace Arm.Shop.Core.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> GetProductosAsync();
        Task<List<ProductoDestacadoDto>> GetDestacadosAsync(int cantidad = 3);
    }
}