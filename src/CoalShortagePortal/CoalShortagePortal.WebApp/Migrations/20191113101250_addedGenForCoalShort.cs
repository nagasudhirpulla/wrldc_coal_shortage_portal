using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class addedGenForCoalShort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratingStationForCoalShortages",
                columns: table => new
                {
                    GeneratingStationForCoalShortageId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Agency = table.Column<string>(nullable: true),
                    Capacity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratingStationForCoalShortages", x => x.GeneratingStationForCoalShortageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratingStationForCoalShortages");
        }
    }
}
