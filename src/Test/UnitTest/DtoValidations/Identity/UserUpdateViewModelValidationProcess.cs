using Api.Dtos.User;

#nullable disable

namespace UnitTest.DtoValidations.Identity;

public class ValidUserUpdateDto : TheoryData<UserUpdateDto>
{
    public ValidUserUpdateDto()
    {
        var validModel = new Arrangement().ValidUserUpdateDto;
        Add(validModel);
    }
}

public class NotValidUserUpdateDto : TheoryData<UserUpdateDto>
{
    public NotValidUserUpdateDto()
    {
        var validModel = new Arrangement().ValidUserUpdateDto;

        // Username
        Add(validModel with { Username = "" });


        Add(validModel with { Username = null });


        Add(validModel with { Username = new string('0', 41) });

        // Firstname
        Add(validModel with { Firstname = "" });


        Add(validModel with { Firstname = null });


        Add(validModel with { Firstname = new string('0', 36) });

        // Lastname
        Add(validModel with { Lastname = "" });


        Add(validModel with { Lastname = null });


        Add(validModel with { Lastname = new string('0', 36) });

        // Email
        Add(validModel with { Email = "" });


        Add(validModel with { Email = null });


        Add(validModel with { Email = "AbCC@" });

        Add(validModel with { Email = new string('0', 321) });

        // PhoneNumber
        Add(validModel with { PhoneNumber = new string('0', 16) });

        Add(validModel with { PhoneNumber = "12345 3 " });

        // Gender
        Add(validModel with { Gender = (GenderType)3 });

        // TeamId
        Add(validModel with { TeamId = 0 });
    }
}

public class UserUpdateDtoValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidUserUpdateDto))]
    public void ValidUserUpdateDto(UserUpdateDto dto)
    {
        //Arrange
        var validator = new UserUpdateDtoValidator();

        //Act & Assert
        validator.Validate(dto).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidUserUpdateDto))]
    public void NotValidUserUpdateDto(UserUpdateDto dto)
    {
        //Arrange
        var validator = new UserUpdateDtoValidator();

        //Act & Assert
        validator.Validate(dto).IsValid.Should().BeFalse();
    }
}
