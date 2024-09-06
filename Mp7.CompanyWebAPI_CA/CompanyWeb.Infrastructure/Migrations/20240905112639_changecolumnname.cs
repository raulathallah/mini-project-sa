using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changecolumnname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bookrequestid",
                table: "leaverequests",
                newName: "leaverequestid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "leaverequestid",
                table: "leaverequests",
                newName: "bookrequestid");
        }
    }
}
