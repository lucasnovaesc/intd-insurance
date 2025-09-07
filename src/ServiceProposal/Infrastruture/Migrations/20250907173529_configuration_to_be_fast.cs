using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class configuration_to_be_fast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Address", "DateCreation", "DateModification", "Email", "Name", "PhoneNumber" },
                values: new object[] { new Guid("690c6507-1c3b-4e84-a77e-8779fbba038d"), "rua", new DateTime(2025, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), "gustavo@gmail.com", "gustavo", "12312312312" });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "ProductTypeId", "DateCreation", "DateModification", "Description", "Name" },
                values: new object[] { new Guid("0fa7d783-e5b6-4317-ae7e-e932d1a8d92b"), new DateTime(2025, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), "seguro de via cobertura media", "seguro de vida" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "DateCreation", "DateModification", "Description", "Name", "Price", "ProductTypeId" },
                values: new object[] { new Guid("50beec7c-b8f6-4edb-9730-6517fdf64b2f"), new DateTime(2025, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), "cobertura bronze com 50 hospitais", "cobertura bronze", 100m, new Guid("0fa7d783-e5b6-4317-ae7e-e932d1a8d92b") });

            migrationBuilder.InsertData(
                table: "Proposal",
                columns: new[] { "ProposalId", "CustomerId", "DateCreation", "DateModification", "ProductId", "ProposalNumber", "ProposalStatusId" },
                values: new object[] { new Guid("cd8b89cc-92b3-4a3b-9fe4-ef2f819c9a1d"), new Guid("690c6507-1c3b-4e84-a77e-8779fbba038d"), new DateTime(2025, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), new Guid("50beec7c-b8f6-4edb-9730-6517fdf64b2f"), 1L, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Proposal",
                keyColumn: "ProposalId",
                keyValue: new Guid("cd8b89cc-92b3-4a3b-9fe4-ef2f819c9a1d"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: new Guid("690c6507-1c3b-4e84-a77e-8779fbba038d"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("50beec7c-b8f6-4edb-9730-6517fdf64b2f"));

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "ProductTypeId",
                keyValue: new Guid("0fa7d783-e5b6-4317-ae7e-e932d1a8d92b"));
        }
    }
}
