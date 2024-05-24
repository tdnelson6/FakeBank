using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FakeBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTableFIX1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "Accounts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountType", "Balance", "CreatedAt", "Currency", "InterestRate", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "1234567890", "Savings", 1000.0, new DateTime(2024, 5, 23, 13, 10, 33, 703, DateTimeKind.Local).AddTicks(3834), "USD", 0.050000000000000003, "John Doe", new DateTime(2024, 5, 23, 13, 10, 33, 703, DateTimeKind.Local).AddTicks(3888) },
                    { 2, "0987654321", "Checking", 500.0, new DateTime(2024, 5, 23, 13, 10, 33, 703, DateTimeKind.Local).AddTicks(3894), "USD", 0.01, "Jane Doe", new DateTime(2024, 5, 23, 13, 10, 33, 703, DateTimeKind.Local).AddTicks(3896) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
