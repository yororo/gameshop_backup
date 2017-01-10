using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GameShop.Data.EF.Contexts;
using GameShop.Contracts.Enumerations;

namespace GameShop.Data.EF.Migrations
{
    [DbContext(typeof(GameShopContext))]
    [Migration("20170110192602_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameShop.Data.EF.Entities.Games.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AdvertisementId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<short>("GameGenre");

                    b.Property<short>("GamePlatform");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<short>("State");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Games.GameAdvertisement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("FriendlyId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<short>("State");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("GameAdvertisements");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Games.GameSellingInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<short>("Currency");

                    b.Property<Guid>("GameId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ReasonForSelling");

                    b.Property<decimal>("SellingPrice");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("GameSellingInformation");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Games.GameTradingInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CashAmountToAdd");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<short>("Currency");

                    b.Property<Guid>("GameId");

                    b.Property<bool>("IsOwnerWillingToAddCash");

                    b.Property<bool>("IsOwnerWillingToReceiveCash");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("ReasonForSelling");

                    b.Property<string>("TradeNotes");

                    b.Property<decimal>("TradingPrice");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("GameTradingInformation");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Games.Game", b =>
                {
                    b.HasOne("GameShop.Data.EF.Entities.Games.GameAdvertisement", "Advertisement")
                        .WithMany("Games")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Games.GameSellingInformation", b =>
                {
                    b.HasOne("GameShop.Data.EF.Entities.Games.Game", "Game")
                        .WithOne("SellingInformation")
                        .HasForeignKey("GameShop.Data.EF.Entities.Games.GameSellingInformation", "GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Games.GameTradingInformation", b =>
                {
                    b.HasOne("GameShop.Data.EF.Entities.Games.Game", "Game")
                        .WithOne("TradingInformation")
                        .HasForeignKey("GameShop.Data.EF.Entities.Games.GameTradingInformation", "GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
