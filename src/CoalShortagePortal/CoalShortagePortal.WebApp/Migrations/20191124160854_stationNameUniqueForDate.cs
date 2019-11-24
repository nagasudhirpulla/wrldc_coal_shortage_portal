using Microsoft.EntityFrameworkCore.Migrations;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class stationNameUniqueForDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "OtherReasonsResponses",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CriticalCoalResponses",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CoalShortageResponses",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStationForOtherReasons_EndDate_Name",
                table: "GeneratingStationForOtherReasons",
                columns: new[] { "EndDate", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStationForOtherReasons_StartDate_Name",
                table: "GeneratingStationForOtherReasons",
                columns: new[] { "StartDate", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStationForCriticalCoals_EndDate_Name",
                table: "GeneratingStationForCriticalCoals",
                columns: new[] { "EndDate", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStationForCriticalCoals_StartDate_Name",
                table: "GeneratingStationForCriticalCoals",
                columns: new[] { "StartDate", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStationForCoalShortages_EndDate_Name",
                table: "GeneratingStationForCoalShortages",
                columns: new[] { "EndDate", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStationForCoalShortages_StartDate_Name",
                table: "GeneratingStationForCoalShortages",
                columns: new[] { "StartDate", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GeneratingStationForOtherReasons_EndDate_Name",
                table: "GeneratingStationForOtherReasons");

            migrationBuilder.DropIndex(
                name: "IX_GeneratingStationForOtherReasons_StartDate_Name",
                table: "GeneratingStationForOtherReasons");

            migrationBuilder.DropIndex(
                name: "IX_GeneratingStationForCriticalCoals_EndDate_Name",
                table: "GeneratingStationForCriticalCoals");

            migrationBuilder.DropIndex(
                name: "IX_GeneratingStationForCriticalCoals_StartDate_Name",
                table: "GeneratingStationForCriticalCoals");

            migrationBuilder.DropIndex(
                name: "IX_GeneratingStationForCoalShortages_EndDate_Name",
                table: "GeneratingStationForCoalShortages");

            migrationBuilder.DropIndex(
                name: "IX_GeneratingStationForCoalShortages_StartDate_Name",
                table: "GeneratingStationForCoalShortages");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "OtherReasonsResponses",
                type: "text",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CriticalCoalResponses",
                type: "text",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CoalShortageResponses",
                type: "text",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
