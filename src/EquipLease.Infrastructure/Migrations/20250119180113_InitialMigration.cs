using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipLease.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaPerUnit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionFacilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StandardArea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlacementContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacementContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacementContracts_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlacementContracts_ProductionFacilities_ProductionFacilityId",
                        column: x => x.ProductionFacilityId,
                        principalTable: "ProductionFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_EquipmentTypeId",
                table: "PlacementContracts",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_ProductionFacilityId",
                table: "PlacementContracts",
                column: "ProductionFacilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlacementContracts");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "ProductionFacilities");
        }
    }
}
