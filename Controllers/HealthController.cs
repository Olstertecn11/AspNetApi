using LaCazuelaChapinaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaCazuelaChapinaAPI.Controllers{

  [Route("api/[controller]")]
  [ApiController]
  public class HealthController : ControllerBase
  {
    private readonly DatabaseService _databaseService;

    public HealthController(DatabaseService databaseService)
    {
      _databaseService = databaseService;
    }

    [HttpGet("check")]
    public async Task<IActionResult> CheckDatabaseConnection()
    {
      var (success, error) = await _databaseService.CheckDatabaseConnectionAsync();

      if (success)
      {
        return Ok(new { status = "Healthy" });
      }
      else
      {
        return StatusCode(500, new { status = "Unhealthy", error });
      }
    }
  }
}
