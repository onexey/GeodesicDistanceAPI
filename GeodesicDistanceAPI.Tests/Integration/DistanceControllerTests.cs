using Bogus;
using GeodesicDistanceAPI.Controllers;
using GeodesicDistanceAPI.Helpers;
using GeodesicDistanceAPI.Models;
using GeodesicDistanceAPI.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace GeodesicDistanceAPI.Tests.Integration;
public class DistanceControllerTests
{
    private readonly DistanceController _controller;

    public DistanceControllerTests()
    {
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        var httpContext = new DefaultHttpContext();
        mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

        var earthHelper = new EarthHelper();
        var validator = new PathRequestValidator();
        var logger = new NullLogger<DistanceController>();

        _controller = new DistanceController(logger, earthHelper, validator);
    }

    [Fact]
    public void Post_WithValidData_ShouldReturnSuccessWithValue()
    {
        // Arrange
        var coordinateFaker = new Faker<Coordinate>()
            .RuleFor(c => c.Latitude, f => f.Random.Double(-90, 90))
            .RuleFor(c => c.Longitude, f => f.Random.Double(-180, 180))
            .RuleFor(c => c.Latitude, f => f.Random.Double(-90, 90))
            .RuleFor(c => c.Longitude, f => f.Random.Double(-180, 180));

        var faker = new Faker<PathRequest>()
            .RuleFor(d => d.Start, coordinateFaker)
            .RuleFor(d => d.End, coordinateFaker);
        var pathRequest = faker.Generate();

        // Act
        var getResult = _controller.GetDistance(pathRequest);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(getResult);
        Assert.Equal(200, okObjectResult.StatusCode);
        Assert.NotNull(okObjectResult.Value);
        var dtoResult = Assert.IsType<Distance>(okObjectResult.Value);
        Assert.NotNull(dtoResult.Unit);
    }

    [Fact]
    public void Post_WithWrongLocale_ShouldReturnBadRequest()
    {
        // Arrange
        var coordinateFaker = new Faker<Coordinate>()
            .RuleFor(c => c.Latitude, f => f.Random.Double(-90, 90))
            .RuleFor(c => c.Longitude, f => f.Random.Double(-180, 180))
            .RuleFor(c => c.Latitude, f => f.Random.Double(-90, 90))
            .RuleFor(c => c.Longitude, f => f.Random.Double(-180, 180));

        var faker = new Faker<PathRequest>()
            .RuleFor(d => d.Start, coordinateFaker)
            .RuleFor(d => d.End, coordinateFaker)
            .RuleFor(d => d.Region, f => f.Random.String());
        var pathRequest = faker.Generate();

        // Act
        var getResult = _controller.GetDistance(pathRequest);

        // Assert
        _ = Assert.IsType<BadRequestObjectResult>(getResult);
    }
}
