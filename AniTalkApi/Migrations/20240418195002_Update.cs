using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AniTalkApi.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Images_AvatarId",
                table: "Dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformation_Images_AvatarId",
                table: "PersonalInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Images_CoverId",
                table: "Titles");

            migrationBuilder.DropTable(
                name: "ImagesInReview");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Titles_CoverId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInformation_AvatarId",
                table: "PersonalInformation");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_AvatarId",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "PersonalInformation");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Dialogs");

            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Titles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reviews",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "ImageUrls",
                table: "Reviews",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "PersonalInformation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string[]>(
                name: "ImageUrls",
                table: "Messages",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Dialogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "PersonalInformation");

            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Dialogs");

            migrationBuilder.AddColumn<int>(
                name: "CoverId",
                table: "Titles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "PersonalInformation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "Dialogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImagesInReview",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "integer", nullable: false),
                    ReviewId = table.Column<int>(type: "integer", nullable: false),
                    ReviewTitleId = table.Column<int>(type: "integer", nullable: true),
                    ReviewUserId = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesInReview", x => new { x.ImageId, x.ReviewId });
                    table.ForeignKey(
                        name: "FK_ImagesInReview_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagesInReview_Reviews_ReviewTitleId_ReviewUserId",
                        columns: x => new { x.ReviewTitleId, x.ReviewUserId },
                        principalTable: "Reviews",
                        principalColumns: new[] { "TitleId", "UserId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Titles_CoverId",
                table: "Titles",
                column: "CoverId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformation_AvatarId",
                table: "PersonalInformation",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_AvatarId",
                table: "Dialogs",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Url",
                table: "Images",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagesInReview_ReviewTitleId_ReviewUserId",
                table: "ImagesInReview",
                columns: new[] { "ReviewTitleId", "ReviewUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Images_AvatarId",
                table: "Dialogs",
                column: "AvatarId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformation_Images_AvatarId",
                table: "PersonalInformation",
                column: "AvatarId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Images_CoverId",
                table: "Titles",
                column: "CoverId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
