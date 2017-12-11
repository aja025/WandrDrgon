using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WanderDragon.Data.Migrations
{
    public partial class update01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DragonSprite",
                columns: table => new
                {
                    DragonSpriteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Image1 = table.Column<string>(type: "TEXT", nullable: true),
                    Image2 = table.Column<string>(type: "TEXT", nullable: true),
                    Image3 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragonSprite", x => x.DragonSpriteId);
                });

            migrationBuilder.CreateTable(
                name: "ItemSprite",
                columns: table => new
                {
                    ItemSpriteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    ItemName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSprite", x => x.ItemSpriteId);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AboutMe = table.Column<string>(type: "TEXT", nullable: true),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Dragon",
                columns: table => new
                {
                    DragonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Birthdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChallengesWon = table.Column<int>(type: "INTEGER", nullable: false),
                    DragonSpriteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Hometown = table.Column<string>(type: "TEXT", nullable: true),
                    KmRadius = table.Column<decimal>(type: "TEXT", nullable: false),
                    KmTravelled = table.Column<decimal>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    TripsTaken = table.Column<int>(type: "INTEGER", nullable: false),
                    XP = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dragon", x => x.DragonId);
                    table.ForeignKey(
                        name: "FK_Dragon_DragonSprite_DragonSpriteId",
                        column: x => x.DragonSpriteId,
                        principalTable: "DragonSprite",
                        principalColumn: "DragonSpriteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dragon_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemLog",
                columns: table => new
                {
                    ItemLogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Alexandrite = table.Column<int>(type: "INTEGER", nullable: false),
                    Amethyst = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimalMask = table.Column<int>(type: "INTEGER", nullable: false),
                    Aquamarine = table.Column<int>(type: "INTEGER", nullable: false),
                    BeadedNecklace = table.Column<int>(type: "INTEGER", nullable: false),
                    Bloodstone = table.Column<int>(type: "INTEGER", nullable: false),
                    Boomarang = table.Column<int>(type: "INTEGER", nullable: false),
                    Cherryblossom = table.Column<int>(type: "INTEGER", nullable: false),
                    Cholla = table.Column<int>(type: "INTEGER", nullable: false),
                    Croissant = table.Column<int>(type: "INTEGER", nullable: false),
                    Garnet = table.Column<int>(type: "INTEGER", nullable: false),
                    Hamburger = table.Column<int>(type: "INTEGER", nullable: false),
                    HockeyPuck = table.Column<int>(type: "INTEGER", nullable: false),
                    Jade = table.Column<int>(type: "INTEGER", nullable: false),
                    KoalaDoll = table.Column<int>(type: "INTEGER", nullable: false),
                    MapleLeaf = table.Column<int>(type: "INTEGER", nullable: false),
                    MatryoshkaDoll = table.Column<int>(type: "INTEGER", nullable: false),
                    Moonstone = table.Column<int>(type: "INTEGER", nullable: false),
                    Noodlebowl = table.Column<int>(type: "INTEGER", nullable: false),
                    Opal = table.Column<int>(type: "INTEGER", nullable: false),
                    ParrotFeather = table.Column<int>(type: "INTEGER", nullable: false),
                    Pearl = table.Column<int>(type: "INTEGER", nullable: false),
                    Pierogi = table.Column<int>(type: "INTEGER", nullable: false),
                    PinaColada = table.Column<int>(type: "INTEGER", nullable: false),
                    Pineapple = table.Column<int>(type: "INTEGER", nullable: false),
                    Plumeria = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    Sapphire = table.Column<int>(type: "INTEGER", nullable: false),
                    TeaBag = table.Column<int>(type: "INTEGER", nullable: false),
                    TigersEye = table.Column<int>(type: "INTEGER", nullable: false),
                    Topaz = table.Column<int>(type: "INTEGER", nullable: false),
                    Turquoise = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLog", x => x.ItemLogId);
                    table.ForeignKey(
                        name: "FK_ItemLog_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelLog",
                columns: table => new
                {
                    TravelLogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelLog", x => x.TravelLogId);
                    table.ForeignKey(
                        name: "FK_TravelLog_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dragon_DragonSpriteId",
                table: "Dragon",
                column: "DragonSpriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Dragon_ProfileId",
                table: "Dragon",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLog_ProfileId",
                table: "ItemLog",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelLog_ProfileId",
                table: "TravelLog",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dragon");

            migrationBuilder.DropTable(
                name: "ItemLog");

            migrationBuilder.DropTable(
                name: "ItemSprite");

            migrationBuilder.DropTable(
                name: "TravelLog");

            migrationBuilder.DropTable(
                name: "DragonSprite");

            migrationBuilder.DropTable(
                name: "Profile");
        }
    }
}
