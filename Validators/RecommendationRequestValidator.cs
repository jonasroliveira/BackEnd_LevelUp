using BackEnd_LevelUp.DTOs;
using FluentValidation;

namespace BackEnd_LevelUp.validators;

public class RecommendationRequestValidator : AbstractValidator<RecommendationRequestDto>
{
    public RecommendationRequestValidator()
    {
        RuleFor(x => x.Genres)
            .NotNull()
            .Must(list => list != null && list.Count >= 1)
            .WithMessage("At least one gender is required.");

        RuleFor(x => x.Platform)
            .Must(p => p == null || p == "pc" || p == "browser" || p == "both")
            .WithMessage("Platform it must be 'pc', 'browser' ou 'both'.");

        RuleFor(x => x.MinRamGb)
           .GreaterThan(0)
           .When(x => x.MinRamGb.HasValue)
           .WithMessage("MinRamGb must be greater than zero.");
    }
}