using GeodesicDistanceAPI.Enums;
using GeodesicDistanceAPI.Models;
using System.Collections;
using GeodesicDistanceAPI.Tests.Model;

namespace GeodesicDistanceAPI.Tests.Data;
internal class EarthHelperValidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new PathRequestTestModel()
            {
                CalculationType = CalculationType.Approximate,
                Region = "en-us",
                Start = new Coordinate()
                {
                    Latitude = 36.12,
                    Longitude = -86.67
                },
                End = new Coordinate()
                {
                    Latitude = 33.94,
                    Longitude = -118.4
                },
                ExpectedResult = new Distance
                {
                    Value = 1793.9368818135385,
                    Unit = "mi",
                }
            }
        };

        yield return new object[]
        {
            new PathRequestTestModel()
            {
                CalculationType = CalculationType.Approximate,
                Region = "en-us",
                Start = new Coordinate()
                {
                    Latitude = 53.297975,
                    Longitude = -6.372663
                },
                End = new Coordinate()
                {
                    Latitude = 41.385101,
                    Longitude = -81.440440
                },
                ExpectedResult = new Distance
                {
                    Value = 3440.856856598313,
                    Unit = "mi",
                }
            }
        };

        yield return new object[]
        {
            new PathRequestTestModel()
            {
                CalculationType = CalculationType.Approximate,
                Region = "tr-tr",
                Start = new Coordinate()
                {
                    Latitude = 36.12,
                    Longitude = -86.67
                },
                End = new Coordinate()
                {
                    Latitude = 33.94,
                    Longitude = -118.4
                },
                ExpectedResult = new Distance
                {
                    Value = 2886.4444428379834,
                    Unit = "km",
                }
            }
        };

        yield return new object[]
        {
            new PathRequestTestModel()
            {
                CalculationType = CalculationType.Approximate,
                Region = "tr-tr",
                Start = new Coordinate()
                {
                    Latitude = 53.297975,
                    Longitude = -6.372663
                },
                End = new Coordinate()
                {
                    Latitude = 41.385101,
                    Longitude = -81.440440
                },
                ExpectedResult = new Distance
                {
                    Value = 5536.338682266685,
                    Unit = "km",
                }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
