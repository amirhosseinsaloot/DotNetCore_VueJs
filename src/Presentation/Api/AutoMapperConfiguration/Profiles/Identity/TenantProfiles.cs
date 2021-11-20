using AutoMapper;
using Data.Entities.Identity;
using Services.Domain;

namespace Api.AutoMapperConfiguration;

public class TenantProfiles : Profile
{
    #region Ctor

    public TenantProfiles()
    {
        CreateMap<Tenant, TenantViewModel>();

        CreateMap<Tenant, TenantListViewModel>();
    }

    #endregion Ctor
}
