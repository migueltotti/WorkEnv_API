using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkEnv.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WorkSpacePrivatePropertyFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_masterCode",
                table: "WorkSpace",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_masterCode",
                table: "WorkSpace");
        }
    }
}
