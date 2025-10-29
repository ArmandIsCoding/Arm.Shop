using Arm.Shop.Core.DTOs;

namespace Arm.Shop.Core.Services
{
    public interface ICategoriaService
    {
        public Task<List<CategoriaDto>> GetDestacadasAsync(int cantidad = 3);
    }
}
