using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicAPI.Migrations
{
    public partial class UpdateArtist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Artists",
                newName: "ImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Artists",
                newName: "MyProperty");
        }
    }
}
