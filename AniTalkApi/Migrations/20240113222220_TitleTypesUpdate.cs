using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniTalkApi.Migrations
{
    /// <inheritdoc />
    public partial class TitleTypesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecialDescription",
                table: "TitleTypes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialDescription",
                table: "TitleTypes");
        }
    }
}
