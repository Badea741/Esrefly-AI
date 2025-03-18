using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esrefly.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactoinDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId1",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Users_UserId1",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_UserId1",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Users_ExternalId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_UserId1",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Goals_UserId1",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserId1",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Expenses");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Incomes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "TransactionDate",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Expenses");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Incomes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Goals",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Expenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId1",
                table: "Incomes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId1",
                table: "Goals",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId1",
                table: "Expenses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId1",
                table: "Expenses",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Users_UserId1",
                table: "Goals",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_UserId1",
                table: "Incomes",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
