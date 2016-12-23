using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GameShop.Data.EF.Entities;
using System.Threading;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OpenIddict;

namespace GameShop.Data.EF.Contexts
{
    internal class GameShopContext : OpenIddictDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileAddress> ProfileAddresses { get; set; }
        public DbSet<ProfileContactInformation> ProfileContactInformation { get; set; }

        public GameShopContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity table names.
            modelBuilder.Entity<User>()
                        .ToTable("Users");

            modelBuilder.Entity<IdentityUserRole<Guid>>()
                        .ToTable("UserRoles");

            modelBuilder.Entity<IdentityRole<Guid>>()
                        .ToTable("Roles");

            modelBuilder.Entity<IdentityRoleClaim<Guid>>()
                        .ToTable("RoleClaims");

            modelBuilder.Entity<IdentityUserClaim<Guid>>()
                         .ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserToken<Guid>>()
                        .ToTable("UserTokens");

            modelBuilder.Entity<IdentityUserLogin<Guid>>()
                        .ToTable("UserLogins");

            // User-Profile relationships.
            modelBuilder.Entity<User>()
                        .HasOne(user => user.Profile)
                        .WithOne(profile => profile.User)
                        .HasForeignKey<Profile>(profile => profile.UserId);

            // Profile-Address relationships.
            modelBuilder.Entity<Profile>()
                        .HasMany(profile => profile.Addresses)
                        .WithOne(address => address.Profile)
                        .HasForeignKey(address => address.ProfileId);

            modelBuilder.Entity<ProfileAddress>()
                        .HasKey(pa => pa.ProfileId);

            // Profile-ContactInformation relationships.
            modelBuilder.Entity<Profile>()
                        .HasMany(profile => profile.ContactInformation)
                        .WithOne(contactInformation => contactInformation.Profile)
                        .HasForeignKey(contactInformation => contactInformation.ProfileId);

            modelBuilder.Entity<ProfileContactInformation>()
                        .HasKey(pc => pc.ProfileId);
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
                            .Where(x => x.Entity is IEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IEntity)entity.Entity).CreatedDate = DateTime.Now;
                }

                ((IEntity)entity.Entity).ModifiedDate = DateTime.Now;
            }
        }

        #endregion Save Methods
    }
}
