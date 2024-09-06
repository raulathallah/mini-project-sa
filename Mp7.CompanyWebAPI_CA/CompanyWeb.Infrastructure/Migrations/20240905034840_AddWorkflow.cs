using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompanyWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "leaverequests",
                columns: table => new
                {
                    bookrequestid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeid = table.Column<int>(type: "integer", nullable: false),
                    leavetype = table.Column<string>(type: "text", nullable: true),
                    leavereason = table.Column<string>(type: "text", nullable: true),
                    startdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    enddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    processid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leaverequests", x => x.bookrequestid);
                });

            migrationBuilder.CreateTable(
                name: "nextsteprules",
                columns: table => new
                {
                    ruleid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currentstepid = table.Column<int>(type: "integer", nullable: false),
                    nextstepid = table.Column<int>(type: "integer", nullable: false),
                    conditiontype = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    conditionvalue = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nextsteprules", x => x.ruleid);
                });

            migrationBuilder.CreateTable(
                name: "process",
                columns: table => new
                {
                    processid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflowid = table.Column<int>(type: "integer", nullable: false),
                    requesterid = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    currentstepid = table.Column<int>(type: "integer", nullable: false),
                    requestdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_process", x => x.processid);
                });

            migrationBuilder.CreateTable(
                name: "workflow",
                columns: table => new
                {
                    workflowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflowname = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow", x => x.workflowid);
                });

            migrationBuilder.CreateTable(
                name: "workflowaction",
                columns: table => new
                {
                    actionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    processid = table.Column<int>(type: "integer", nullable: false),
                    stepid = table.Column<int>(type: "integer", nullable: false),
                    actorid = table.Column<string>(type: "text", nullable: true),
                    action = table.Column<string>(type: "text", nullable: true),
                    actiondate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflowaction", x => x.actionid);
                });

            migrationBuilder.CreateTable(
                name: "workflowsequences",
                columns: table => new
                {
                    stepid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflowid = table.Column<int>(type: "integer", nullable: false),
                    steporder = table.Column<int>(type: "integer", nullable: false),
                    stepname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    requiredrole = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflowsequences", x => x.stepid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leaverequests");

            migrationBuilder.DropTable(
                name: "nextsteprules");

            migrationBuilder.DropTable(
                name: "process");

            migrationBuilder.DropTable(
                name: "workflow");

            migrationBuilder.DropTable(
                name: "workflowaction");

            migrationBuilder.DropTable(
                name: "workflowsequences");
        }
    }
}
