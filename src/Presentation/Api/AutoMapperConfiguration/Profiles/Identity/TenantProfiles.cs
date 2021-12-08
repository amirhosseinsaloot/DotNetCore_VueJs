using AutoMapper;
using Data.Entities.Identity;
using Service.Identity.Models;

namespace Api.AutoMapperConfiguration.Profiles.Identity;

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
