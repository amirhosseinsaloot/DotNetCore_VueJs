using Core.Entities.Identity;

namespace Infrastructure.Data.DbObjects.TenantDbObject;

public class TenantDbObjectPostgres : ITenantDbObject
{
    private readonly ApplicationDbContext _applicationDbContext;

    protected readonly DbSet<Tenant> _dbSet;

    public TenantDbObjectPostgres(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<Tenant>();
    }

    public async Task<Tenant?> GetTenantByUserAsync(int userId, CancellationToken cancellationToken)
    {
        var result = await _dbSet
                           .FromSqlRaw($"SELECT id , name , created_on FROM get_tenant_by_user({userId})")
                           .ToListAsync(cancellationToken);
        return result.FirstOrDefault();
    }
}