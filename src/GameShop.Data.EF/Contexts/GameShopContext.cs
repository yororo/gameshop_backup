using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GameShop.Contracts.Enumerations;
using GameShop.Data.EF.Entities;
using GameShop.Data.EF.Entities.Products;

namespace GameShop.Data.EF.Contexts
{
    internal class GameShopContext : DbContext
     {
        // public DbSet<Profile> Profiles { get; set; }
        // public DbSet<ProfileAddress> ProfileAddresses { get; set; }
        // public DbSet<ProfileContactInformation> ProfileContactInformation { get; set; }

        public DbSet<EfAdvertisement> Advertisements { get; set; }

        public DbSet<EfAdvertisementProducts> AdvertisementProducts { get; set; }

        #region Product DbSets

        public DbSet<EfProduct> Products { get; set; }
        public DbSet<EfGame> Games { get; set; }
        public DbSet<EfGameConsole> GameConsoles { get; set; }
            
        #endregion Product DbSets

        public DbSet<EfSellingInformation> SellingInformation { get; set; }
        public DbSet<EfTradingInformation> TradingInformation { get; set; }

        public DbSet<EfMeetupInformation> MeetupInformation { get; set; }
        public DbSet<EfMeetupLocation> MeetupLocations { get; set; }

        public GameShopContext(DbContextOptions options)
            : base(options)
        {
            // Don't track results of query for performance.
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set composite keys.
            modelBuilder.Entity<EfAdvertisementProducts>()
                        .HasKey(ap => new { ap.AdvertisementId, ap.ProductId });

            // ONE (Advertisement) to MANY (Products)
            modelBuilder.Entity<EfAdvertisementProducts>()
                        .HasOne(ap => ap.Advertisement)
                        .WithMany(advertisement => advertisement.AdvertisementProducts)
                        .HasForeignKey(ap => ap.AdvertisementId);

            // ONE (Product) to MANY (Advertisements).
            modelBuilder.Entity<EfAdvertisementProducts>()
                        .HasOne(ap => ap.Product)
                        .WithMany(product => product.AdvertisementProducts)
                        .HasForeignKey(ap => ap.ProductId);

            // ONE (Product) to ONE (SellingInformation).
            modelBuilder.Entity<EfProduct>()
                        .HasOne(product => product.SellingInformation)
                        .WithOne(sellingInfo => sellingInfo.Product)
                        .HasForeignKey<EfSellingInformation>(sellingInfo => sellingInfo.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);

            // ONE (Product) to ONE (TradingInformation).
            modelBuilder.Entity<EfProduct>()
                        .HasOne(product => product.TradingInformation)
                        .WithOne(tradingInfo => tradingInfo.Product)
                        .HasForeignKey<EfTradingInformation>(tradingInfo => tradingInfo.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);

            // Set General ProductCategory as Discriminator value.
            modelBuilder.Entity<EfProduct>()
                        .HasDiscriminator()
                        .HasValue<EfProduct>(ProductCategory.General.ToString())
                        .HasValue<EfGame>(ProductCategory.Games.ToString())
                        .HasValue<EfGameConsole>(ProductCategory.GameConsoles.ToString());

            // Set EfProduct as base type and set the Games ProductCategory as Discriminator value.
            modelBuilder.Entity<EfGame>()
                        .HasBaseType<EfProduct>();

            // Set EfProduct as base type and set the GameConsoles ProductCategory as Discriminator value.
            modelBuilder.Entity<EfGameConsole>()
                        .HasBaseType<EfProduct>();

            // ONE (Advertisement) to ONE (MeetupInformation).
            modelBuilder.Entity<EfAdvertisement>()
                        .HasOne(advertisement => advertisement.MeetupInformation)
                        .WithOne(meetupInfo => meetupInfo.Advertisement)
                        .HasForeignKey<EfMeetupInformation>(meetupInfo => meetupInfo.AdvertisementId)
                        .OnDelete(DeleteBehavior.Cascade);
                        
            // ONE (MeetupInformation) to MANY (MeetupLocations).
            modelBuilder.Entity<EfMeetupInformation>()
                        .HasMany(meetupInfo => meetupInfo.MeetupLocations)
                        .WithOne(meetupLocation => meetupLocation.MeetupInformation)
                        .HasForeignKey(meetupLocation => meetupLocation.MeetupInformationId);

            /*
            //    Table Names
            */

            modelBuilder.Entity<EfAdvertisement>()
                        .ToTable("Advertisements");

            modelBuilder.Entity<EfProduct>()
                        .ToTable("Products");
            
            modelBuilder.Entity<EfAdvertisementProducts>()
                        .ToTable("AdvertisementProducts");

            modelBuilder.Entity<EfSellingInformation>()
                        .ToTable("SellingInformation");

            modelBuilder.Entity<EfTradingInformation>()
                        .ToTable("TradingInformation");

            modelBuilder.Entity<EfMeetupInformation>()
                        .ToTable("MeetupInformation");

            modelBuilder.Entity<EfMeetupLocation>()
                        .ToTable("MeetupLocations");

            // // Identity table names.
            // modelBuilder.Entity<User>()
            //             .ToTable("Users");

            // modelBuilder.Entity<IdentityUserRole<Guid>>()
            //             .ToTable("UserRoles");

            // modelBuilder.Entity<IdentityRole<Guid>>()
            //             .ToTable("Roles");

            // modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            //             .ToTable("RoleClaims");

            // modelBuilder.Entity<IdentityUserClaim<Guid>>()
            //              .ToTable("UserClaims");

            // modelBuilder.Entity<IdentityUserToken<Guid>>()
            //             .ToTable("UserTokens");

            // modelBuilder.Entity<IdentityUserLogin<Guid>>()
            //             .ToTable("UserLogins");

            // // User-Profile relationships.
            // modelBuilder.Entity<User>()
            //             .HasOne(user => user.Profile)
            //             .WithOne(profile => profile.User)
            //             .HasForeignKey<Profile>(profile => profile.UserId);

            // // Profile-Address relationships.
            // modelBuilder.Entity<Profile>()
            //             .HasMany(profile => profile.Addresses)
            //             .WithOne(address => address.Profile)
            //             .HasForeignKey(address => address.ProfileId);

            // modelBuilder.Entity<ProfileAddress>()
            //             .HasKey(pa => pa.ProfileId);

            // // Profile-ContactInformation relationships.
            // modelBuilder.Entity<Profile>()
            //             .HasMany(profile => profile.ContactInformation)
            //             .WithOne(contactInformation => contactInformation.Profile)
            //             .HasForeignKey(contactInformation => contactInformation.ProfileId);

            // modelBuilder.Entity<ProfileContactInformation>()
            //             .HasKey(pc => pc.ProfileId);
        }

        #region Save Methods

        //
        // Summary:
        //     Saves all changes made in this context to the database.
        //
        // Returns:
        //     The number of state entries written to the database.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        public override int SaveChanges()
        {
            // Set  timestamps before saving.
            SetTimestamps();
            return base.SaveChanges();
        }

        //
        // Summary:
        //     Saves all changes made in this context to the database.
        //
        // Parameters:
        //   acceptAllChangesOnSuccess:
        //     Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        //     is called after the changes have been sent successfully to the database.
        //
        // Returns:
        //     The number of state entries written to the database.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            // Set  timestamps before saving.
            SetTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        //
        // Summary:
        //     Asynchronously saves all changes made in this context to the database.
        //
        // Parameters:
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Returns:
        //     A task that represents the asynchronous save operation. The task result contains
        //     the number of state entries written to the database.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        //     Multiple active operations on the same context instance are not supported. Use
        //     'await' to ensure that any asynchronous operations have completed before calling
        //     another method on this context.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Set  timestamps before saving.
            SetTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        //
        // Summary:
        //     Asynchronously saves all changes made in this context to the database.
        //
        // Parameters:
        //   acceptAllChangesOnSuccess:
        //     Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        //     is called after the changes have been sent successfully to the database.
        //
        //   cancellationToken:
        //     A System.Threading.CancellationToken to observe while waiting for the task to
        //     complete.
        //
        // Returns:
        //     A task that represents the asynchronous save operation. The task result contains
        //     the number of state entries written to the database.
        //
        // Remarks:
        //     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        //     to discover any changes to entity instances before saving to the underlying database.
        //     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        //     Multiple active operations on the same context instance are not supported. Use
        //     'await' to ensure that any asynchronous operations have completed before calling
        //     another method on this context.
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Set  timestamps before saving.
            SetTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Set entity timestamps before saving to database.
        /// </summary>
        private void SetTimestamps()
        {
            // Get all entities who are in Added/Modified state.
            var entities = ChangeTracker.Entries()
                            .Where(x => x.Entity is IEfEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IEfEntity)entity.Entity).CreatedDate = DateTime.Now;
                }

                ((IEfEntity)entity.Entity).ModifiedDate = DateTime.Now;
            }
        }

        #endregion Save Methods
    }
}
