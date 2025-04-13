using Bira.App.SchoolManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bira.App.SchoolManager.Infra.Repositories.BaseContext
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e =>
            e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DateRegistration") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DateRegistration").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("DateRegistration").IsModified = false;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}