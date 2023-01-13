using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class RenameImageColumnsInUserAndPostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerUri",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUri",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "BannerImageName",
                table: "BlogPosts",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureName",
                table: "AspNetUsers",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImageName",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ProfilePictureName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "BannerUri",
                table: "BlogPosts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUri",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
