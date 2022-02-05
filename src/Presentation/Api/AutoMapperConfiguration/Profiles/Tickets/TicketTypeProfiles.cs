using AutoMapper;
using Data.Entities.Tickets;
using Service.DomainDto.Ticket;

namespace Api.AutoMapperConfiguration.Profiles.Tickets;

public class TicketTypeProfiles : Profile
{
    public TicketTypeProfiles()
    {
        CreateMap<TicketTypeCreateUpdateDto, TicketType>()
            .ForMember(dest => dest.Id, src => src.Ignore());
        CreateMap<TicketType, TicketTypeDto>();
        CreateMap<TicketType, TicketTypeListDto>();
    }
}