using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_Solution.Migrations
{
    /// <inheritdoc />
    public partial class revertDeleteCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Carts",
               columns: table => new
               {
                   cartId = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   nubmberOfItems = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Carts", x => x.cartId);
               });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
