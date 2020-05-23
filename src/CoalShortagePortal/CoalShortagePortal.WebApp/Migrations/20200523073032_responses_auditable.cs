using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class responses_auditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "OtherReasonsResponses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "OtherReasonsResponses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "OtherReasonsResponses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "OtherReasonsResponses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CriticalCoalResponses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "CriticalCoalResponses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "CriticalCoalResponses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "CriticalCoalResponses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtherReasonsResponses_CreatedById",
                table: "OtherReasonsResponses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OtherReasonsResponses_LastModifiedById",
                table: "OtherReasonsResponses",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalCoalResponses_CreatedById",
                table: "CriticalCoalResponses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CriticalCoalResponses_LastModifiedById",
                table: "CriticalCoalResponses",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalCoalResponses_AspNetUsers_CreatedById",
                table: "CriticalCoalResponses",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CriticalCoalResponses_AspNetUsers_LastModifiedById",
                table: "CriticalCoalResponses",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherReasonsResponses_AspNetUsers_CreatedById",
                table: "OtherReasonsResponses",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherReasonsResponses_AspNetUsers_LastModifiedById",
                table: "OtherReasonsResponses",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriticalCoalResponses_AspNetUsers_CreatedById",
                table: "CriticalCoalResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CriticalCoalResponses_AspNetUsers_LastModifiedById",
                table: "CriticalCoalResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherReasonsResponses_AspNetUsers_CreatedById",
                table: "OtherReasonsResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherReasonsResponses_AspNetUsers_LastModifiedById",
                table: "OtherReasonsResponses");

            migrationBuilder.DropIndex(
                name: "IX_OtherReasonsResponses_CreatedById",
                table: "OtherReasonsResponses");

            migrationBuilder.DropIndex(
                name: "IX_OtherReasonsResponses_LastModifiedById",
                table: "OtherReasonsResponses");

            migrationBuilder.DropIndex(
                name: "IX_CriticalCoalResponses_CreatedById",
                table: "CriticalCoalResponses");

            migrationBuilder.DropIndex(
                name: "IX_CriticalCoalResponses_LastModifiedById",
                table: "CriticalCoalResponses");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "OtherReasonsResponses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "OtherReasonsResponses");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "OtherReasonsResponses");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "OtherReasonsResponses");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CriticalCoalResponses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CriticalCoalResponses");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "CriticalCoalResponses");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "CriticalCoalResponses");
        }
    }
}
