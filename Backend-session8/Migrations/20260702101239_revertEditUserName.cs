using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_Solution.Migrations
{
    /// <inheritdoc />
    public partial class revertEditUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "Name",
               table: "Users",
               newName: "username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
