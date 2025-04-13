using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPLAwardManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVoteFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Voters",
                newName: "VoterId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Awards",
                newName: "AwardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VoterId",
                table: "Voters",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AwardId",
                table: "Awards",
                newName: "Id");
        }
    }
}
