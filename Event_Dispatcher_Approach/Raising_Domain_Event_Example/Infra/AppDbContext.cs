using Microsoft.EntityFrameworkCore;
using Raising_Domain_Event_Example.Domain.Company;
// persistence
class AppDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Company> Companies => Set<Company>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.Entity<Account>(entity => { 
            entity.HasKey(e => e.Id);
            entity.Ignore(e => e.Events);
        });
}
