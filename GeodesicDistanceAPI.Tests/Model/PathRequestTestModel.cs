using GeodesicDistanceAPI.Models;

namespace GeodesicDistanceAPI.Tests.Model;
public class PathRequestTestModel : PathRequest
{
    private PathRequest? _underlyingData;

    public Distance ExpectedResult { get; set; }

    public PathRequest UnderlyingData
    {
        get
        {
            if (_underlyingData != null)
                return _underlyingData;

            _underlyingData = new PathRequest
            {
                CalculationType = this.CalculationType,
                End = this.End,
                Region = this.Region,
                Start = this.Start
            };

            return _underlyingData;
        }
    }
}
