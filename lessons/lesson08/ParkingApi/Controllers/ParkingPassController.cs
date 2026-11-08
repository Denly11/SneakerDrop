
using Microsoft.AspNetCore.Mvc;
using ParkingApi.Dtos;
using ParkingApi.Entities;
using ParkingApi.Exceptions;
using ParkingApi.Services;

namespace ParkingApi.Controllers;

[ApiController]
[Route("api/[controller]")]//to controller v [] je jenmom zástupný symbol, který se nahradí názvem controlleru, v tomto případě ParkingPass
public class ParkingPassController : ControllerBase
{
    private readonly ParkingPassService _parkingPassService;

    public ParkingPassController(ParkingPassService parkingPassService)
    {
        _parkingPassService = parkingPassService;
    }

    [HttpPost]
    public IActionResult CreateParkingPass([FromBody] CreateParkingPassRequest req)
    {
        try
        {
            var result = _parkingPassService.CreateParkingPass(req);

            if (result is null)
            {
                return NotFound(new { error = $"Parking lot {req.ParkingLotId} was not found." });
            }

            return StatusCode(201, result);
        }
        catch (HandledException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpGet]//je tu parkingpassresponse protož eto má být nějak oddělené
    public ActionResult<List<ParkingPassResponse>> Search([FromQuery] int? parkingLotId,[FromQuery] string? licensePlate, [FromQuery] DateOnly? validOn)
    {
        List<ParkingPassResponse> SearchedPasses = new List<ParkingPassResponse>();

        SearchedPasses = _parkingPassService.Search(parkingLotId, licensePlate, validOn);

        return Ok(SearchedPasses);
    }
    


}