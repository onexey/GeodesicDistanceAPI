using FluentValidation;
using GeodesicDistanceAPI.Helpers;
using GeodesicDistanceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeodesicDistanceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DistanceController : ControllerBase
{
    private readonly ILogger<DistanceController> _logger;
    private readonly EarthHelper _earthHelper;
    private readonly IValidator<PathRequest> _validator;

    public DistanceController(ILogger<DistanceController> logger,
        EarthHelper earthHelper,
        IValidator<PathRequest> validator)
    {
        _logger = logger;
        _earthHelper = earthHelper;
        _validator = validator;
    }

    [HttpPost(Name = "Distance")]
    public IActionResult GetDistance([FromBody] PathRequest pathRequest)
    {
        var validationResult = _validator.Validate(pathRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = _earthHelper.CalculateDistance(pathRequest);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, null);
            return BadRequest(e.Message);
        }
    }
}
