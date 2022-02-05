using AutoMapper;
using Data.Entities.Teams;
using Service.DomainDto.Team;

namespace Api.AutoMapperConfiguration.Profiles.Teams;

public class TeamProfiles : Profile
{
    #region Ctor

    public TeamProfiles()
    {
        TeamProfile();
    }

    #endregion Ctor

    #region Methods

    public void TeamProfile()
    {
        CreateMap<TeamCreateUpdateDto, Team>()
            .ForMember(dest => dest.Id, src => src.Ignore());

        CreateMap<Team, TeamDto>();

        CreateMap<Team, TeamListDto>();
    }

    #endregion Methods
}
