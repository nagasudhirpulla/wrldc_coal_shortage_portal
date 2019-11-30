using Microsoft.EntityFrameworkCore.Migrations;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class addSerialNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SerialNum",
                table: "GeneratingStationForOtherReasons",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "SerialNum",
                table: "GeneratingStationForCriticalCoals",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "SerialNum",
                table: "GeneratingStationForCoalShortages",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNum",
                table: "GeneratingStationForOtherReasons");

            migrationBuilder.DropColumn(
                name: "SerialNum",
                table: "GeneratingStationForCriticalCoals");

            migrationBuilder.DropColumn(
                name: "SerialNum",
                table: "GeneratingStationForCoalShortages");
        }
    }
}
