using GeodesicDistanceAPI.Enums;

namespace GeodesicDistanceAPI.Models;

public class PathRequest
{
    public Coordinate Start { get; set; }

    public Coordinate End { get; set; }

    public string Region { get; set; } = "en-us";

    public CalculationType CalculationType { get; set; } = CalculationType.Approximate;
}
