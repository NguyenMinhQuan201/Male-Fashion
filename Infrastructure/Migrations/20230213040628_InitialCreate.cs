using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImg_Products_ProductId",
                table: "ProductImg");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImg",
                table: "ProductImg");

            migrationBuilder.RenameTable(
                name: "ProductImg",
                newName: "ProductImgs");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImg_ProductId",
                table: "ProductImgs",
                newName: "IX_ProductImgs_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImgs",
                table: "ProductImgs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImgs_Products_ProductId",
                table: "ProductImgs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImgs_Products_ProductId",
                table: "ProductImgs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImgs",
                table: "ProductImgs");

            migrationBuilder.RenameTable(
                name: "ProductImgs",
                newName: "ProductImg");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImgs_ProductId",
                table: "ProductImg",
                newName: "IX_ProductImg_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImg",
                table: "ProductImg",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImg_Products_ProductId",
                table: "ProductImg",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
