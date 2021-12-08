using AutoMapper;
using Data.Entities.Teams;
using Service.Domain.Teams.Models;

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
        CreateMap<TeamCreateUpdateViewModel, Team>()
            .ForMember(dest => dest.Id, src => src.Ignore());

        CreateMap<Team, TeamViewModel>();

        CreateMap<Team, TeamListViewModel>();
    }

    #endregion Methods
}
