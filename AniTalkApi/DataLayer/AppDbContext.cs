﻿using AniTalkApi.DataLayer.Models;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.DataLayer.Models.ManyToMany;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Image = AniTalkApi.DataLayer.Models.Image;

namespace AniTalkApi.DataLayer;

public class AppDbContext : IdentityDbContext<User>
{
    private readonly string _connectionString;

    public AppDbContext(IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionStringName = environment.IsDevelopment() ? "Development" : "Production";
        _connectionString = configuration
                                .GetConnectionString(connectionStringName) 
                            ?? throw new ArgumentNullException
                                ($"Invalid onnection string \"{connectionStringName}\"");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region ValueConvertors

        modelBuilder.Entity<User>()
            .Property(u => u.Status)
            .HasConversion<EnumToStringConverter<UserStatus>>();

        modelBuilder.Entity<Title>()
            .Property(t => t.TitleStatus)
            .HasConversion<EnumToStringConverter<TitleStatus>>();

        modelBuilder.Entity<TitleAuthors>()
            .Property(ta => ta.AuthorType)
            .HasConversion<EnumToStringConverter<AuthorType>>();

        modelBuilder.Entity<Relationships>()
            .Property(r => r.RelationshipsStatus)
            .HasConversion<EnumToStringConverter<RelationshipsStatus>>();

        #endregion

        #region PrimaryKeys

        modelBuilder.Entity<TagsInTitle>()
            .HasKey(tt => new { tt.TagId, tt.TitleId });

        modelBuilder.Entity<GenresInTitle>()
            .HasKey(gt => new { gt.GenreId, gt.TitleId });

        modelBuilder.Entity<TitleAuthors>()
            .HasKey(ta => new { ta.AuthorId, ta.TitleId });

        modelBuilder.Entity<FavoriteTitles>()
            .HasKey(ft => new { ft.TitleId, ft.UserId });

        modelBuilder.Entity<Relationships>()
            .HasKey(r => new { r.MainUserId, r.RelationshipsWithUserId });

        modelBuilder.Entity<Review>()
            .HasKey(r => new { r.TitleId, r.UserId });

        modelBuilder.Entity<ImagesInReview>()
            .HasKey(it => new { it.ImageId, it.ReviewId });

        modelBuilder.Entity<UsersInDialog>()
            .HasKey(ud => new { ud.DialogId, ud.UserId });

        modelBuilder.Entity<TitleTypes>()
            .HasKey(t => new { t.TitleId, t.TitleTypeId });

        #endregion

        #region UniqueFields

        modelBuilder.Entity<Author>()
            .HasIndex(a => a.PersonalInformationId)
            .IsUnique();

        modelBuilder.Entity<Title>()
            .HasIndex(t => t.Name)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.PersonalInformationId)
            .IsUnique();

        modelBuilder.Entity<Tag>()
            .HasIndex(t => t.Name)
            .IsUnique();

        modelBuilder.Entity<Genre>()
            .HasIndex(g => g.Name)
            .IsUnique();

        modelBuilder.Entity<Forum>()
            .HasIndex(f => f.DialogId)
            .IsUnique();

        modelBuilder.Entity<Image>()
            .HasIndex(i => i.Url)
            .IsUnique();

        #endregion

        #region Indexes

        modelBuilder.Entity<Relationships>()
            .HasIndex(r => r.RelationshipsStatus);

        modelBuilder.Entity<PersonalInformation>()
            .HasIndex(pi => pi.Country);

        modelBuilder.Entity<PersonalInformation>()
            .HasIndex(pi => pi.City);

        modelBuilder.Entity<PersonalInformation>()
            .HasIndex(pi => pi.Age);

        modelBuilder.Entity<Review>()
            .HasIndex(r => r.StarsCount);

        modelBuilder.Entity<Message>()
            .HasIndex(m => m.SendingTime);

        #endregion

        #region ForeignKeys

        modelBuilder.Entity<User>()
            .HasMany(u => u.RelationshipsAsMainUser)
            .WithOne(r => r.MainUser)
            .HasForeignKey(r => r.MainUserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.RelationshipsAsSubjectUser)
            .WithOne(r => r.RelationshipsWithUser)
            .HasForeignKey(r => r.RelationshipsWithUserId);

        #endregion

        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasNoKey();

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasNoKey();

        modelBuilder.Entity<IdentityUserToken<string>>()
            .HasNoKey();


    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Dialog> Dialogs { get; set; }

    public DbSet<Forum> Forums { get; set; }

    public DbSet<Genre> Genres { get; set; }
    
    public DbSet<Models.Image> Images { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<PersonalInformation> PersonalInformation { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Title> Titles { get; set; }

    public DbSet<TitleType> TitleType { get; set; }
}