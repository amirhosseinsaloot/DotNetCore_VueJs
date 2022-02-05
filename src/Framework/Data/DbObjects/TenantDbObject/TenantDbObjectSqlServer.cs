using Data.Entities.Identity;

namespace Data.DbObjects.TenantDbObject;

public class TenantDbObjectSqlServer : ITenantDbObject
{
    private readonly ApplicationDbContext _applicationDbContext;

    protected readonly DbSet<Tenant> _dbSet;

    public TenantDbObjectSqlServer(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<Tenant>();
    }

    public async Task<Tenant?> GetTenantByUserAsync(int userId, CancellationToken cancellationToken)
    {
        var result = await _dbSet
                          .FromSqlRaw($"EXEC GetTenantByUser @InputUserId = {userId}")
                          .ToListAsync(cancellationToken);
        return result.FirstOrDefault();
    }
}
