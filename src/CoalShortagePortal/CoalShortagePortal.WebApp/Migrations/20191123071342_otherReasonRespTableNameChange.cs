using Microsoft.EntityFrameworkCore.Migrations;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class otherReasonRespTableNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherReasonsResponse",
                table: "OtherReasonsResponse");

            migrationBuilder.RenameTable(
                name: "OtherReasonsResponse",
                newName: "OtherReasonsResponses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherReasonsResponses",
                table: "OtherReasonsResponses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherReasonsResponses",
                table: "OtherReasonsResponses");

            migrationBuilder.RenameTable(
                name: "OtherReasonsResponses",
                newName: "OtherReasonsResponse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherReasonsResponse",
                table: "OtherReasonsResponse",
                column: "Id");
        }
    }
}
