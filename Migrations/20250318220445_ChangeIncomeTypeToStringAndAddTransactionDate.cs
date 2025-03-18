using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esrefly.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIncomeTypeToStringAndAddTransactionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IncomeType",
                table: "Incomes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IncomeType",
                table: "Incomes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
