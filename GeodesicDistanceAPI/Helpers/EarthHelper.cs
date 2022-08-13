using FluentValidation;
using GeodesicDistanceAPI.Enums;
using GeodesicDistanceAPI.Models;
using GeodesicDistanceAPI.Validators;
using System.Globalization;

namespace GeodesicDistanceAPI.Helpers;

public class EarthHelper
{
    private readonly double _earthRadius = 6371; /* in Km */
    private readonly IValidator<string> _regionValidator = new RegionValidator();

    public Distance CalculateDistance(PathRequest pathRequest)
    {
        switch (pathRequest.CalculationType)
        {
            case CalculationType.Approximate:
                return CalculateApproximateDistance(pathRequest);
            case CalculationType.Actual:
            /*
             * For the sake of simplicity I won't implement this.
             * In the end general structure won't change either way.
             * -ms. 2022-08-13
             */
            default:
                throw new NotImplementedException();
        }
    }

    private Distance CalculateApproximateDistance(PathRequest pathRequest)
    {
        /* Source of the Haversine formula: https://stackoverflow.com/a/45038000/4438125 */
        var dLat = ToRadians(pathRequest.End.Latitude - pathRequest.Start.Latitude);
        var dLon = ToRadians(pathRequest.End.Longitude - pathRequest.Start.Longitude);
        var lat1 = ToRadians(pathRequest.Start.Latitude);
        var lat2 = ToRadians(pathRequest.End.Latitude);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        var c = 2 * Math.Asin(Math.Sqrt(a));
        var d = _earthRadius * c;

        var regionInfo = new RegionInfo(pathRequest.Region);
        if (!regionInfo.IsMetric)
        {
            d = ToImperial(d);
        }

        var result = new Distance
        {
            Value = d,
            Unit = GetUnit(pathRequest.Region),
        };

        return result;
    }

    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }

    private static double ToImperial(double distance)
    {
        return distance / 1.609; /* Approximate */
    }

    private string GetUnit(string region)
    {
        var unit = "km"; /* I'll use km as default */

        if (!_regionValidator.Validate(region).IsValid)
            return unit;

        var regionInfo = new RegionInfo(region);
        if (regionInfo.IsMetric)
            return unit;

        unit = "mi";
        return unit;
    }
}
