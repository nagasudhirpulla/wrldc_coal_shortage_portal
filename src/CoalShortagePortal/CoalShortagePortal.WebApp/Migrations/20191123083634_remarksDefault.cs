using Microsoft.EntityFrameworkCore.Migrations;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class remarksDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "OtherReasonsResponses",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CriticalCoalResponses",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CoalShortageResponses",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "OtherReasonsResponses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CriticalCoalResponses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CoalShortageResponses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "");
        }
    }
}
