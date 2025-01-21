using EquipLease.Infrastructure.Common;
using EquipLease.Infrastructure.DTOs;

namespace EquipLease.Infrastructure.Interfaces;

public interface IPlacementContractService
{
    Task<Result<PlacementContractDto>> CreatePlacementContractAsync(CreatePlacementContractDto dto);
    Task<Result<List<PlacementContractDto>>> GetPlacementContractsAsync();
}

