using FluentValidation;
using System.Globalization;

namespace GeodesicDistanceAPI.Validators;

public class RegionValidator : AbstractValidator<string>
{
    private static List<string>? _validCultures;

    private static bool IsValidRegion(string isoCountryCode)
    {
        if (_validCultures == null)
        {
            /* Based on: https://stackoverflow.com/a/45307472/4438125 */
            _validCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(x => !x.Equals(CultureInfo.InvariantCulture)) //Remove the invariant culture as a region cannot be created from it.
                .Where(x => !x.IsNeutralCulture) //Remove nuetral cultures as a region cannot be created from them.
                .Select(x => x.Name.ToUpper()).ToList();
        }

        return _validCultures.Contains(isoCountryCode.ToUpper());
    }

    public RegionValidator()
    {
        RuleFor(x => x).Custom((x, context) =>
        {
            if (IsValidRegion(x)) return;

            const string error = "The region name en should not correspond to neutral culture; a specific culture name is required.";
            context.AddFailure(error);
        });
    }
}

