using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PtixiakiReservations.Migrations
{
    /// <inheritdoc />
    public partial class AddNonLinearEventDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attended",
                table: "Reservation",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPastReservation",
                table: "Reservation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Reservation",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Reservation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Day",
                table: "Date",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attended",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "IsPastReservation",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Date");
        }
    }
}
