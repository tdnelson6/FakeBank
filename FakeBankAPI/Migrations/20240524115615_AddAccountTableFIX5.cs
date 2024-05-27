using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTableFIX5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "Accounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNumber", "CreatedAt", "UpdatedAt" },
                values: new object[] { 1234567890, new DateTime(2024, 5, 24, 13, 56, 15, 374, DateTimeKind.Local).AddTicks(1994), new DateTime(2024, 5, 24, 13, 56, 15, 374, DateTimeKind.Local).AddTicks(2048) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNumber", "CreatedAt", "UpdatedAt" },
                values: new object[] { 987654321, new DateTime(2024, 5, 24, 13, 56, 15, 374, DateTimeKind.Local).AddTicks(2053), new DateTime(2024, 5, 24, 13, 56, 15, 374, DateTimeKind.Local).AddTicks(2055) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNumber", "CreatedAt", "UpdatedAt" },
                values: new object[] { "1234567890", new DateTime(2024, 5, 24, 10, 35, 43, 543, DateTimeKind.Local).AddTicks(2520), new DateTime(2024, 5, 24, 10, 35, 43, 543, DateTimeKind.Local).AddTicks(2595) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNumber", "CreatedAt", "UpdatedAt" },
                values: new object[] { "0987654321", new DateTime(2024, 5, 24, 10, 35, 43, 543, DateTimeKind.Local).AddTicks(2609), new DateTime(2024, 5, 24, 10, 35, 43, 543, DateTimeKind.Local).AddTicks(2611) });
        }
    }
}
