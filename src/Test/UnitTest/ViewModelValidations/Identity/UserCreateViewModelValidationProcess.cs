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
