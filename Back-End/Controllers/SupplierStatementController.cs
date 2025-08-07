using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetStatement([FromQuery] string companyKey, [FromQuery] string hash)
        {
            var result = await _service.GetSupplierStatementAsync(companyKey, hash);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions([FromQuery] string companyKey, [FromQuery] string hash)
        {
            var result = await _service.GetSupplierTransactionsAsync(companyKey, hash);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
