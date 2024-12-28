using API.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Contexts;

public class ReadOnlyDataContext
{
    private readonly DataContext _context;
    
    public ReadOnlyDataContext(DataContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public IQueryable<User> Users => _context.Users.IgnoreAutoIncludes();
}