using Microsoft.AspNetCore.Mvc;
using ParkingApi.Dtos;
using ParkingApi.Exceptions;
using ParkingApi.Services;

namespace ParkingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingLotsController : ControllerBase
{
    private readonly ParkingLotService _parkingLotService;

    public ParkingLotsController(ParkingLotService parkingLotService)
    {
        _parkingLotService = parkingLotService;
    }

    [HttpPost]
    public IActionResult CreateParkingLot([FromBody] ParkingLotRequest req)
    {
        try
        {
            var result = _parkingLotService.CreateParkingLot(req);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (HandledException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _parkingLotService.GetParkingLotById(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] ParkingLotRequest req)
    {
        try
        {
            var result = _parkingLotService.UpdateParkingLot(id, req);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (HandledException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpPost("{id:int}/spots")]
    public IActionResult CreateParkingSpot(int id, [FromBody] CreateParkingSpotRequest req)
    {
        try
        {
            var result = _parkingLotService.CreateParkingSpot(id, req);

            if (result is null)
            {
                return NotFound();
            }

            return StatusCode(201, result);
        }
        catch (HandledException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }
}
