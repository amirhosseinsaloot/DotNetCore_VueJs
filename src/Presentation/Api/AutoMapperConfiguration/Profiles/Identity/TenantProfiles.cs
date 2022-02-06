using Api.Dtos.Tenant;
using AutoMapper;
using Core.Entities.Identity;

namespace Api.AutoMapperConfiguration.Profiles.Identity;

public class TenantProfiles : Profile
{
    public TenantProfiles()
    {
        CreateMap<Tenant, TenantDto>();
        CreateMap<Tenant, TenantListDto>();
    }
}