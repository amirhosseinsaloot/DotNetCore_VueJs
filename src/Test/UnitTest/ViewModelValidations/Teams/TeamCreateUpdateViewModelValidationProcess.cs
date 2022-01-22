using Service.Domain.Teams.Models;

namespace UnitTest.ViewModelValidations.Teams;

public class ValidTeamCreateUpdateViewModel : TheoryData<TeamCreateUpdateViewModel>
{
    public ValidTeamCreateUpdateViewModel()
    {
        var validModel = new Arrangement().ValidTeamCreateUpdateViewModel;
        Add(validModel);
    }
}

public class NotValidTeamCreateUpdateViewModel : TheoryData<TeamCreateUpdateViewModel>
{
    public NotValidTeamCreateUpdateViewModel()
    {
        var validModel = new Arrangement().ValidTeamCreateUpdateViewModel;

        // Name
        Add(validModel with { Name = "" });

        Add(validModel with { Name = null });

        Add(validModel with { Name = new string('0', 51) });

        // Description
        Add(validModel with { Description = "" });

        Add(validModel with { Description = null });

        Add(validModel with { Description = new string('0', 101) });

        // ParentId
        Add(validModel with { ParentId = 0 });
    }
}

public class TeamCreateUpdateViewModelValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidTeamCreateUpdateViewModel))]
    public void ValidTeamCreateUpdateViewModel(TeamCreateUpdateViewModel viewModel)
    {
        //Arrange
        var validator = new TeamCreateUpdateViewModelValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidTeamCreateUpdateViewModel))]
    public void NotValidTeamCreateUpdateViewModel(TeamCreateUpdateViewModel viewModel)
    {
        //Arrange
        var validator = new TeamCreateUpdateViewModelValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeFalse();
    }
}
