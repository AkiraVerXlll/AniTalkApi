using AniTalkApi.DataLayer.Models;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.DataLayer.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


public class AppDbContext : DbContext
{
    private readonly string _connectionString;
    public AppDbContext(IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionStringName = environment.IsDevelopment() ? "Development" : "Production";
        _connectionString = configuration
            .GetConnectionString(connectionStringName) 
                           ?? throw new ArgumentNullException
                               ($"Connection string \"{connectionStringName}\" is empty");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region ParsingEnums

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<EnumToStringConverter<UserRoles>>();

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

        #region PrimaryKeyConfig

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

        #endregion
    }

    public DbSet<PersonalInformation> PersonalInformation { get; set; }

}
