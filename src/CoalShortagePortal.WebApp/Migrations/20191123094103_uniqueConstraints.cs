using Microsoft.EntityFrameworkCore.Migrations;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class uniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OtherReasonsResponses_DataDate_Station",
                table: "OtherReasonsResponses",
                columns: new[] { "DataDate", "Station" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CriticalCoalResponses_DataDate_Station",
                table: "CriticalCoalResponses",
                columns: new[] { "DataDate", "Station" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoalShortageResponses_DataDate_Station",
                table: "CoalShortageResponses",
                columns: new[] { "DataDate", "Station" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OtherReasonsResponses_DataDate_Station",
                table: "OtherReasonsResponses");

            migrationBuilder.DropIndex(
                name: "IX_CriticalCoalResponses_DataDate_Station",
                table: "CriticalCoalResponses");

            migrationBuilder.DropIndex(
                name: "IX_CoalShortageResponses_DataDate_Station",
                table: "CoalShortageResponses");
        }
    }
}
