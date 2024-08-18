using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceApplication.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "CategoryID");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Products",
                newName: "ProductID");
        }
    }
}
