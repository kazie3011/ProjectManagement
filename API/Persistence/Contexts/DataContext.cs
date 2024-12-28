using API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Contexts;

public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(true, cancellationToken);
    }
    
    public override int SaveChanges() => throw new NotImplementedException();
    public override int SaveChanges(bool acceptAllChangesOnSuccess) => throw new NotImplementedException();
}