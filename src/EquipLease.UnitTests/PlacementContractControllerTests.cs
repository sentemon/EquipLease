using EquipLease.Api.Controllers;
using EquipLease.Infrastructure;
using EquipLease.Infrastructure.DTOs;
using EquipLease.Infrastructure.Interfaces;
using EquipLease.Infrastructure.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EquipLease.UnitTests;

public class PlacementContractControllerIntegrationTests
{
    private readonly AppDbContext _context;
    private readonly PlacementContractController _controller;

    public PlacementContractControllerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        IPlacementContractService service = new PlacementContractService(_context, Mock.Of<ILogger<PlacementContractService>>());
        _controller = new PlacementContractController(service);
    }

    [Fact]
    public async Task GetPlacementContracts_ShouldReturnAllSeedData()
    {
        // Act
        var result = await _controller.GetPlacementContracts();

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.StatusCode.Should().Be(200);

        var contracts = okResult?.Value as List<PlacementContractDto>;
        contracts.Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(c => c.ProductionFacilityName == "Facility 1" && c.EquipmentTypeName == "Type A")
            .And.Contain(c => c.ProductionFacilityName == "Facility 2" && c.EquipmentTypeName == "Type B");
    }

    [Fact]
    public async Task CreatePlacementContract_ShouldAddNewContract()
    {
        // Arrange
        var createDto = new CreatePlacementContractDto("PF001", "ET002", 5);

        // Act
        var result = await _controller.CreatePlacementContract(createDto);

        // Assert
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        createdResult?.StatusCode.Should().Be(201);

        var newContract = createdResult?.Value as PlacementContractDto;
        newContract.Should().NotBeNull();
        newContract?.ProductionFacilityName.Should().Be("Facility 1");
        newContract?.EquipmentTypeName.Should().Be("Type B");
        newContract?.EquipmentQuantity.Should().Be(5);

        var contracts = await _context.PlacementContracts.ToListAsync();
        contracts.Should().HaveCount(3);
    }

    [Fact]
    public async Task CreatePlacementContract_ShouldReturnBadRequest_WhenInvalidFacility()
    {
        // Arrange
        var createDto = new CreatePlacementContractDto("InvalidFacility", "ET002", 5);

        // Act
        var result = await _controller.CreatePlacementContract(createDto);

        // Assert
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult?.StatusCode.Should().Be(400);
        badRequestResult?.Value.Should().Be("Production facility not found.");

        var contracts = await _context.PlacementContracts.ToListAsync();
        contracts.Should().HaveCount(2);
    }

    [Fact]
    public async Task CreatePlacementContract_ShouldReturnBadRequest_WhenAreaNotEnough()
    {
        // Arrange
        var createDto = new CreatePlacementContractDto("PF001", "ET001", 100);

        // Act
        var result = await _controller.CreatePlacementContract(createDto);

        // Assert
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult?.StatusCode.Should().Be(400);
        badRequestResult?.Value.Should().Be("Not enough available area in the production facility.");

        var contracts = await _context.PlacementContracts.ToListAsync();
        contracts.Should().HaveCount(2);
    }
}