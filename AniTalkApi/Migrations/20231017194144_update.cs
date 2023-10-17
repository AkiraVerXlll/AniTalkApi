using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniTalkApi.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_PersonalInformation_PersonalInformationId",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialog_Image_AvatarId",
                table: "Dialog");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteTitles_Title_TitleId",
                table: "FavoriteTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteTitles_User_UserId",
                table: "FavoriteTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_Forum_Dialog_DialogId",
                table: "Forum");

            migrationBuilder.DropForeignKey(
                name: "FK_Forum_Title_TitleId",
                table: "Forum");

            migrationBuilder.DropForeignKey(
                name: "FK_GenresInTitle_Genre_GenreId",
                table: "GenresInTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_GenresInTitle_Title_TitleId",
                table: "GenresInTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesInReview_Image_ImageId",
                table: "ImagesInReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesInReview_Review_ReviewTitleId_ReviewUserID",
                table: "ImagesInReview");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Dialog_DialogId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformation_Image_AvatarId",
                table: "PersonalInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_User_MainUserId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_User_RelationshipsWithUserId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Title_TitleId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_UserID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsInTitle_Tag_TagId",
                table: "TagsInTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsInTitle_Title_TitleId",
                table: "TagsInTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_Title_Image_CoverId",
                table: "Title");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleAuthors_Author_AuthorId",
                table: "TitleAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleAuthors_Title_TitleId",
                table: "TitleAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleTypes_Title_TitleId",
                table: "TitleTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_User_PersonalInformation_PersonalInformationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInDialog_Dialog_DialogId",
                table: "UsersInDialog");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInDialog_User_UserId",
                table: "UsersInDialog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Title",
                table: "Title");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forum",
                table: "Forum");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dialog",
                table: "Dialog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Title",
                newName: "Titles");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "Forum",
                newName: "Forums");

            migrationBuilder.RenameTable(
                name: "Dialog",
                newName: "Dialogs");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_User_PersonalInformationId",
                table: "Users",
                newName: "IX_Users_PersonalInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Nickname",
                table: "Users",
                newName: "IX_Users_Nickname");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Title_Name",
                table: "Titles",
                newName: "IX_Titles_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Title_CoverId",
                table: "Titles",
                newName: "IX_Titles_CoverId");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_Name",
                table: "Tags",
                newName: "IX_Tags_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserID",
                table: "Reviews",
                newName: "IX_Reviews_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Review_StarsCount",
                table: "Reviews",
                newName: "IX_Reviews_StarsCount");

            migrationBuilder.RenameIndex(
                name: "IX_Message_SendingTime",
                table: "Messages",
                newName: "IX_Messages_SendingTime");

            migrationBuilder.RenameIndex(
                name: "IX_Message_SenderId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_DialogId",
                table: "Messages",
                newName: "IX_Messages_DialogId");

            migrationBuilder.RenameIndex(
                name: "IX_Genre_Name",
                table: "Genres",
                newName: "IX_Genres_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Forum_TitleId",
                table: "Forums",
                newName: "IX_Forums_TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Forum_DialogId",
                table: "Forums",
                newName: "IX_Forums_DialogId");

            migrationBuilder.RenameIndex(
                name: "IX_Dialog_AvatarId",
                table: "Dialogs",
                newName: "IX_Dialogs_AvatarId");

            migrationBuilder.RenameIndex(
                name: "IX_Author_PersonalInformationId",
                table: "Authors",
                newName: "IX_Authors_PersonalInformationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Titles",
                table: "Titles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                columns: new[] { "TitleId", "UserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forums",
                table: "Forums",
                column: "DialogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dialogs",
                table: "Dialogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Url",
                table: "Images",
                column: "Url",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_PersonalInformation_PersonalInformationId",
                table: "Authors",
                column: "PersonalInformationId",
                principalTable: "PersonalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Images_AvatarId",
                table: "Dialogs",
                column: "AvatarId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteTitles_Titles_TitleId",
                table: "FavoriteTitles",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteTitles_Users_UserId",
                table: "FavoriteTitles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forums_Dialogs_DialogId",
                table: "Forums",
                column: "DialogId",
                principalTable: "Dialogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forums_Titles_TitleId",
                table: "Forums",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenresInTitle_Genres_GenreId",
                table: "GenresInTitle",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenresInTitle_Titles_TitleId",
                table: "GenresInTitle",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesInReview_Images_ImageId",
                table: "ImagesInReview",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesInReview_Reviews_ReviewTitleId_ReviewUserID",
                table: "ImagesInReview",
                columns: new[] { "ReviewTitleId", "ReviewUserID" },
                principalTable: "Reviews",
                principalColumns: new[] { "TitleId", "UserID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Dialogs_DialogId",
                table: "Messages",
                column: "DialogId",
                principalTable: "Dialogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
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
                name: "FK_Relationships_Users_MainUserId",
                table: "Relationships",
                column: "MainUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Users_RelationshipsWithUserId",
                table: "Relationships",
                column: "RelationshipsWithUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Titles_TitleId",
                table: "Reviews",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsInTitle_Tags_TagId",
                table: "TagsInTitle",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsInTitle_Titles_TitleId",
                table: "TagsInTitle",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleAuthors_Authors_AuthorId",
                table: "TitleAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleAuthors_Titles_TitleId",
                table: "TitleAuthors",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Images_CoverId",
                table: "Titles",
                column: "CoverId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleTypes_Titles_TitleId",
                table: "TitleTypes",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonalInformation_PersonalInformationId",
                table: "Users",
                column: "PersonalInformationId",
                principalTable: "PersonalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInDialog_Dialogs_DialogId",
                table: "UsersInDialog",
                column: "DialogId",
                principalTable: "Dialogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInDialog_Users_UserId",
                table: "UsersInDialog",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_PersonalInformation_PersonalInformationId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Images_AvatarId",
                table: "Dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteTitles_Titles_TitleId",
                table: "FavoriteTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteTitles_Users_UserId",
                table: "FavoriteTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_Forums_Dialogs_DialogId",
                table: "Forums");

            migrationBuilder.DropForeignKey(
                name: "FK_Forums_Titles_TitleId",
                table: "Forums");

            migrationBuilder.DropForeignKey(
                name: "FK_GenresInTitle_Genres_GenreId",
                table: "GenresInTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_GenresInTitle_Titles_TitleId",
                table: "GenresInTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesInReview_Images_ImageId",
                table: "ImagesInReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesInReview_Reviews_ReviewTitleId_ReviewUserID",
                table: "ImagesInReview");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Dialogs_DialogId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformation_Images_AvatarId",
                table: "PersonalInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_MainUserId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Users_RelationshipsWithUserId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Titles_TitleId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsInTitle_Tags_TagId",
                table: "TagsInTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_TagsInTitle_Titles_TitleId",
                table: "TagsInTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleAuthors_Authors_AuthorId",
                table: "TitleAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleAuthors_Titles_TitleId",
                table: "TitleAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Images_CoverId",
                table: "Titles");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleTypes_Titles_TitleId",
                table: "TitleTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalInformation_PersonalInformationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInDialog_Dialogs_DialogId",
                table: "UsersInDialog");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInDialog_Users_UserId",
                table: "UsersInDialog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Titles",
                table: "Titles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_Url",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forums",
                table: "Forums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dialogs",
                table: "Dialogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Titles",
                newName: "Title");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Forums",
                newName: "Forum");

            migrationBuilder.RenameTable(
                name: "Dialogs",
                newName: "Dialog");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Author");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PersonalInformationId",
                table: "User",
                newName: "IX_User_PersonalInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Nickname",
                table: "User",
                newName: "IX_User_Nickname");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Titles_Name",
                table: "Title",
                newName: "IX_Title_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Titles_CoverId",
                table: "Title",
                newName: "IX_Title_CoverId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_Name",
                table: "Tag",
                newName: "IX_Tag_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserID",
                table: "Review",
                newName: "IX_Review_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_StarsCount",
                table: "Review",
                newName: "IX_Review_StarsCount");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SendingTime",
                table: "Message",
                newName: "IX_Message_SendingTime");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Message",
                newName: "IX_Message_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_DialogId",
                table: "Message",
                newName: "IX_Message_DialogId");

            migrationBuilder.RenameIndex(
                name: "IX_Genres_Name",
                table: "Genre",
                newName: "IX_Genre_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Forums_TitleId",
                table: "Forum",
                newName: "IX_Forum_TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Forums_DialogId",
                table: "Forum",
                newName: "IX_Forum_DialogId");

            migrationBuilder.RenameIndex(
                name: "IX_Dialogs_AvatarId",
                table: "Dialog",
                newName: "IX_Dialog_AvatarId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_PersonalInformationId",
                table: "Author",
                newName: "IX_Author_PersonalInformationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Title",
                table: "Title",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                columns: new[] { "TitleId", "UserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forum",
                table: "Forum",
                column: "DialogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dialog",
                table: "Dialog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_PersonalInformation_PersonalInformationId",
                table: "Author",
                column: "PersonalInformationId",
                principalTable: "PersonalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialog_Image_AvatarId",
                table: "Dialog",
                column: "AvatarId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteTitles_Title_TitleId",
                table: "FavoriteTitles",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteTitles_User_UserId",
                table: "FavoriteTitles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forum_Dialog_DialogId",
                table: "Forum",
                column: "DialogId",
                principalTable: "Dialog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forum_Title_TitleId",
                table: "Forum",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenresInTitle_Genre_GenreId",
                table: "GenresInTitle",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenresInTitle_Title_TitleId",
                table: "GenresInTitle",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesInReview_Image_ImageId",
                table: "ImagesInReview",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesInReview_Review_ReviewTitleId_ReviewUserID",
                table: "ImagesInReview",
                columns: new[] { "ReviewTitleId", "ReviewUserID" },
                principalTable: "Review",
                principalColumns: new[] { "TitleId", "UserID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Dialog_DialogId",
                table: "Message",
                column: "DialogId",
                principalTable: "Dialog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformation_Image_AvatarId",
                table: "PersonalInformation",
                column: "AvatarId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_User_MainUserId",
                table: "Relationships",
                column: "MainUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_User_RelationshipsWithUserId",
                table: "Relationships",
                column: "RelationshipsWithUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Title_TitleId",
                table: "Review",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_UserID",
                table: "Review",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsInTitle_Tag_TagId",
                table: "TagsInTitle",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagsInTitle_Title_TitleId",
                table: "TagsInTitle",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Title_Image_CoverId",
                table: "Title",
                column: "CoverId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleAuthors_Author_AuthorId",
                table: "TitleAuthors",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleAuthors_Title_TitleId",
                table: "TitleAuthors",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleTypes_Title_TitleId",
                table: "TitleTypes",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_PersonalInformation_PersonalInformationId",
                table: "User",
                column: "PersonalInformationId",
                principalTable: "PersonalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInDialog_Dialog_DialogId",
                table: "UsersInDialog",
                column: "DialogId",
                principalTable: "Dialog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInDialog_User_UserId",
                table: "UsersInDialog",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
