using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AniTalkApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorType",
                table: "TitleAuthors");

            migrationBuilder.AddColumn<string>(
                name: "NormalizeName",
                table: "TitleType",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizeName",
                table: "Titles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AuthorTypeId",
                table: "TitleAuthors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NormalizeName",
                table: "Tags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizeName",
                table: "Genres",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AuthorType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalizeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitleType_Name",
                table: "TitleType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitleType_NormalizeName",
                table: "TitleType",
                column: "NormalizeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_NormalizeName",
                table: "Titles",
                column: "NormalizeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitleAuthors_AuthorTypeId",
                table: "TitleAuthors",
                column: "AuthorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_NormalizeName",
                table: "Tags",
                column: "NormalizeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_NormalizeName",
                table: "Genres",
                column: "NormalizeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorType_Name",
                table: "AuthorType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorType_NormalizeName",
                table: "AuthorType",
                column: "NormalizeName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleAuthors_AuthorType_AuthorTypeId",
                table: "TitleAuthors",
                column: "AuthorTypeId",
                principalTable: "AuthorType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitleAuthors_AuthorType_AuthorTypeId",
                table: "TitleAuthors");

            migrationBuilder.DropTable(
                name: "AuthorType");

            migrationBuilder.DropIndex(
                name: "IX_TitleType_Name",
                table: "TitleType");

            migrationBuilder.DropIndex(
                name: "IX_TitleType_NormalizeName",
                table: "TitleType");

            migrationBuilder.DropIndex(
                name: "IX_Titles_NormalizeName",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_TitleAuthors_AuthorTypeId",
                table: "TitleAuthors");

            migrationBuilder.DropIndex(
                name: "IX_Tags_NormalizeName",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Genres_NormalizeName",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "NormalizeName",
                table: "TitleType");

            migrationBuilder.DropColumn(
                name: "NormalizeName",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "AuthorTypeId",
                table: "TitleAuthors");

            migrationBuilder.DropColumn(
                name: "NormalizeName",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "NormalizeName",
                table: "Genres");

            migrationBuilder.AddColumn<string>(
                name: "AuthorType",
                table: "TitleAuthors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
