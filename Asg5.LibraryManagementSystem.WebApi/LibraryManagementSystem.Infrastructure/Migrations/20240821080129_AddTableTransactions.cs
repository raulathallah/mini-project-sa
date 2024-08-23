using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bookusertransactions",
                columns: table => new
                {
                    bookusertransactionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    locationid = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    isbn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    duedate = table.Column<DateOnly>(type: "date", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookusertransactions", x => x.bookusertransactionid);
                    table.ForeignKey(
                        name: "FK_bookusertransactions_books_bookid",
                        column: x => x.bookid,
                        principalTable: "books",
                        principalColumn: "bookid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookusertransactions_locations_locationid",
                        column: x => x.locationid,
                        principalTable: "locations",
                        principalColumn: "locationid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookusertransactions_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookusertransactions_bookid",
                table: "bookusertransactions",
                column: "bookid");

            migrationBuilder.CreateIndex(
                name: "IX_bookusertransactions_locationid",
                table: "bookusertransactions",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "IX_bookusertransactions_userid",
                table: "bookusertransactions",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookusertransactions");
        }
    }
}
