using Api.AutoMapperConfiguration.Profiles.Authentication;
using Api.AutoMapperConfiguration.Profiles.Identity;
using Api.AutoMapperConfiguration.Profiles.Teams;
using Api.AutoMapperConfiguration.Profiles.Tickets;
using AutoMapper;
using System.Reflection;

namespace Api.Extensions;

public static class AutoMapperRegistration
{
    public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        List<Profile> profileList = new List<Profile>()
            {
                new AuthenticationProfiles(),
                new TeamProfiles(),
                new TenantProfiles(),
                new TicketTypeProfiles()
            };

        services.AddAutoMapper(config =>
        {
            config.AddProfiles(profileList);
        }, assemblies);
    }
}
