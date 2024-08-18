using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceApplication.Data.Migrations
{
    public partial class UpdatedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Bundle_BundlesID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bundle",
                table: "Bundle");

            migrationBuilder.RenameTable(
                name: "Bundle",
                newName: "Bundles");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bundles",
                table: "Bundles",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Bundles_BundlesID",
                table: "Products",
                column: "BundlesID",
                principalTable: "Bundles",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Bundles_BundlesID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bundles",
                table: "Bundles");

            migrationBuilder.RenameTable(
                name: "Bundles",
                newName: "Bundle");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryID",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bundle",
                table: "Bundle",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Bundle_BundlesID",
                table: "Products",
                column: "BundlesID",
                principalTable: "Bundle",
                principalColumn: "ID");
        }
    }
}
