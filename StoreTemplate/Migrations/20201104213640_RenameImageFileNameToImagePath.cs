using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreTemplate.Migrations
{
    public partial class RenameImageFileNameToImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("ImageFileName", "Products", "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
