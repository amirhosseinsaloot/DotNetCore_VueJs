using Service.Identity.Models;

#nullable disable

namespace UnitTest.ViewModelValidations.Identity;

public class ValidTokenRequest : TheoryData<TokenRequest>
{
    public ValidTokenRequest()
    {
        var validModel = new Arrangement().ValidTokenRequest;

        Add(validModel with { GrantType = "PassWorD" });

        Add(validModel with { GrantType = "refreSH_tokeN" });
    }
}

public class NotValidTokenRequest : TheoryData<TokenRequest>
{
    public NotValidTokenRequest()
    {
        var validModel = new Arrangement().ValidTokenRequest;

        // GrantType
        Add(validModel with { GrantType = "" });

        Add(validModel with { GrantType = null });

        Add(validModel with { GrantType = new string('0', 16) });

        Add(validModel with { GrantType = "Test" });

        // Username
        Add(validModel with { Username = "" });

        Add(validModel with { Username = null });

        Add(validModel with { Username = new string('0', 41) });

        // Password
        Add(validModel with { Password = "" });

        Add(validModel with { Password = null });

        Add(validModel with { Password = "12345" });

        Add(validModel with { Password = new string('0', 128) });

        // RefreshToken
        Add(validModel with { RefreshToken = new string('0', 51) });
    }
}

public class TokenRequestViewModelValidationProcess
{
    [Theory]
    [ClassData(typeof(ValidTokenRequest))]
    public void ValidTokenRequest(TokenRequest viewModel)
    {
        //Arrange
        var validator = new TokenRequestValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NotValidTokenRequest))]
    public void NotValidTokenRequest(TokenRequest viewModel)
    {
        //Arrange
        var validator = new TokenRequestValidator();

        //Act & Assert
        validator.Validate(viewModel).IsValid.Should().BeFalse();
    }
}
