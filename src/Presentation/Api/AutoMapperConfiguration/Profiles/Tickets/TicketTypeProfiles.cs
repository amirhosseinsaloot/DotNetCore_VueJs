using AutoMapper;
using Data.Entities.Tickets;
using Service.DomainDto.Ticket;

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
        CreateMap<TicketTypeCreateUpdateDto, TicketType>()
            .ForMember(dest => dest.Id, src => src.Ignore());

        CreateMap<TicketType, TicketTypeDto>();

        CreateMap<TicketType, TicketTypeListDto>();
    }

    #endregion Methods
}
