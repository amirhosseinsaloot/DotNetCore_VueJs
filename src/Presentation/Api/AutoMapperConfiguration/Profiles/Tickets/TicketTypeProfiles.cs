using AutoMapper;
using Data.Entities.Tickets;
using Service.Domain.Tickets.Models;

namespace Api.AutoMapperConfiguration.Profiles.Tickets;

public class TicketTypeProfiles : Profile
{
    #region Ctor

    public TicketTypeProfiles()
    {
        TicketTypeProfile();
    }

    #endregion Ctor

    #region Methods

    public void TicketTypeProfile()
    {
        CreateMap<TicketTypeCreateUpdateViewModel, TicketType>()
            .ForMember(dest => dest.Id, src => src.Ignore());

        CreateMap<TicketType, TicketTypeViewModel>();

        CreateMap<TicketType, TicketTypeListViewModel>();
    }

    #endregion Methods
}
