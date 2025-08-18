// Controllers/VisitorEventController.cs
using ClientStatementPortal.DTOs;
using ClientStatementPortal.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientStatementPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorEventController : ControllerBase
    {
        private readonly IVisitorEventService _visitorEventService;

        public VisitorEventController(IVisitorEventService visitorEventService)
        {
            _visitorEventService = visitorEventService;
        }

        [HttpPost("log")]
        public async Task<IActionResult> LogVisitorEvent([FromBody] VisitorEventDto dto)
        {
            var result = await _visitorEventService.LogEventAsync(dto, HttpContext);
            return Ok(result);
        }
    }
}