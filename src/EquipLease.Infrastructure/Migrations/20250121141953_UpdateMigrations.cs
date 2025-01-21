using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EquipLease.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlacementContracts_EquipmentTypes_EquipmentTypeId",
                table: "PlacementContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlacementContracts_ProductionFacilities_ProductionFacilityId",
                table: "PlacementContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionFacilities",
                table: "ProductionFacilities");

            migrationBuilder.DropIndex(
                name: "IX_PlacementContracts_EquipmentTypeId",
                table: "PlacementContracts");

            migrationBuilder.DropIndex(
                name: "IX_PlacementContracts_ProductionFacilityId",
                table: "PlacementContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentTypes",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductionFacilities");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "PlacementContracts");

            migrationBuilder.DropColumn(
                name: "ProductionFacilityId",
                table: "PlacementContracts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EquipmentTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ProductionFacilities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "EquipmentTypeCode",
                table: "PlacementContracts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductionFacilityCode",
                table: "PlacementContracts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EquipmentTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionFacilities",
                table: "ProductionFacilities",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentTypes",
                table: "EquipmentTypes",
                column: "Code");

            migrationBuilder.InsertData(
                table: "EquipmentTypes",
                columns: new[] { "Code", "AreaPerUnit", "Name" },
                values: new object[,]
                {
                    { "ET001", 50, "Type A" },
                    { "ET002", 100, "Type B" }
                });

            migrationBuilder.InsertData(
                table: "ProductionFacilities",
                columns: new[] { "Code", "Name", "StandardArea" },
                values: new object[,]
                {
                    { "PF001", "Facility 1", 500 },
                    { "PF002", "Facility 2", 800 }
                });

            migrationBuilder.InsertData(
                table: "PlacementContracts",
                columns: new[] { "Id", "EquipmentQuantity", "EquipmentTypeCode", "ProductionFacilityCode" },
                values: new object[,]
                {
                    { new Guid("86e28658-ee00-42c6-9d77-75a78bebc7a0"), 10, "ET001", "PF001" },
                    { new Guid("e68e6060-026c-4118-83c3-c92a28502c41"), 5, "ET002", "PF002" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_EquipmentTypeCode",
                table: "PlacementContracts",
                column: "EquipmentTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_ProductionFacilityCode",
                table: "PlacementContracts",
                column: "ProductionFacilityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_PlacementContracts_EquipmentTypes_EquipmentTypeCode",
                table: "PlacementContracts",
                column: "EquipmentTypeCode",
                principalTable: "EquipmentTypes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlacementContracts_ProductionFacilities_ProductionFacilityCode",
                table: "PlacementContracts",
                column: "ProductionFacilityCode",
                principalTable: "ProductionFacilities",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlacementContracts_EquipmentTypes_EquipmentTypeCode",
                table: "PlacementContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlacementContracts_ProductionFacilities_ProductionFacilityCode",
                table: "PlacementContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionFacilities",
                table: "ProductionFacilities");

            migrationBuilder.DropIndex(
                name: "IX_PlacementContracts_EquipmentTypeCode",
                table: "PlacementContracts");

            migrationBuilder.DropIndex(
                name: "IX_PlacementContracts_ProductionFacilityCode",
                table: "PlacementContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentTypes",
                table: "EquipmentTypes");

            migrationBuilder.DeleteData(
                table: "PlacementContracts",
                keyColumn: "Id",
                keyValue: new Guid("86e28658-ee00-42c6-9d77-75a78bebc7a0"));

            migrationBuilder.DeleteData(
                table: "PlacementContracts",
                keyColumn: "Id",
                keyValue: new Guid("e68e6060-026c-4118-83c3-c92a28502c41"));

            migrationBuilder.DeleteData(
                table: "EquipmentTypes",
                keyColumn: "Code",
                keyValue: "ET001");

            migrationBuilder.DeleteData(
                table: "EquipmentTypes",
                keyColumn: "Code",
                keyValue: "ET002");

            migrationBuilder.DeleteData(
                table: "ProductionFacilities",
                keyColumn: "Code",
                keyValue: "PF001");

            migrationBuilder.DeleteData(
                table: "ProductionFacilities",
                keyColumn: "Code",
                keyValue: "PF002");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeCode",
                table: "PlacementContracts");

            migrationBuilder.DropColumn(
                name: "ProductionFacilityCode",
                table: "PlacementContracts");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ProductionFacilities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProductionFacilities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentTypeId",
                table: "PlacementContracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductionFacilityId",
                table: "PlacementContracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "EquipmentTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EquipmentTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionFacilities",
                table: "ProductionFacilities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentTypes",
                table: "EquipmentTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_EquipmentTypeId",
                table: "PlacementContracts",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_ProductionFacilityId",
                table: "PlacementContracts",
                column: "ProductionFacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlacementContracts_EquipmentTypes_EquipmentTypeId",
                table: "PlacementContracts",
                column: "EquipmentTypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlacementContracts_ProductionFacilities_ProductionFacilityId",
                table: "PlacementContracts",
                column: "ProductionFacilityId",
                principalTable: "ProductionFacilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
