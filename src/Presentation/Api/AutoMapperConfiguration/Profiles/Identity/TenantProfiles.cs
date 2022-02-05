using AutoMapper;
using Core.Entities.Identity;
using Infrastructure.Dto.Tenant;

namespace Api.AutoMapperConfiguration.Profiles.Identity;

public class TenantProfiles : Profile
{
    public TenantProfiles()
    {
        CreateMap<Tenant, TenantDto>();
        CreateMap<Tenant, TenantListDto>();
    }
}