using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PtixiakiReservations.Migrations
{
    /// <inheritdoc />
    public partial class VenueLayouts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LayoutId",
                table: "SubArea",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LayoutId",
                table: "Event",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Layout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    VenueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Layout_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubArea_LayoutId",
                table: "SubArea",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_LayoutId",
                table: "Event",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Layout_VenueId",
                table: "Layout",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Layout_LayoutId",
                table: "Event",
                column: "LayoutId",
                principalTable: "Layout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubArea_Layout_LayoutId",
                table: "SubArea",
                column: "LayoutId",
                principalTable: "Layout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Layout_LayoutId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_SubArea_Layout_LayoutId",
                table: "SubArea");

            migrationBuilder.DropTable(
                name: "Layout");

            migrationBuilder.DropIndex(
                name: "IX_SubArea_LayoutId",
                table: "SubArea");

            migrationBuilder.DropIndex(
                name: "IX_Event_LayoutId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "LayoutId",
                table: "SubArea");

            migrationBuilder.DropColumn(
                name: "LayoutId",
                table: "Event");
        }
    }
}
