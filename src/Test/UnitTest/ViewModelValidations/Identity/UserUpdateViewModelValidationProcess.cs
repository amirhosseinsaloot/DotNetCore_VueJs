using Service.Domain.Users.Models;

#nullable disable

namespace UnitTest.ViewModelValidations.Identity;

public class ValidUserUpdateViewModel : TheoryData<UserUpdateViewModel>
{
    public ValidUserUpdateViewModel()
    {
        var validModel = new Arrangement().ValidUserUpdateViewModel;
        Add(validModel);
    }
}

public class NotValidUserUpdateViewModel : TheoryData<UserUpdateViewModel>
{
    public NotValidUserUpdateViewModel()
    {
        var validModel = new Arrangement().ValidUserUpdateViewModel;

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

public class UserUpdateViewModelValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidUserUpdateViewModel))]
    public void ValidUserUpdateViewModel(UserUpdateViewModel viewModel)
    {
        //Arrange
        var validator = new UserUpdateViewModelValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidUserUpdateViewModel))]
    public void NotValidUserUpdateViewModel(UserUpdateViewModel viewModel)
    {
        //Arrange
        var validator = new UserUpdateViewModelValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeFalse();
    }
}
