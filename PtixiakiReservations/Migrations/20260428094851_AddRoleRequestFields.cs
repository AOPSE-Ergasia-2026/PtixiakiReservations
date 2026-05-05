using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PtixiakiReservations.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleRequestFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EventManagerRequestDate",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventManagerRequestReason",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventManagerRequestStatus",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasRequestedEventManagerRole",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRequestedSuperOrganizerRole",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuperOrganizerRequestDate",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuperOrganizerRequestReason",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuperOrganizerRequestStatus",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventManagerRequestDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EventManagerRequestReason",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EventManagerRequestStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasRequestedEventManagerRole",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasRequestedSuperOrganizerRole",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuperOrganizerRequestDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuperOrganizerRequestReason",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuperOrganizerRequestStatus",
                table: "AspNetUsers");
        }
    }
}
