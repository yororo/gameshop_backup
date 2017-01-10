using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.EF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameAdvertisements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FriendlyId = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    State = table.Column<short>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameAdvertisements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdvertisementId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GameGenre = table.Column<short>(nullable: false),
                    GamePlatform = table.Column<short>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    State = table.Column<short>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GameAdvertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "GameAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSellingInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Currency = table.Column<short>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ReasonForSelling = table.Column<string>(nullable: true),
                    SellingPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSellingInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSellingInformation_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameTradingInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CashAmountToAdd = table.Column<decimal>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Currency = table.Column<short>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    IsOwnerWillingToAddCash = table.Column<bool>(nullable: false),
                    IsOwnerWillingToReceiveCash = table.Column<bool>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ReasonForSelling = table.Column<string>(nullable: true),
                    TradeNotes = table.Column<string>(nullable: true),
                    TradingPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTradingInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameTradingInformation_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_AdvertisementId",
                table: "Games",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSellingInformation_GameId",
                table: "GameSellingInformation",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameTradingInformation_GameId",
                table: "GameTradingInformation",
                column: "GameId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSellingInformation");

            migrationBuilder.DropTable(
                name: "GameTradingInformation");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GameAdvertisements");
        }
    }
}
