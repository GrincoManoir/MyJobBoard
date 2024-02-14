using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyJobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOpportunityetcie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Websites",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "string[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SocialNetworks",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "string[]",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Websites",
                table: "Companies",
                type: "string[]",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SocialNetworks",
                table: "Companies",
                type: "string[]",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
