using Application.Common.Dependencies.Services;
using Domain.Common;
using Domain.Common.Money;
using Domain.Partners;
using Domain.Products;
using Domain.Transactions;
using Infrastructure.Identity.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Partner> Partners => Set<Partner>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        private readonly ICurrentUserService _currentUser;
        private IDateTime _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUser, IDateTime dateTime): base(options)
        {
            this._currentUser = currentUser;
            this._dateTime = dateTime;

            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _currentUser = null!;
            _dateTime = null!;
        }

        public override int SaveChanges() => SaveChanges(acceptAllChangesOnSuccess: true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyMyEntityOverrides();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
            => SaveChangesAsync(acceptAllChangesOnSuccess:true,cancellationToken);

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyMyEntityOverrides();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configBuilder)
        {
            configBuilder.Properties<decimal>()
                .HavePrecision(precision: 18, scale: 4);

            configBuilder.Properties<Currency>();
        }


        private void ApplyMyEntityOverrides()
        {
            foreach (var entry in ChangeTracker.Entries<IAudited>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Property(nameof(IAudited.CreatedBy)).CurrentValue = _currentUser.UserId;
                        entry.Property(nameof(IAudited.CreatedAt)).CurrentValue = DateTime.Now; 
                        break;
                    case EntityState.Modified:
                        entry.Property(nameof(IAudited.LastModifiedBy)).CurrentValue = _currentUser.UserId;
                        entry.Property(nameof(IAudited.LastModifiedAt)).CurrentValue = _dateTime.Now;
                        break;

                }
            }

            foreach(var entry in ChangeTracker.Entries<ISoftDeletable>())
            {
                switch(entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        entry.Property(nameof(ISoftDeletable.DeleteBy)).CurrentValue = _currentUser.UserId;
                        entry.Property(nameof(ISoftDeletable.DeleteAt)).CurrentValue = _dateTime.Now;
                        break;
                }
            }
        }
    }
}
