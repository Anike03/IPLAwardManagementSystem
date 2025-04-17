using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPLAwardManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddYearAndIsWinnerToPlayerAward : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VotesReceived",
                table: "PlayerAwards",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "NominationDate",
                table: "PlayerAwards",
                newName: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "PlayerAwards",
                newName: "NominationDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PlayerAwards",
                newName: "VotesReceived");
        }
    }
}
