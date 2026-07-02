using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_Solution.Migrations
{
    /// <inheritdoc />
    public partial class editUseName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "username");
        }
    }
}
