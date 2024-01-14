using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniTalkApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizeNameToTitleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizeName",
                table: "TitleType",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizeName",
                table: "TitleType");
        }
    }
}
