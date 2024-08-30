using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addappuserid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RefreshTokens",
                newName: "RefreshTokenId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "RefreshTokens",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenId",
                table: "RefreshTokens",
                newName: "Id");
        }
    }
}
