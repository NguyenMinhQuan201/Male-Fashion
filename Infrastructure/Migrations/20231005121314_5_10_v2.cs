using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class _5_10_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branding",
                table: "Categorys");

            migrationBuilder.AddColumn<int>(
                name: "Branding",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branding",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Branding",
                table: "Categorys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
