using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTableFIX4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 24, 10, 35, 43, 543, DateTimeKind.Local).AddTicks(2520), new DateTime(2024, 5, 24, 10, 35, 43, 543, DateTimeKind.Local).AddTicks(2595) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 24, 10, 35, 43, 543, DateTimeKind.Local).AddTicks(2609), new DateTime(2024, 5, 24, 10, 35, 43, 543, DateTimeKind.Local).AddTicks(2611) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
