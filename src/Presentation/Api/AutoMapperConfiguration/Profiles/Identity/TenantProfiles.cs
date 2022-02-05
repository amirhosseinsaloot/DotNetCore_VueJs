using AutoMapper;
using Data.Entities.Identity;
using Service.DomainDto.Tenant;

namespace Api.AutoMapperConfiguration.Profiles.Identity;

public class TenantProfiles : Profile
{
    public TenantProfiles()
    {
        CreateMap<Tenant, TenantDto>();
        CreateMap<Tenant, TenantListDto>();
    }
}