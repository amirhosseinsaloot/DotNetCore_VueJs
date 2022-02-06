using Api.Dtos.Ticket;
using AutoMapper;
using Core.Entities.Tickets;

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