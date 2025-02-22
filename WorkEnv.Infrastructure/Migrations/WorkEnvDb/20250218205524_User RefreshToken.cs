using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkEnv.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_maxNumberOfParticipants",
                table: "Activity",
                newName: "MaxNumberOfParticipants");

            migrationBuilder.AddColumn<string>(
                name: "_refreshToken",
                table: "Users",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_refreshToken",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "MaxNumberOfParticipants",
                table: "Activity",
                newName: "_maxNumberOfParticipants");
        }
    }
}
