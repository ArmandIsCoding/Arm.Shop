using Microsoft.AspNetCore.Http;

namespace Arm.Shop.Core.Services
{
    public interface IImagenService
    {
        Task GuardarImagenAsync(int productoId, IFormFile archivo, bool esPrincipal);
        Task<IEnumerable<string>> ObtenerImagenesAsync(int productoId);
        Task<string?> ObtenerImagenPrincipalAsync(int productoId);
    }
}
