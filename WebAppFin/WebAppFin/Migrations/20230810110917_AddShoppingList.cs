using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppFin.Migrations
{
    public partial class AddShoppingList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ShoppingLists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserName",
                table: "ShoppingLists",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingLists_AspNetUsers_UserName",
                table: "ShoppingLists",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingLists_AspNetUsers_UserName",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_UserName",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ShoppingLists");
        }
    }
}
