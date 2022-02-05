using AutoMapper;
using Data.Entities.Teams;
using Service.DomainDto.Team;

namespace Api.AutoMapperConfiguration.Profiles.Teams;

public class TeamProfiles : Profile
{
    public TeamProfiles()
    {
        CreateMap<TeamCreateUpdateDto, Team>()
            .ForMember(dest => dest.Id, src => src.Ignore());
        CreateMap<Team, TeamDto>();
        CreateMap<Team, TeamListDto>();
    }
}