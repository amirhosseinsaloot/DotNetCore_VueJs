using Service.Domain.Users.Models;

namespace UnitTest.ViewModelValidations.Identity;

public class ValidUserCreateViewModel : TheoryData<UserCreateViewModel>
{
    public ValidUserCreateViewModel()
    {
        var validModel = new Arrangement().ValidUserCreateViewModel;
        Add(validModel);
    }
}

public class NotValidUserCreateViewModel : TheoryData<UserCreateViewModel>
{
    public NotValidUserCreateViewModel()
    {
        var validModel = new Arrangement().ValidUserCreateViewModel;

        // Username
        Add(validModel with { Username = "" });

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Add(validModel with { Username = null });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Add(validModel with { Username = new string('0', 41) });

        // Firstname
        Add(validModel with { Firstname = "" });

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Add(validModel with { Firstname = null });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Add(validModel with { Firstname = new string('0', 36) });

        // Lastname
        Add(validModel with { Lastname = "" });

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Add(validModel with { Lastname = null });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Add(validModel with { Lastname = new string('0', 36) });

        // Email
        Add(validModel with { Email = "" });

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Add(validModel with { Email = null });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Add(validModel with { Email = "AbCC@" });

        Add(validModel with { Email = new string('0', 321) });

        // Password
        Add(validModel with { Password = "" });

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Add(validModel with { Password = null });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

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

public class UserCreateViewModelValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidUserCreateViewModel))]
    public void ValidUserCreateViewModel(UserCreateViewModel viewModel)
    {
        //Arrange
        var validator = new UserCreateViewModelValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidUserCreateViewModel))]
    public void NotValidUserCreateViewModel(UserCreateViewModel viewModel)
    {
        //Arrange
        var validator = new UserCreateViewModelValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeFalse();
    }
}
