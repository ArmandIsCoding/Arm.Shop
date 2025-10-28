using Arm.Shop.Core.DTOs;

namespace Arm.Shop.Core.Services
{
    public interface IEmpresaService
    {
        Task<EmpresaMetadataDto?> GetMetadataAsync();
    }
}