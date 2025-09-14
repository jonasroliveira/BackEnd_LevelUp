using BackEnd_LevelUp.DTOs;
using BackEnd_LevelUp.validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BackEnd_LevelUp.Tests.Validators;

public class RecommendationRequestValidatorTest
{
    private readonly RecommendationRequestValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Genres_Is_Null_Or_Empty()
    {
        var dtoNull = new RecommendationRequestDto { Genres = null! };
        var resultNull = _validator.TestValidate(dtoNull);
        resultNull.ShouldHaveValidationErrorFor(x => x.Genres);

        var dtoEmpty = new RecommendationRequestDto { Genres = new List<string>() };
        var resultEmpty = _validator.TestValidate(dtoEmpty);
        resultEmpty.ShouldHaveValidationErrorFor(x => x.Genres);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Genres_Has_At_Least_One()
    {
        var dto = new RecommendationRequestDto { Genres = new List<string> { "Action" } };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.Genres);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("pc")]
    [InlineData("browser")]
    [InlineData("both")]
    public void Should_Not_Have_Error_For_Valid_Platform(string? platform)
    {
        var dto = new RecommendationRequestDto { Genres = new List<string> { "Action" }, Platform = platform };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.Platform);
    }

    [Theory]
    [InlineData("mobile")]
    [InlineData("console")]
    [InlineData("invalid")]
    public void Should_Have_Error_For_Invalid_Platform(string platform)
    {
        var dto = new RecommendationRequestDto { Genres = new List<string> { "Action" }, Platform = platform };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Platform);
    }

    [Fact]
    public void Should_Have_Error_When_MinRamGb_Is_Zero_Or_Negative()
    {
        var dtoZero = new RecommendationRequestDto { Genres = new List<string> { "Action" }, MinRamGb = 0 };
        var resultZero = _validator.TestValidate(dtoZero);
        resultZero.ShouldHaveValidationErrorFor(x => x.MinRamGb);

        var dtoNegative = new RecommendationRequestDto { Genres = new List<string> { "Action" }, MinRamGb = -2 };
        var resultNegative = _validator.TestValidate(dtoNegative);
        resultNegative.ShouldHaveValidationErrorFor(x => x.MinRamGb);
    }

    [Fact]
    public void Should_Not_Have_Error_When_MinRamGb_Is_Positive()
    {
        var dto = new RecommendationRequestDto { Genres = new List<string> { "Action" }, MinRamGb = 8 };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.MinRamGb);
    }

    [Fact]
    public void Should_Not_Have_Error_When_MinRamGb_Is_Null()
    {
        var dto = new RecommendationRequestDto { Genres = new List<string> { "Action" }, MinRamGb = null };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(x => x.MinRamGb);
    }
}