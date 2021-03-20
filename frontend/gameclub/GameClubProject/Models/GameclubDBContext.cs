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

        public virtual DbSet<IgdbApi> IgdbApis { get; set; }
        public virtual DbSet<MembershipStatus> MembershipStatuses { get; set; }
        public virtual DbSet<PersonalUserTrackedGame> PersonalUserTrackedGames { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserGamerTag> UserGamerTags { get; set; }
        public virtual DbSet<VideoGameMain> VideoGameMains { get; set; }
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

            modelBuilder.Entity<IgdbApi>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__IGDB_API__4866A5D74AC7DE72");

                entity.ToTable("IGDB_API");

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.IgdbClientId)
                    .HasMaxLength(255)
                    .HasColumnName("IGDB_CLIENT_ID");

                entity.Property(e => e.IgdbClientSecret)
                    .HasMaxLength(255)
                    .HasColumnName("IGDB_CLIENT_SECRET");
            });

            modelBuilder.Entity<MembershipStatus>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__membersh__4866A5D728304AAD");

                entity.ToTable("membershipStatus");

                entity.HasIndex(e => e.MembershipStatusId, "UQ__membersh__19137F515B6CBCD7")
                    .IsUnique();

                entity.HasIndex(e => e.Description, "UQ__membersh__489B0D977BE934CA")
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
                    .HasName("PK__personal__4866A5D7FE24FF2F");

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
                    .HasConstraintName("FK__personalU__gameI__37A5467C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PersonalUserTrackedGames)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__personalU__userI__36B12243");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__userAcco__4866A5D77FE73871");

                entity.ToTable("userAccount");

                entity.HasIndex(e => e.UserName, "UQ__userAcco__66DCF95CB977994E")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__userAcco__AB6E616446AD2B02")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "UQ__userAcco__CB9A1CDEA43BF1A1")
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
                    .HasConstraintName("FK__userAccou__membe__34C8D9D1");
            });

            modelBuilder.Entity<UserGamerTag>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__userGame__4866A5D72D774886");

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
                    .HasConstraintName("FK__userGamer__userI__35BCFE0A");
            });

            modelBuilder.Entity<VideoGameMain>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D71B0443C9");

                entity.ToTable("videoGameMain");

                entity.HasIndex(e => e.GameId, "UQ__videoGam__DA90B4B3C75AA614")
                    .IsUnique();

                entity.Property(e => e.PKey).HasColumnName("pKey");

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("gameID");
            });

            modelBuilder.Entity<VideoGameUserContent>(entity =>
            {
                entity.HasKey(e => e.PKey)
                    .HasName("PK__videoGam__4866A5D7C9368B34");

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
                    .HasConstraintName("FK__videoGame__gameI__398D8EEE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VideoGameUserContents)
                    .HasPrincipalKey(p => p.UserId)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__videoGame__userI__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
