using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationStocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_stocks_locationid",
                table: "stocks",
                column: "locationid");

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_books_bookid",
                table: "stocks",
                column: "bookid",
                principalTable: "books",
                principalColumn: "bookid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_locations_locationid",
                table: "stocks",
                column: "locationid",
                principalTable: "locations",
                principalColumn: "locationid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stocks_books_bookid",
                table: "stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_stocks_locations_locationid",
                table: "stocks");

            migrationBuilder.DropIndex(
                name: "IX_stocks_locationid",
                table: "stocks");
        }
    }
}
