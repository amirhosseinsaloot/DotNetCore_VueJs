using Service.Domain.Roles.Models;

namespace UnitTest.ViewModelValidations.Identity;

public class ValidRoleCreateUpdateViewModel : TheoryData<RoleCreateUpdateViewModel>
{
    public ValidRoleCreateUpdateViewModel()
    {
        var validModel = new Arrangement().ValidRoleCreateUpdateViewModel;
        Add(validModel);
    }
}

public class NotValidRoleCreateUpdateViewModel : TheoryData<RoleCreateUpdateViewModel>
{
    public NotValidRoleCreateUpdateViewModel()
    {
        var validModel = new Arrangement().ValidRoleCreateUpdateViewModel;

        // Name
        Add(validModel with { Name = "" });

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Add(validModel with { Name = null });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Add(validModel with { Name = new string('0', 16) });

        // Description
        Add(validModel with { Description = "" });

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Add(validModel with { Description = null });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        Add(validModel with { Description = new string('0', 101) });
    }
}

public class RoleCreateUpdateViewModelValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidRoleCreateUpdateViewModel))]
    public void ValidRoleCreateUpdateViewModel(RoleCreateUpdateViewModel viewModel)
    {
        //Arrange
        var validator = new RoleCreateUpdateViewModelValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidRoleCreateUpdateViewModel))]
    public void NotValidRoleCreateUpdateViewModel(RoleCreateUpdateViewModel viewModel)
    {
        //Arrange
        var validator = new RoleCreateUpdateViewModelValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeFalse();
    }
}
