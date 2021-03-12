using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameClubProject.Migrations
{
    public partial class GameclubLocalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "membershipStatus",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    membershipStatusID = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__membersh__4866A5D736439232", x => x.pKey);
                    table.UniqueConstraint("AK_membershipStatus_membershipStatusID", x => x.membershipStatusID);
                });

            migrationBuilder.CreateTable(
                name: "videoGameMain",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    gameTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    category = table.Column<int>(type: "int", nullable: true),
                    gameStatus = table.Column<int>(type: "int", nullable: true),
                    ageRatingCategory = table.Column<int>(type: "int", nullable: true),
                    ageRatingTitle = table.Column<int>(type: "int", nullable: true),
                    ageRatingSynopsis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    rating = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    totalReviews = table.Column<int>(type: "int", nullable: true),
                    firstReleaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    versionTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D71B8AF9C7", x => x.pKey);
                    table.UniqueConstraint("AK_videoGameMain_gameID", x => x.gameID);
                });

            migrationBuilder.CreateTable(
                name: "userAccount",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    userName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    membershipStatusID = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    addressStreet = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    addressStreet2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    addressCity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    addressState = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    addressZipCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__userAcco__4866A5D719596C8F", x => x.pKey);
                    table.UniqueConstraint("AK_userAccount_userID", x => x.userID);
                    table.ForeignKey(
                        name: "FK__userAccou__membe__45F365D3",
                        column: x => x.membershipStatusID,
                        principalTable: "membershipStatus",
                        principalColumn: "membershipStatusID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "alternativeGameNames",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    gameTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    altName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    checkSum = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__alternat__4866A5D78439C31F", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__alternati__gameI__49C3F6B7",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameCharacters",
                columns: table => new
                {
                    characterID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    species = table.Column<int>(type: "int", nullable: true),
                    countryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    gender = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__ADF9199F9CDC0201", x => x.characterID);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__4D94879B",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameGameModes",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    gameModeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    gameModeURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D7A59537A2", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__4AB81AF0",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameGenres",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    genreName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    checksum = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D796F744FF", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__5070F446",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameInvolvedCompanies",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    companyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    parentCompany = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D7CDB1A089", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__5441852A",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameKeywords",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    keyword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D74817F98D", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__534D60F1",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameMultiplayerMode",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    campaignCoop = table.Column<bool>(type: "bit", nullable: true),
                    dropIn = table.Column<bool>(type: "bit", nullable: true),
                    lanCoop = table.Column<bool>(type: "bit", nullable: true),
                    offlineCoop = table.Column<bool>(type: "bit", nullable: true),
                    offlineCoopMax = table.Column<int>(type: "int", nullable: true),
                    onlineCoop = table.Column<bool>(type: "bit", nullable: true),
                    onlineCoopMax = table.Column<int>(type: "int", nullable: true),
                    onlineMax = table.Column<int>(type: "int", nullable: true),
                    splitscreen = table.Column<bool>(type: "bit", nullable: true),
                    splitscreenOnline = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D7874FD802", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__5165187F",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGamePlatforms",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    platform = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D77FA951A8", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__4F7CD00D",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameThemes",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    themeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    themeSlug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D7D2C0E38A", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__52593CB8",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personalUserTrackedGames",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__personal__4866A5D7D181A328", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__personalU__gameI__48CFD27E",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__personalU__userI__47DBAE45",
                        column: x => x.userID,
                        principalTable: "userAccount",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "userGamerTags",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    gamerTag = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__userGame__4866A5D7C4862834", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__userGamer__userI__46E78A0C",
                        column: x => x.userID,
                        principalTable: "userAccount",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameUserContent",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    userRating = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    userReview = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D7FF1B40F5", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__4CA06362",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__videoGame__userI__4BAC3F29",
                        column: x => x.userID,
                        principalTable: "userAccount",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videoGameCharacterAlias",
                columns: table => new
                {
                    pkey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    characterID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    aliasName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__40A62DB994424331", x => x.pkey);
                    table.ForeignKey(
                        name: "FK__videoGame__chara__4E88ABD4",
                        column: x => x.characterID,
                        principalTable: "videoGameCharacters",
                        principalColumn: "characterID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alternativeGameNames_gameID",
                table: "alternativeGameNames",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "UQ__membersh__19137F51E3295E81",
                table: "membershipStatus",
                column: "membershipStatusID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__membersh__489B0D97AC3B0CD4",
                table: "membershipStatus",
                column: "description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personalUserTrackedGames_gameID",
                table: "personalUserTrackedGames",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_personalUserTrackedGames_userID",
                table: "personalUserTrackedGames",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_userAccount_membershipStatusID",
                table: "userAccount",
                column: "membershipStatusID");

            migrationBuilder.CreateIndex(
                name: "UQ__userAcco__66DCF95C67CF0D5C",
                table: "userAccount",
                column: "userName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__userAcco__AB6E6164A5A73C2C",
                table: "userAccount",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__userAcco__CB9A1CDE1FFB70EC",
                table: "userAccount",
                column: "userID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userGamerTags_userID",
                table: "userGamerTags",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameCharacterAlias_characterID",
                table: "videoGameCharacterAlias",
                column: "characterID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameCharacters_gameID",
                table: "videoGameCharacters",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameGameModes_gameID",
                table: "videoGameGameModes",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameGenres_gameID",
                table: "videoGameGenres",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameInvolvedCompanies_gameID",
                table: "videoGameInvolvedCompanies",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameKeywords_gameID",
                table: "videoGameKeywords",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "UQ__videoGam__DA90B4B3D5EC9B21",
                table: "videoGameMain",
                column: "gameID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_videoGameMultiplayerMode_gameID",
                table: "videoGameMultiplayerMode",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGamePlatforms_gameID",
                table: "videoGamePlatforms",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameThemes_gameID",
                table: "videoGameThemes",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameUserContent_gameID",
                table: "videoGameUserContent",
                column: "gameID");

            migrationBuilder.CreateIndex(
                name: "IX_videoGameUserContent_userID",
                table: "videoGameUserContent",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alternativeGameNames");

            migrationBuilder.DropTable(
                name: "personalUserTrackedGames");

            migrationBuilder.DropTable(
                name: "userGamerTags");

            migrationBuilder.DropTable(
                name: "videoGameCharacterAlias");

            migrationBuilder.DropTable(
                name: "videoGameGameModes");

            migrationBuilder.DropTable(
                name: "videoGameGenres");

            migrationBuilder.DropTable(
                name: "videoGameInvolvedCompanies");

            migrationBuilder.DropTable(
                name: "videoGameKeywords");

            migrationBuilder.DropTable(
                name: "videoGameMultiplayerMode");

            migrationBuilder.DropTable(
                name: "videoGamePlatforms");

            migrationBuilder.DropTable(
                name: "videoGameThemes");

            migrationBuilder.DropTable(
                name: "videoGameUserContent");

            migrationBuilder.DropTable(
                name: "videoGameCharacters");

            migrationBuilder.DropTable(
                name: "userAccount");

            migrationBuilder.DropTable(
                name: "videoGameMain");

            migrationBuilder.DropTable(
                name: "membershipStatus");
        }
    }
}
