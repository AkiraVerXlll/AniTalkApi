﻿// <auto-generated />
using System;
using AniTalkApi.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AniTalkApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240113222220_TitleTypesUpdate")]
    partial class TitleTypesUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PersonalInformationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInformationId")
                        .IsUnique();

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Dialog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AvatarId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Forum", b =>
                {
                    b.Property<int>("DialogId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFrozen")
                        .HasColumnType("boolean");

                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DialogId");

                    b.HasIndex("DialogId")
                        .IsUnique();

                    b.HasIndex("TitleId");

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.FavoriteTitles", b =>
                {
                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int?>("Order")
                        .HasColumnType("integer");

                    b.HasKey("TitleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteTitles");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.GenresInTitle", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("GenreId", "TitleId");

                    b.HasIndex("TitleId");

                    b.ToTable("GenresInTitle");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.ImagesInReview", b =>
                {
                    b.Property<int>("ImageId")
                        .HasColumnType("integer");

                    b.Property<int>("ReviewId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int?>("ReviewTitleId")
                        .HasColumnType("integer");

                    b.Property<string>("ReviewUserId")
                        .HasColumnType("text");

                    b.HasKey("ImageId", "ReviewId");

                    b.HasIndex("ReviewTitleId", "ReviewUserId");

                    b.ToTable("ImagesInReview");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.TagsInTitle", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("TagId", "TitleId");

                    b.HasIndex("TitleId");

                    b.ToTable("TagsInTitle");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.TitleAuthors", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<string>("AuthorType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AuthorId", "TitleId");

                    b.HasIndex("TitleId");

                    b.ToTable("TitleAuthors");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.TitleTypes", b =>
                {
                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<int>("TitleTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("SpecialDescription")
                        .HasColumnType("text");

                    b.HasKey("TitleId", "TitleTypeId");

                    b.HasIndex("TitleTypeId");

                    b.ToTable("TitleTypes");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.UsersInDialog", b =>
                {
                    b.Property<int>("DialogId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("DialogId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersInDialog");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DialogId")
                        .HasColumnType("integer");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SendingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.HasIndex("SenderId");

                    b.HasIndex("SendingTime");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.PersonalInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AboutYourself")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<int?>("AvatarId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("City")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Country")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Surname")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("Age");

                    b.HasIndex("AvatarId");

                    b.HasIndex("City");

                    b.HasIndex("Country");

                    b.ToTable("PersonalInformation");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Relationships", b =>
                {
                    b.Property<string>("MainUserId")
                        .HasColumnType("text");

                    b.Property<string>("RelationshipsWithUserId")
                        .HasColumnType("text");

                    b.Property<string>("RelationshipsStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MainUserId", "RelationshipsWithUserId");

                    b.HasIndex("RelationshipsStatus");

                    b.HasIndex("RelationshipsWithUserId");

                    b.ToTable("Relationships");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Review", b =>
                {
                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("StarsCount")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("TitleId", "UserId");

                    b.HasIndex("StarsCount");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Title", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CoverId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("TitleStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CoverId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.TitleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TitleType");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<int>("PersonalInformationId")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInformationId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.HasKey("ProviderKey", "LoginProvider", "UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("RoleId", "UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Author", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.PersonalInformation", "PersonalInformation")
                        .WithOne("Author")
                        .HasForeignKey("AniTalkApi.DataLayer.Models.Author", "PersonalInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Dialog", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Image", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Forum", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Dialog", "Dialog")
                        .WithOne("Forum")
                        .HasForeignKey("AniTalkApi.DataLayer.Models.Forum", "DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.Title", "Title")
                        .WithMany("Forums")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dialog");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.FavoriteTitles", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Title", "Title")
                        .WithMany("FavoriteTitlesOf")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.User", "User")
                        .WithMany("FavoriteTitles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Title");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.GenresInTitle", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Genre", "Genre")
                        .WithMany("GenresInTitle")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.Title", "Title")
                        .WithMany("Genres")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.ImagesInReview", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.Review", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewTitleId", "ReviewUserId");

                    b.Navigation("Image");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.TagsInTitle", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Tag", "Tag")
                        .WithMany("TagsInTitle")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.Title", "Title")
                        .WithMany("Tags")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.TitleAuthors", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Author", "Author")
                        .WithMany("Works")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.Title", "Title")
                        .WithMany("TitleAuthors")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.TitleTypes", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Title", "Title")
                        .WithMany("TitleTypes")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.TitleType", "TitleType")
                        .WithMany("TitleTypes")
                        .HasForeignKey("TitleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Title");

                    b.Navigation("TitleType");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.ManyToMany.UsersInDialog", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Dialog", "Dialog")
                        .WithMany("UsersInDialogs")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.User", "User")
                        .WithMany("Dialogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dialog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Message", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Dialog", "Dialog")
                        .WithMany("Messages")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dialog");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.PersonalInformation", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Image", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Relationships", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.User", "MainUser")
                        .WithMany("RelationshipsAsMainUser")
                        .HasForeignKey("MainUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.User", "RelationshipsWithUser")
                        .WithMany("RelationshipsAsSubjectUser")
                        .HasForeignKey("RelationshipsWithUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainUser");

                    b.Navigation("RelationshipsWithUser");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Review", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Title", "Title")
                        .WithMany("Reviews")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AniTalkApi.DataLayer.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Title");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Title", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.Image", "Cover")
                        .WithMany()
                        .HasForeignKey("CoverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cover");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.User", b =>
                {
                    b.HasOne("AniTalkApi.DataLayer.Models.PersonalInformation", "PersonalInformation")
                        .WithOne("User")
                        .HasForeignKey("AniTalkApi.DataLayer.Models.User", "PersonalInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalInformation");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Author", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Dialog", b =>
                {
                    b.Navigation("Forum");

                    b.Navigation("Messages");

                    b.Navigation("UsersInDialogs");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Genre", b =>
                {
                    b.Navigation("GenresInTitle");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.PersonalInformation", b =>
                {
                    b.Navigation("Author");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Tag", b =>
                {
                    b.Navigation("TagsInTitle");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.Title", b =>
                {
                    b.Navigation("FavoriteTitlesOf");

                    b.Navigation("Forums");

                    b.Navigation("Genres");

                    b.Navigation("Reviews");

                    b.Navigation("Tags");

                    b.Navigation("TitleAuthors");

                    b.Navigation("TitleTypes");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.TitleType", b =>
                {
                    b.Navigation("TitleTypes");
                });

            modelBuilder.Entity("AniTalkApi.DataLayer.Models.User", b =>
                {
                    b.Navigation("Dialogs");

                    b.Navigation("FavoriteTitles");

                    b.Navigation("RelationshipsAsMainUser");

                    b.Navigation("RelationshipsAsSubjectUser");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
