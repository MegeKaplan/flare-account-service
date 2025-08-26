using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flare.AccountService.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayNameToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Accounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Accounts");
        }
    }
}
