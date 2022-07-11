namespace Domain.Enums;

public enum TicketStatus : byte
{
    New = 1,

    Open = 2,

    OnHold = 3,

    Solved = 4,

    Closed = 5,

    Assigned = 6
}
