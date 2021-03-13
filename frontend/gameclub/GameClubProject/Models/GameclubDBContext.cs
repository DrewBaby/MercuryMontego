using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GameClubProject
{
    public partial class GameclubDBContext : DbContext
    {
        public GameclubDBContext()
        {
        }

        public GameclubDBContext(DbContextOptions<GameclubDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlternativeGameName> AlternativeGameNames { get; set; }
        public virtual DbSet<MembershipStatus> MembershipStatuses { get; set; }
        public virtual DbSet<PersonalUserTrackedGame> PersonalUserTrackedGames { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserGamerTag> UserGamerTags { get; set; }
        public virtual DbSet<VideoGameCharacter> VideoGameCharacters { get; set; }
        public virtual DbSet<VideoGameCharacterAlias> VideoGameCharacterAliases { get; set; }
        public virtual DbSet<VideoGameGameMode> VideoGameGameModes { get; set; }
        public virtual DbSet<VideoGameGenre> VideoGameGenres { get; set; }
        public virtual DbSet<VideoGameInvolvedCompany> VideoGameInvolvedCompanies { get; set; }
        public virtual DbSet<VideoGameKeyword> VideoGameKeywords { get; set; }
        public virtual DbSet<VideoGameMain> VideoGameMains { get; set; }
        public virtual DbSet<VideoGameMultiplayerMode> VideoGameMultiplayerModes { get; set; }
        public virtual DbSet<VideoGamePlatform> VideoGamePlatforms { get; set; }
        public virtual DbSet<VideoGameTheme> VideoGameThemes { get; set; }
        public virtual DbSet<VideoGameUserContent> VideoGameUserContents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GameclubDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            
                modelBuilder.Entity<AlternativeGameName>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__alternat__4866A5D78439C31F");

                entity.ToTable("alternativeGameNames");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.AltName)
                    .HasMaxLength(255)
                    .HasColumnName("altName");

                entity.Property(e => e.CheckSum)
                    .HasMaxLength(255)
                    .HasColumnName("checkSum");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.GameTitle)
                    .HasMaxLength(255)
                    .HasColumnName("gameTitle");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.AlternativeGameNames)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__alternati__gameI__49C3F6B7");
            });

            modelBuilder.Entity<MembershipStatus>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__membersh__4866A5D736439232");

                entity.ToTable("membershipStatus");

                entity.HasIndex(e => e.MembershipStatusId, "UQ__membersh__19137F51E3295E81")
                    .IsUnique();

                entity.HasIndex(e => e.Description, "UQ__membersh__489B0D97AC3B0CD4")
                    .IsUnique();

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.MembershipStatusId).HasColumnName("membershipStatusID");
            });

            modelBuilder.Entity<PersonalUserTrackedGame>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__personal__4866A5D7D181A328");

                entity.ToTable("personalUserTrackedGames");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.PersonalUserTrackedGames)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__personalU__gameI__48CFD27E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PersonalUserTrackedGames)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__personalU__userI__47DBAE45");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__userAcco__4866A5D719596C8F");

                entity.ToTable("userAccount");

                entity.HasIndex(e => e.UserName, "UQ__userAcco__66DCF95C67CF0D5C")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__userAcco__AB6E6164A5A73C2C")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "UQ__userAcco__CB9A1CDE1FFB70EC")
                    .IsUnique();

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.AddressCity)
                    .HasMaxLength(255)
                    .HasColumnName("addressCity");

                entity.Property(e => e.AddressState)
                    .HasMaxLength(255)
                    .HasColumnName("addressState");

                entity.Property(e => e.AddressStreet)
                    .HasMaxLength(255)
                    .HasColumnName("addressStreet");

                entity.Property(e => e.AddressStreet2)
                    .HasMaxLength(255)
                    .HasColumnName("addressStreet2");

                entity.Property(e => e.AddressZipCode)
                    .HasMaxLength(255)
                    .HasColumnName("addressZipCode");

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("lastName");

                entity.Property(e => e.MembershipStatusId).HasColumnName("membershipStatusID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userName");

                entity.HasOne(d => d.MembershipStatus)
                    .WithMany(p => p.UserAccounts)
                    .HasPrincipalKey(p => p.MembershipStatusId)
                    .HasForeignKey(d => d.MembershipStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__userAccou__membe__45F365D3");
            });

            modelBuilder.Entity<UserGamerTag>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__userGame__4866A5D7C4862834");

                entity.ToTable("userGamerTags");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.GamerTag)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gamerTag");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGamerTags)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__userGamer__userI__46E78A0C");
            });

            modelBuilder.Entity<VideoGameCharacter>(entity =>
            {
                entity.HasKey(e => e.CharacterId)
                    .HasName("PK__videoGam__ADF9199F9CDC0201");

                entity.ToTable("videoGameCharacters");

                entity.Property(e => e.CharacterId)
                    .HasMaxLength(255)
                    .HasColumnName("characterID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .HasColumnName("countryName");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PKey)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("pKey");

                entity.Property(e => e.Species).HasColumnName("species");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameCharacters)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__4D94879B");
            });

            modelBuilder.Entity<VideoGameCharacterAlias>(entity =>
            {
                entity.HasKey(e => e.Pkey)
                    .HasName("PK__videoGam__40A62DB994424331");

                entity.ToTable("videoGameCharacterAlias");

                entity.Property(e => e.Pkey).HasColumnName("pkey");

                entity.Property(e => e.AliasName)
                    .HasMaxLength(255)
                    .HasColumnName("aliasName");

                entity.Property(e => e.CharacterId)
                    .HasMaxLength(255)
                    .HasColumnName("characterID");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.VideoGameCharacterAliases)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK__videoGame__chara__4E88ABD4");
            });

            modelBuilder.Entity<VideoGameGameMode>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7A59537A2");

                entity.ToTable("videoGameGameModes");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.GameModeName)
                    .HasMaxLength(255)
                    .HasColumnName("gameModeName");

                entity.Property(e => e.GameModeUrl)
                    .HasMaxLength(255)
                    .HasColumnName("gameModeURL");

                entity.Property(e => e.Slug)
                    .HasMaxLength(255)
                    .HasColumnName("slug");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameGameModes)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__4AB81AF0");
            });

            modelBuilder.Entity<VideoGameGenre>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D796F744FF");

                entity.ToTable("videoGameGenres");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.Checksum)
                    .HasMaxLength(255)
                    .HasColumnName("checksum");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(255)
                    .HasColumnName("genreName");

                entity.Property(e => e.Slug)
                    .HasMaxLength(255)
                    .HasColumnName("slug");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameGenres)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__5070F446");
            });

            modelBuilder.Entity<VideoGameInvolvedCompany>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7CDB1A089");

                entity.ToTable("videoGameInvolvedCompanies");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("companyName");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.ParentCompany)
                    .HasMaxLength(255)
                    .HasColumnName("parentCompany");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameInvolvedCompanies)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__5441852A");
            });

            modelBuilder.Entity<VideoGameKeyword>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D74817F98D");

                entity.ToTable("videoGameKeywords");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.Keyword)
                    .HasMaxLength(255)
                    .HasColumnName("keyword");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameKeywords)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__534D60F1");
            });

            modelBuilder.Entity<VideoGameMain>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D71B8AF9C7");

                entity.ToTable("videoGameMain");

                entity.HasIndex(e => e.GameId, "UQ__videoGam__DA90B4B3D5EC9B21")
                    .IsUnique();

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.AgeRatingCategory).HasColumnName("ageRatingCategory");

                entity.Property(e => e.AgeRatingSynopsis)
                    .HasMaxLength(255)
                    .HasColumnName("ageRatingSynopsis");

                entity.Property(e => e.AgeRatingTitle).HasColumnName("ageRatingTitle");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.FirstReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("firstReleaseDate");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.GameStatus).HasColumnName("gameStatus");

                entity.Property(e => e.GameTitle)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameTitle");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("rating");

                entity.Property(e => e.TotalReviews).HasColumnName("totalReviews");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.VersionTitle)
                    .HasMaxLength(255)
                    .HasColumnName("versionTitle");
            });

            modelBuilder.Entity<VideoGameMultiplayerMode>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7874FD802");

                entity.ToTable("videoGameMultiplayerMode");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.CampaignCoop).HasColumnName("campaignCoop");

                entity.Property(e => e.DropIn).HasColumnName("dropIn");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.LanCoop).HasColumnName("lanCoop");

                entity.Property(e => e.OfflineCoop).HasColumnName("offlineCoop");

                entity.Property(e => e.OfflineCoopMax).HasColumnName("offlineCoopMax");

                entity.Property(e => e.OnlineCoop).HasColumnName("onlineCoop");

                entity.Property(e => e.OnlineCoopMax).HasColumnName("onlineCoopMax");

                entity.Property(e => e.OnlineMax).HasColumnName("onlineMax");

                entity.Property(e => e.Splitscreen).HasColumnName("splitscreen");

                entity.Property(e => e.SplitscreenOnline).HasColumnName("splitscreenOnline");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameMultiplayerModes)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__5165187F");
            });

            modelBuilder.Entity<VideoGamePlatform>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D77FA951A8");

                entity.ToTable("videoGamePlatforms");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.Platform)
                    .HasMaxLength(255)
                    .HasColumnName("platform");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGamePlatforms)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__4F7CD00D");
            });

            modelBuilder.Entity<VideoGameTheme>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7D2C0E38A");

                entity.ToTable("videoGameThemes");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.ThemeName)
                    .HasMaxLength(255)
                    .HasColumnName("themeName");

                entity.Property(e => e.ThemeSlug)
                    .HasMaxLength(255)
                    .HasColumnName("themeSlug");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameThemes)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__52593CB8");
            });

            modelBuilder.Entity<VideoGameUserContent>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7FF1B40F5");

                entity.ToTable("videoGameUserContent");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userID");

                entity.Property(e => e.UserRating)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("userRating");

                entity.Property(e => e.UserReview)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userReview");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameUserContents)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__4CA06362");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VideoGameUserContents)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__userI__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

