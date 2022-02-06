using Api.Dtos.Team;

#nullable disable

namespace UnitTest.DtoValidations.Teams;
public class ValidTeamCreateUpdateDto : TheoryData<TeamCreateUpdateDto>
{
    public ValidTeamCreateUpdateDto()
    {
        var validModel = new Arrangement().ValidTeamCreateUpdateDto;
        Add(validModel);
    }
}

public class NotValidTeamCreateUpdateDto : TheoryData<TeamCreateUpdateDto>
{
    public NotValidTeamCreateUpdateDto()
    {
        var validModel = new Arrangement().ValidTeamCreateUpdateDto;

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

public class TeamCreateUpdateDtoValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidTeamCreateUpdateDto))]
    public void ValidTeamCreateUpdateDto(TeamCreateUpdateDto dto)
    {
        //Arrange
        var validator = new TeamCreateUpdateDtoValidator();

        //Act & Assert
        validator.Validate(dto).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidTeamCreateUpdateDto))]
    public void NotValidTeamCreateUpdateViewModel(TeamCreateUpdateDto dto)
    {
        //Arrange
        var validator = new TeamCreateUpdateDtoValidator();

        //Act & Assert
        validator.Validate(dto).IsValid.Should().BeFalse();
    }
}
