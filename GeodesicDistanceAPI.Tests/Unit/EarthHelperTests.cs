using System.Globalization;
using GeodesicDistanceAPI.Helpers;
using GeodesicDistanceAPI.Models;
using GeodesicDistanceAPI.Tests.Data;
using GeodesicDistanceAPI.Tests.Model;
using Xunit.Abstractions;

namespace GeodesicDistanceAPI.Tests.Unit;
public class EarthHelperTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly EarthHelper _helper;

    public EarthHelperTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _helper = new EarthHelper();
    }

    [Theory]
    [ClassData(typeof(EarthHelperValidData))]
    public void WithValidDataShouldReturnValidResult(PathRequestTestModel pathRequest)
    {
        var calcResult = _helper.CalculateDistance(pathRequest.UnderlyingData);

        _outputHelper.WriteLine(calcResult.Value.ToString(CultureInfo.InvariantCulture));
        Assert.NotNull(calcResult);
        Assert.NotNull(calcResult.Unit);
        Assert.Equal(pathRequest.ExpectedResult.Unit, calcResult.Unit);
        Assert.Equal(pathRequest.ExpectedResult.Value, calcResult.Value);
    }
}
