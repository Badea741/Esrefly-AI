using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esrefly.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDateInAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<string>(
                name: "IncomeType",
                table: "Incomes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "Incomes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Incomes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "Goals",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "Expenses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Expenses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "IncomeType",
                table: "Incomes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                column: "ExternalId");
        }
    }
}
