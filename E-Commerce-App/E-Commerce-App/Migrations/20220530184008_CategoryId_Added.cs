using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Commerce_App.Migrations
{
    public partial class CategoryId_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_categoryId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_categoryId",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "products",
                newName: "IX_products_categoryId");

            migrationBuilder.AlterColumn<int>(
                name: "categoryId",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_categoryId",
                table: "products",
                column: "categoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
