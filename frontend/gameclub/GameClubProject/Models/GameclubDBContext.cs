using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GameClubProject.Models
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

        public virtual DbSet<MembershipStatus> MembershipStatuses { get; set; }
        public virtual DbSet<PersonalUserTrackedGame> PersonalUserTrackedGames { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserGamerTag> UserGamerTags { get; set; }
        public virtual DbSet<VideoGameAlternativeName> VideoGameAlternativeNames { get; set; }
        public virtual DbSet<VideoGameArtwork> VideoGameArtworks { get; set; }
        public virtual DbSet<VideoGameCharacter> VideoGameCharacters { get; set; }
        public virtual DbSet<VideoGameCharacterAlias> VideoGameCharacterAliases { get; set; }
        public virtual DbSet<VideoGameCharacterAppearsIn> VideoGameCharacterAppearsIns { get; set; }
        public virtual DbSet<VideoGameGameMode> VideoGameGameModes { get; set; }
        public virtual DbSet<VideoGameGenre> VideoGameGenres { get; set; }
        public virtual DbSet<VideoGameInvolvedCompany> VideoGameInvolvedCompanies { get; set; }
        public virtual DbSet<VideoGameKeyword> VideoGameKeywords { get; set; }
        public virtual DbSet<VideoGameMain> VideoGameMains { get; set; }
        public virtual DbSet<VideoGameMultiplayerMode> VideoGameMultiplayerModes { get; set; }
        public virtual DbSet<VideoGamePlatform> VideoGamePlatforms { get; set; }
        public virtual DbSet<VideoGameScreenshot> VideoGameScreenshots { get; set; }
        public virtual DbSet<VideoGameSimilarTitle> VideoGameSimilarTitles { get; set; }
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

            modelBuilder.Entity<MembershipStatus>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__membersh__4866A5D7258385FF");

                entity.ToTable("membershipStatus");

                entity.HasIndex(e => e.MembershipStatusId, "UQ__membersh__19137F51B5276FBC")
                    .IsUnique();

                entity.HasIndex(e => e.Description, "UQ__membersh__489B0D9753DD5E21")
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
                    .HasName("PK__personal__4866A5D7078437FF");

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
                    .HasConstraintName("FK__personalU__gameI__534D60F1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PersonalUserTrackedGames)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__personalU__userI__52593CB8");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__userAcco__4866A5D7EA46BED5");

                entity.ToTable("userAccount");

                entity.HasIndex(e => e.UserName, "UQ__userAcco__66DCF95CD55BBC02")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__userAcco__AB6E6164C75B5F3E")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "UQ__userAcco__CB9A1CDE05D982F6")
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
                    .HasConstraintName("FK__userAccou__membe__5070F446");
            });

            modelBuilder.Entity<UserGamerTag>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__userGame__4866A5D7A2E7BEBD");

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
                    .HasConstraintName("FK__userGamer__userI__5165187F");
            });

            modelBuilder.Entity<VideoGameAlternativeName>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D788948344");

                entity.ToTable("videoGameAlternativeNames");

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
                    .WithMany(p => p.VideoGameAlternativeNames)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__5441852A");
            });

            modelBuilder.Entity<VideoGameArtwork>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D753B7E936");

                entity.ToTable("videoGameArtwork");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.AlphaChannel).HasColumnName("alphaChannel");

                entity.Property(e => e.Animated).HasColumnName("animated");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ImageId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("imageID");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameArtworks)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__4F7CD00D");
            });

            modelBuilder.Entity<VideoGameCharacter>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PK__videoGam__DA90B4B22C1B9823");

                entity.ToTable("videoGameCharacters");

                entity.HasIndex(e => e.CharacterId, "UQ__videoGam__ADF9199EA481EA9B")
                    .IsUnique();

                entity.Property(e => e.GameId)
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.AlphaChannel).HasColumnName("alphaChannel");

                entity.Property(e => e.Animated).HasColumnName("animated");

                entity.Property(e => e.CharacterId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("characterID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .HasColumnName("countryName");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.MugshotId).HasColumnName("mugshotID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PKey)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("pKey");

                entity.Property(e => e.Species).HasColumnName("species");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.HasOne(d => d.Game)
                    .WithOne(p => p.VideoGameCharacter)
                    .HasPrincipalKey<VideoGameMain>(p => p.GameId)
                    .HasForeignKey<VideoGameCharacter>(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__5812160E");
            });

            modelBuilder.Entity<VideoGameCharacterAlias>(entity =>
            {
                entity.HasKey(e => e.Pkey)
                    .HasName("PK__videoGam__40A62DB99B16859E");

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
                    .HasPrincipalKey(p => p.CharacterId)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK__videoGame__chara__628FA481");
            });

            modelBuilder.Entity<VideoGameCharacterAppearsIn>(entity =>
            {
                entity.HasKey(e => e.CharacterId)
                    .HasName("PK__videoGam__ADF9199F468E58AA");

                entity.ToTable("videoGameCharacterAppearsIn");

                entity.Property(e => e.CharacterId)
                    .HasMaxLength(255)
                    .HasColumnName("characterID");

                entity.Property(e => e.GameId)
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.PKey)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("pKey");

                entity.HasOne(d => d.Character)
                    .WithOne(p => p.VideoGameCharacterAppearsIn)
                    .HasPrincipalKey<VideoGameCharacter>(p => p.CharacterId)
                    .HasForeignKey<VideoGameCharacterAppearsIn>(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__chara__6383C8BA");
            });

            modelBuilder.Entity<VideoGameGameMode>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7A914C8B4");

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
                    .HasConstraintName("FK__videoGame__gameI__5535A963");
            });

            modelBuilder.Entity<VideoGameGenre>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D705CDFAE4");

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
                    .HasConstraintName("FK__videoGame__gameI__5AEE82B9");
            });

            modelBuilder.Entity<VideoGameInvolvedCompany>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7924CFC58");

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
                    .HasConstraintName("FK__videoGame__gameI__5EBF139D");
            });

            modelBuilder.Entity<VideoGameKeyword>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D77A2D6DA5");

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
                    .HasConstraintName("FK__videoGame__gameI__5DCAEF64");
            });

            modelBuilder.Entity<VideoGameMain>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D791060557");

                entity.ToTable("videoGameMain");

                entity.HasIndex(e => e.GameId, "UQ__videoGam__DA90B4B340B464EA")
                    .IsUnique();

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.AggregatedRating)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("aggregatedRating");

                entity.Property(e => e.AggregatedRatingCount).HasColumnName("aggregatedRatingCount");

                entity.Property(e => e.AlphaChannel).HasColumnName("alphaChannel");

                entity.Property(e => e.Animated).HasColumnName("animated");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CoverUrl)
                    .HasMaxLength(255)
                    .HasColumnName("coverUrl");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.FirstReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("firstReleaseDate");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.GameStatus)
                    .HasMaxLength(255)
                    .HasColumnName("gameStatus");

                entity.Property(e => e.GameTitle)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameTitle");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ImageId)
                    .HasMaxLength(255)
                    .HasColumnName("imageID");

                entity.Property(e => e.Storyline).HasColumnName("storyline");

                entity.Property(e => e.Summary).HasColumnName("summary");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.Property(e => e.VersionTitle)
                    .HasMaxLength(255)
                    .HasColumnName("versionTitle");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<VideoGameMultiplayerMode>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7AB0C4313");

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
                    .HasConstraintName("FK__videoGame__gameI__5BE2A6F2");
            });

            modelBuilder.Entity<VideoGamePlatform>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7D0655899");

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
                    .HasConstraintName("FK__videoGame__gameI__59FA5E80");
            });

            modelBuilder.Entity<VideoGameScreenshot>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D75D497DE8");

                entity.ToTable("videoGameScreenshot");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.AlphaChannel).HasColumnName("alphaChannel");

                entity.Property(e => e.Animated).HasColumnName("animated");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ImageId)
                    .HasMaxLength(255)
                    .HasColumnName("imageID");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.VideoGameScreenshots)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__gameI__4E88ABD4");
            });

            modelBuilder.Entity<VideoGameSimilarTitle>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D723D5AAE9");

                entity.ToTable("videoGameSimilarTitles");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.HostGameId)
                    .HasMaxLength(255)
                    .HasColumnName("hostGameID");

                entity.Property(e => e.SimilarGameId)
                    .HasMaxLength(255)
                    .HasColumnName("similarGameID");

                entity.HasOne(d => d.HostGame)
                    .WithMany(p => p.VideoGameSimilarTitles)
                    .HasPrincipalKey(p => p.GameId)
                    .HasForeignKey(d => d.HostGameId)
                    .HasConstraintName("FK__videoGame__hostG__5FB337D6");
            });

            modelBuilder.Entity<VideoGameTheme>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7A07CD8FC");

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
                    .HasConstraintName("FK__videoGame__gameI__5CD6CB2B");
            });

            modelBuilder.Entity<VideoGameUserContent>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D78D4A9CC3");

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
                    .HasConstraintName("FK__videoGame__gameI__571DF1D5");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VideoGameUserContents)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__userI__5629CD9C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
