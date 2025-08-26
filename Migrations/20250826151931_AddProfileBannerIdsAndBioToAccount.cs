using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flare.AccountService.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileBannerIdsAndBioToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerImageId",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageId",
                table: "Accounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImageId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "Accounts");
        }
    }
}
