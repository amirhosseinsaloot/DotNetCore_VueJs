using Core.Entities.Identity;

namespace Infrastructure.Data.DbObjects.TenantDbObject;

public interface ITenantDbObject
{
    /// <summary>
    /// Asynchronously find the tenant from user.
    /// </summary>
    /// <param name="userId">Id of user for finding Tenant entity that user belongs to.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is the Tenant entity that user belongs to that mapped to TDto.
    /// </returns>
    Task<Tenant?> GetTenantByUserAsync(int userId, CancellationToken cancellationToken);
}
