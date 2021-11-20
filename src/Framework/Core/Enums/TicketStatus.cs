namespace Core.Enums;

public enum TicketStatus : byte
{
    New = 1,

    Open = 2,

    OnHold = 4,

    Solved = 8,

    Closed = 16,

    Assigned = 32
}
