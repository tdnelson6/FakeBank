using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTableFIX2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2024, 5, 23, 13, 48, 15, 96, DateTimeKind.Local).AddTicks(298), new DateTime(2024, 5, 23, 13, 48, 15, 96, DateTimeKind.Local).AddTicks(378) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 13, 48, 15, 96, DateTimeKind.Local).AddTicks(385), new DateTime(2024, 5, 23, 13, 48, 15, 96, DateTimeKind.Local).AddTicks(389) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2024, 5, 23, 13, 10, 33, 703, DateTimeKind.Local).AddTicks(3834), new DateTime(2024, 5, 23, 13, 10, 33, 703, DateTimeKind.Local).AddTicks(3888) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 23, 13, 10, 33, 703, DateTimeKind.Local).AddTicks(3894), new DateTime(2024, 5, 23, 13, 10, 33, 703, DateTimeKind.Local).AddTicks(3896) });
        }
    }
}
