using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment2.DAL.Migrations
{
    public partial class MoveNumberOfToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfImages",
                table: "SlideshowFiles");

            migrationBuilder.DropColumn(
                name: "NumberOfVideos",
                table: "SlideshowFiles");

            migrationBuilder.DropColumn(
                name: "NumberOfImages",
                table: "AlbumFiles");

            migrationBuilder.DropColumn(
                name: "NumberOfVideos",
                table: "AlbumFiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfImages",
                table: "SlideshowFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVideos",
                table: "SlideshowFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfImages",
                table: "AlbumFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVideos",
                table: "AlbumFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
