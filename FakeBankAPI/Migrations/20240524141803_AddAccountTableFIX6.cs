using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTableFIX6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 24, 16, 18, 2, 942, DateTimeKind.Local).AddTicks(8026), new DateTime(2024, 5, 24, 16, 18, 2, 942, DateTimeKind.Local).AddTicks(8078) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 24, 16, 18, 2, 942, DateTimeKind.Local).AddTicks(8083), new DateTime(2024, 5, 24, 16, 18, 2, 942, DateTimeKind.Local).AddTicks(8085) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 24, 13, 56, 15, 374, DateTimeKind.Local).AddTicks(1994), new DateTime(2024, 5, 24, 13, 56, 15, 374, DateTimeKind.Local).AddTicks(2048) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 24, 13, 56, 15, 374, DateTimeKind.Local).AddTicks(2053), new DateTime(2024, 5, 24, 13, 56, 15, 374, DateTimeKind.Local).AddTicks(2055) });
        }
    }
}
