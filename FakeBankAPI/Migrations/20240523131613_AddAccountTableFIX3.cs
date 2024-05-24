using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTableFIX3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 15, 16, 12, 575, DateTimeKind.Local).AddTicks(1920), new DateTime(2024, 5, 23, 15, 16, 12, 575, DateTimeKind.Local).AddTicks(1975) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 15, 16, 12, 575, DateTimeKind.Local).AddTicks(1979), new DateTime(2024, 5, 23, 15, 16, 12, 575, DateTimeKind.Local).AddTicks(1981) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 13, 48, 15, 96, DateTimeKind.Local).AddTicks(298), new DateTime(2024, 5, 23, 13, 48, 15, 96, DateTimeKind.Local).AddTicks(378) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 13, 48, 15, 96, DateTimeKind.Local).AddTicks(385), new DateTime(2024, 5, 23, 13, 48, 15, 96, DateTimeKind.Local).AddTicks(389) });
        }
    }
}
