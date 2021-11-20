using Services.Domain;

namespace UnitTest.ViewModelValidations.Teams;

public class Arrangement
{
    public TeamCreateUpdateViewModel ValidTeamCreateUpdateViewModel => new()
    {
        Name = "Manhattan",
        Description = "Manhattan Team of Health",
        ParentId = null
    };
}
