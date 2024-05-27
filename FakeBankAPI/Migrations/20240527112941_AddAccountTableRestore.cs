using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTableRestore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 27, 13, 29, 41, 157, DateTimeKind.Local).AddTicks(5030), new DateTime(2024, 5, 27, 13, 29, 41, 157, DateTimeKind.Local).AddTicks(5080) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 27, 13, 29, 41, 157, DateTimeKind.Local).AddTicks(5084), new DateTime(2024, 5, 27, 13, 29, 41, 157, DateTimeKind.Local).AddTicks(5086) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 35, 50, 121, DateTimeKind.Local).AddTicks(565), new DateTime(2024, 5, 27, 10, 35, 50, 121, DateTimeKind.Local).AddTicks(617) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 27, 10, 35, 50, 121, DateTimeKind.Local).AddTicks(621), new DateTime(2024, 5, 27, 10, 35, 50, 121, DateTimeKind.Local).AddTicks(623) });
        }
    }
}
