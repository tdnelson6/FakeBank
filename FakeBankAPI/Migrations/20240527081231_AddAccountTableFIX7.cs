﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTableFIX7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "1234567890", new DateTime(2024, 5, 27, 10, 12, 30, 586, DateTimeKind.Local).AddTicks(2887), new DateTime(2024, 5, 27, 10, 12, 30, 586, DateTimeKind.Local).AddTicks(2949) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNumber", "CreatedAt", "UpdatedAt" },
                values: new object[] { "0987654321", new DateTime(2024, 5, 27, 10, 12, 30, 586, DateTimeKind.Local).AddTicks(2954), new DateTime(2024, 5, 27, 10, 12, 30, 586, DateTimeKind.Local).AddTicks(2956) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { 1234567890, new DateTime(2024, 5, 24, 16, 18, 2, 942, DateTimeKind.Local).AddTicks(8026), new DateTime(2024, 5, 24, 16, 18, 2, 942, DateTimeKind.Local).AddTicks(8078) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNumber", "CreatedAt", "UpdatedAt" },
                values: new object[] { 987654321, new DateTime(2024, 5, 24, 16, 18, 2, 942, DateTimeKind.Local).AddTicks(8083), new DateTime(2024, 5, 24, 16, 18, 2, 942, DateTimeKind.Local).AddTicks(8085) });
        }
    }
}
