using Api.Dtos.Ticket;
using AutoMapper;
using Domain.Entities.Tickets;

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