using Microsoft.EntityFrameworkCore.Migrations;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class regiontag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "GeneratingStationForOtherReasons",
                nullable: false,
                defaultValue: "WR");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "GeneratingStationForCriticalCoals",
                nullable: false,
                defaultValue: "WR");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "GeneratingStationForCoalShortages",
                nullable: false,
                defaultValue: "WR");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "GeneratingStationForOtherReasons");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "GeneratingStationForCriticalCoals");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "GeneratingStationForCoalShortages");
        }
    }
}
