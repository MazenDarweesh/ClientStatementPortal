using Microsoft.AspNetCore.Mvc;

namespace ClientStatementPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierStatementController : ControllerBase
    {
        private readonly IStatementRetrievalService _service;

        public SupplierStatementController(IStatementRetrievalService service)
        {
            _service = service;
        }

        [HttpGet("statement")]
        public async Task<IActionResult> GetDetails([FromQuery] string key, [FromQuery] string hash)
        {
            var result = await _service.GetSupplierDetailsAsync(key, hash);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions([FromQuery] string key, [FromQuery] string hash)
        {
            var result = await _service.GetSupplierTransactionsAsync(key, hash);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        // Added endpoint for statement totals
        [HttpGet("totals")]
        public async Task<IActionResult> GetStatementTotals([FromQuery] string key, [FromQuery] string hash)
        {
            var result = await _service.GetSupplierStatementTotalsAsync(key, hash);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
