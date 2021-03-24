using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameClubProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IGDB_API",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IGDB_CLIENT_ID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IGDB_CLIENT_SECRET = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__IGDB_API__4866A5D74AC7DE72", x => x.pKey);
                });

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
                    table.PrimaryKey("PK__membersh__4866A5D728304AAD", x => x.pKey);
                    table.UniqueConstraint("AK_membershipStatus_membershipStatusID", x => x.membershipStatusID);
                });

            migrationBuilder.CreateTable(
                name: "videoGameMain",
                columns: table => new
                {
                    pKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoGam__4866A5D71B0443C9", x => x.pKey);
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
                    membershipStatusID = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK__userAcco__4866A5D77FE73871", x => x.pKey);
                    table.UniqueConstraint("AK_userAccount_userID", x => x.userID);
                    table.ForeignKey(
                        name: "FK__userAccou__membe__34C8D9D1",
                        column: x => x.membershipStatusID,
                        principalTable: "membershipStatus",
                        principalColumn: "membershipStatusID",
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
                    table.PrimaryKey("PK__personal__4866A5D7FE24FF2F", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__personalU__gameI__37A5467C",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__personalU__userI__36B12243",
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
                    table.PrimaryKey("PK__userGame__4866A5D72D774886", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__userGamer__userI__35BCFE0A",
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
                    table.PrimaryKey("PK__videoGam__4866A5D7C9368B34", x => x.pKey);
                    table.ForeignKey(
                        name: "FK__videoGame__gameI__398D8EEE",
                        column: x => x.gameID,
                        principalTable: "videoGameMain",
                        principalColumn: "gameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__videoGame__userI__38996AB5",
                        column: x => x.userID,
                        principalTable: "userAccount",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__membersh__19137F515B6CBCD7",
                table: "membershipStatus",
                column: "membershipStatusID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__membersh__489B0D977BE934CA",
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
                name: "UQ__userAcco__66DCF95CB977994E",
                table: "userAccount",
                column: "userName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__userAcco__AB6E616446AD2B02",
                table: "userAccount",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__userAcco__CB9A1CDEA43BF1A1",
                table: "userAccount",
                column: "userID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userGamerTags_userID",
                table: "userGamerTags",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "UQ__videoGam__DA90B4B3C75AA614",
                table: "videoGameMain",
                column: "gameID",
                unique: true);

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
                name: "IGDB_API");

            migrationBuilder.DropTable(
                name: "personalUserTrackedGames");

            migrationBuilder.DropTable(
                name: "userGamerTags");

            migrationBuilder.DropTable(
                name: "videoGameUserContent");

            migrationBuilder.DropTable(
                name: "videoGameMain");

            migrationBuilder.DropTable(
                name: "userAccount");

            migrationBuilder.DropTable(
                name: "membershipStatus");
        }
    }
}
