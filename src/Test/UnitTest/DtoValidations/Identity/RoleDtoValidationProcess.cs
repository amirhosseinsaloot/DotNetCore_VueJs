using Service.DomainDto.Role;

#nullable disable

namespace UnitTest.DtoValidations.Identity;

public class ValidRoleCreateUpdateDto : TheoryData<RoleCreateUpdateDto>
{
    public ValidRoleCreateUpdateDto()
    {
        var validModel = new Arrangement().ValidRoleCreateUpdateDto;
        Add(validModel);
    }
}

public class NotValidRoleCreateUpdateDto : TheoryData<RoleCreateUpdateDto>
{
    public NotValidRoleCreateUpdateDto()
    {
        var validModel = new Arrangement().ValidRoleCreateUpdateDto;

        // Name
        Add(validModel with { Name = "" });


        Add(validModel with { Name = null });


        Add(validModel with { Name = new string('0', 16) });

        // Description
        Add(validModel with { Description = "" });


        Add(validModel with { Description = null });


        Add(validModel with { Description = new string('0', 101) });
    }
}

public class RoleCreateUpdateDtoValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidRoleCreateUpdateDto))]
    public void ValidRoleCreateUpdateDto(RoleCreateUpdateDto dto)
    {
        //Arrange
        var validator = new RoleCreateUpdateDtoValidator();

        //Act & Assert
        validator.Validate(dto).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidRoleCreateUpdateDto))]
    public void NotValidRoleCreateUpdateDto(RoleCreateUpdateDto dto)
    {
        //Arrange
        var validator = new RoleCreateUpdateDtoValidator();

        //Act & Assert
        validator.Validate(dto).IsValid.Should().BeFalse();
    }
}
