﻿namespace Service.Domain.Tickets.Models;

public record class TicketTypeListViewModel : IListViewModel
{
    public int Id { get; init; }

    public string Type { get; init; }

    public DateTime CreatedOn { get; init; }
}
