using Service.DomainDto.User;

#nullable disable

namespace UnitTest.DtoValidations.Identity;

public class ValidUserDto : TheoryData<UserCreateDto>
{
    public ValidUserDto()
    {
        var validModel = new Arrangement().ValidUserCreateDto;
        Add(validModel);
    }
}

public class NotValidUserCreateDto : TheoryData<UserCreateDto>
{
    public NotValidUserCreateDto()
    {
        var validModel = new Arrangement().ValidUserCreateDto;

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

        // Password
        Add(validModel with { Password = "" });


        Add(validModel with { Password = null });


        Add(validModel with { Password = "12345" });

        Add(validModel with { Password = new string('0', 128) });

        // PhoneNumber
        Add(validModel with { PhoneNumber = new string('0', 16) });

        Add(validModel with { PhoneNumber = "12345 3 " });

        // Gender
        Add(validModel with { Gender = (GenderType)3 });

        // TeamId
        Add(validModel with { TeamId = 0 });
    }
}

public class UserCreateDtoValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidUserDto))]
    public void ValidUserCreateDto(UserCreateDto dto)
    {
        //Arrange
        var validator = new UserCreateDtoValidator();

        //Act & Assert
        validator.Validate(dto).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidUserCreateDto))]
    public void NotValidUserCreateDto(UserCreateDto dto)
    {
        //Arrange
        var validator = new UserCreateDtoValidator();

        //Act & Assert
        validator.Validate(dto).IsValid.Should().BeFalse();
    }
}
