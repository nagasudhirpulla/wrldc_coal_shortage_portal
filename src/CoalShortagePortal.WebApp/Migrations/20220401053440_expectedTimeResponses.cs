using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class expectedTimeResponses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpectedRevivalResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataDate = table.Column<DateTime>(type: "date", nullable: false),
                    RTOutageId = table.Column<int>(type: "integer", nullable: false),
                    ElementOwners = table.Column<string>(type: "text", nullable: false),
                    ElementName = table.Column<string>(type: "text", nullable: false),
                    InstalledCapacity = table.Column<double>(type: "double precision", nullable: false),
                    OutageReason = table.Column<string>(type: "text", nullable: false),
                    OutageType = table.Column<string>(type: "text", nullable: false),
                    OutageDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpectedRevivalTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedById = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectedRevivalResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpectedRevivalResponses_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpectedRevivalResponses_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedRevivalResponses_CreatedById",
                table: "ExpectedRevivalResponses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedRevivalResponses_DataDate_RTOutageId",
                table: "ExpectedRevivalResponses",
                columns: new[] { "DataDate", "RTOutageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedRevivalResponses_LastModifiedById",
                table: "ExpectedRevivalResponses",
                column: "LastModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpectedRevivalResponses");
        }
    }
}
