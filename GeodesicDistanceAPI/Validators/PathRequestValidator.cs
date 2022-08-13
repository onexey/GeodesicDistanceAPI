using FluentValidation;

namespace GeodesicDistanceAPI.Validators;

public class PathRequestValidator : AbstractValidator<Models.PathRequest>
{
    public PathRequestValidator()
    {
        RuleFor(x => x.Start).NotNull();
        RuleFor(x => x.End).NotNull();
        RuleFor(x => x.Region).SetValidator(new RegionValidator());
    }
}
