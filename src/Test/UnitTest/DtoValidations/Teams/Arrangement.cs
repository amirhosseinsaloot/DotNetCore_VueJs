using Service.DomainDto.Team;

namespace UnitTest.DtoValidations.Teams;

public class Arrangement
{
    public TeamCreateUpdateDto ValidTeamCreateUpdateDto => new()
    {
        Name = "Manhattan",
        Description = "Manhattan Team of Health",
        ParentId = null
    };
}
