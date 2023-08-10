using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppFin.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconImage",
                table: "Products",
                newName: "Icon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Products",
                newName: "IconImage");
        }
    }
}
