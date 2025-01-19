using EquipLease.Infrastructure.DTOs;

namespace EquipLease.Infrastructure.Interfaces;

public interface IPlacementContractService
{
    Task<PlacementContractDto?> CreatePlacementContractAsync(CreatePlacementContractDto dto);
    Task<List<PlacementContractDto>> GetPlacementContractsAsync();
}

