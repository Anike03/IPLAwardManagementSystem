using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPLAwardManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCityToVenue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Venues",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Venues",
                newName: "Location");
        }
    }
}
