using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreTemplate.Migrations
{
    public partial class FixProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
