using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiWorksAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVisitorsAddDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.AddColumn<string>(
                name: "Disruptions",
                table: "Attractions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Misc",
                table: "Attractions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Shows",
                table: "Attractions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "VisitorsInQueue",
                table: "Attractions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VisitorsOnRide",
                table: "Attractions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disruptions",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "Misc",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "Shows",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "VisitorsInQueue",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "VisitorsOnRide",
                table: "Attractions");

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttractionInQueueId = table.Column<int>(type: "int", nullable: true),
                    AttractionOnRideId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitors_Attractions_AttractionInQueueId",
                        column: x => x.AttractionInQueueId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visitors_Attractions_AttractionOnRideId",
                        column: x => x.AttractionOnRideId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_AttractionInQueueId",
                table: "Visitors",
                column: "AttractionInQueueId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_AttractionOnRideId",
                table: "Visitors",
                column: "AttractionOnRideId");
        }
    }
}
