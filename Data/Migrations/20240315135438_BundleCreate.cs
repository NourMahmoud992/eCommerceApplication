using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceApplication.Data.Migrations
{
    public partial class BundleCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BundlesID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Bundle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundle", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BundlesID",
                table: "Products",
                column: "BundlesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Bundle_BundlesID",
                table: "Products",
                column: "BundlesID",
                principalTable: "Bundle",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Bundle_BundlesID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Bundle");

            migrationBuilder.DropIndex(
                name: "IX_Products_BundlesID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BundlesID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Products");
        }
    }
}
