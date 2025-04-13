using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPLAwardManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixVoterIdIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoterId",
                table: "Voters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VoterId",
                table: "Voters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
