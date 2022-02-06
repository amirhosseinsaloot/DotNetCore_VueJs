using Api.Dtos.Team;
using AutoMapper;
using Core.Entities.Teams;

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