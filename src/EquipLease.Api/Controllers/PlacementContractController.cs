using EquipLease.Infrastructure.DTOs;
using EquipLease.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EquipLease.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlacementContractController : ControllerBase
{
    private readonly IPlacementContractService _placementContractService;

    public PlacementContractController(IPlacementContractService placementContractService)
    {
        _placementContractService = placementContractService;
    }

    // POST: api/PlacementContract
    [HttpPost]
    public async Task<ActionResult<PlacementContractDto>> CreatePlacementContract([FromBody] CreatePlacementContractDto dto)
    {
        var result = await _placementContractService.CreatePlacementContractAsync(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetPlacementContracts), new { id = result.Response?.EquipmentQuantity }, result.Response);
    }

    // GET: api/PlacementContract
    [HttpGet]
    public async Task<ActionResult<List<PlacementContractDto>>> GetPlacementContracts()
    {
        var contracts = await _placementContractService.GetPlacementContractsAsync();
        return Ok(contracts.Response);
    }
}