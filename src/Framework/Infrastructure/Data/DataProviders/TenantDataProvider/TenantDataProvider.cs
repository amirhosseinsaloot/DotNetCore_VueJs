using Core.Entities.Identity;
using Infrastructure.Data.DbObjects.TenantDbObject;

namespace Infrastructure.Data.DataProviders.TenantDataProvider;

public class TenantDataProvider : DataProvider<Tenant>, ITenantDataProvider
{
    private readonly ITenantDbObject _tenantDbObject;

    public TenantDataProvider(ApplicationDbContext dbContext, IMapper mapper, ITenantDbObject tenantDbObject) : base(dbContext, mapper)
    {
        _tenantDbObject = tenantDbObject;
    }

    public async Task<Tenant?> GetTenantByUserAsync(int userId, CancellationToken cancellationToken)
    {
        return await _tenantDbObject.GetTenantByUserAsync(userId, cancellationToken);
    }

    public async Task<TDto?> GetTenantByUserAsync<TDto>(int userId, CancellationToken cancellationToken) where TDto : class, IDto
    {
        var entity = await _tenantDbObject.GetTenantByUserAsync(userId, cancellationToken);
        return _mapper.Map<TDto>(entity);
    }
}