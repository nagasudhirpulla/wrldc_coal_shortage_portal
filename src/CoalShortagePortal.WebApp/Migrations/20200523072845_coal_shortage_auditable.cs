using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoalShortagePortal.WebApp.Migrations
{
    public partial class coal_shortage_auditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CoalShortageResponses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "CoalShortageResponses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "CoalShortageResponses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "CoalShortageResponses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoalShortageResponses_CreatedById",
                table: "CoalShortageResponses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CoalShortageResponses_LastModifiedById",
                table: "CoalShortageResponses",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CoalShortageResponses_AspNetUsers_CreatedById",
                table: "CoalShortageResponses",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoalShortageResponses_AspNetUsers_LastModifiedById",
                table: "CoalShortageResponses",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoalShortageResponses_AspNetUsers_CreatedById",
                table: "CoalShortageResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CoalShortageResponses_AspNetUsers_LastModifiedById",
                table: "CoalShortageResponses");

            migrationBuilder.DropIndex(
                name: "IX_CoalShortageResponses_CreatedById",
                table: "CoalShortageResponses");

            migrationBuilder.DropIndex(
                name: "IX_CoalShortageResponses_LastModifiedById",
                table: "CoalShortageResponses");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CoalShortageResponses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CoalShortageResponses");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "CoalShortageResponses");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "CoalShortageResponses");
        }
    }
}
