using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PtixiakiReservations.Migrations
{
    /// <inheritdoc />
    public partial class addedUrlsForVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SocialMediaUrl",
                table: "Venue",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VenueUrl",
                table: "Venue",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialMediaUrl",
                table: "Venue");

            migrationBuilder.DropColumn(
                name: "VenueUrl",
                table: "Venue");
        }
    }
}
