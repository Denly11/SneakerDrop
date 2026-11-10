using Microsoft.AspNetCore.Mvc;
using SneakerDrop.Services;
namespace SneakerDrop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DropController : ControllerBase
{
    private readonly SpotService _spotService;

    public DropController(SpotService spotService)
    {
        _spotService = spotService;
    }

    public SpotControler(SpotService spotService)
    {
        _spotService = spotService;
    }
}
