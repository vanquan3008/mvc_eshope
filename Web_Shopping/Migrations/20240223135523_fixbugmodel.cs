using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class fixbugmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserNamr",
                table: "Order",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "OrderCoed",
                table: "Order",
                newName: "OrderCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Order",
                newName: "UserNamr");

            migrationBuilder.RenameColumn(
                name: "OrderCode",
                table: "Order",
                newName: "OrderCoed");
        }
    }
}
