using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GameShop.Data.EF.Contexts;
using GameShop.Contracts.Enumerations;

namespace GameShop.Data.EF.Migrator.Migrations
{
    [DbContext(typeof(GameShopContext))]
    [Migration("20161124152202_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameShop.Data.EF.Entities.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailVerified");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("PasswordHash");

                    b.Property<Guid>("UserId");

                    b.Property<string>("Username");

                    b.HasKey("AccountId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Profile", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<short>("CivilStatus");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("FirstName");

                    b.Property<short>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<short>("Salutation");

                    b.Property<string>("Suffix");

                    b.Property<Guid>("UserId");

                    b.HasKey("ProfileId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.ProfileAddress", b =>
                {
                    b.Property<Guid>("ProfileId");

                    b.Property<Guid>("AddressId");

                    b.Property<string>("Barangay");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Municipality");

                    b.Property<string>("Province");

                    b.Property<string>("Region");

                    b.Property<string>("Street1");

                    b.Property<string>("Street2");

                    b.Property<string>("Street3");

                    b.Property<string>("ZipCode");

                    b.HasKey("ProfileId");

                    b.ToTable("ProfileAddresses");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.ProfileContactInformation", b =>
                {
                    b.Property<Guid>("ProfileId");

                    b.Property<Guid>("ContactInformationId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("MobileNumber");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("ProfileId");

                    b.ToTable("ProfileContactInformation");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Account", b =>
                {
                    b.HasOne("GameShop.Data.EF.Entities.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("GameShop.Data.EF.Entities.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.Profile", b =>
                {
                    b.HasOne("GameShop.Data.EF.Entities.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("GameShop.Data.EF.Entities.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.ProfileAddress", b =>
                {
                    b.HasOne("GameShop.Data.EF.Entities.Profile", "Profile")
                        .WithMany("Addresses")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameShop.Data.EF.Entities.ProfileContactInformation", b =>
                {
                    b.HasOne("GameShop.Data.EF.Entities.Profile", "Profile")
                        .WithMany("ContactInformation")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
