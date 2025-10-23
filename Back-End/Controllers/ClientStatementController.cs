using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientStatementController : ControllerBase
{
    private readonly IStatementRetrievalService _service;

    public ClientStatementController(IStatementRetrievalService service)
    {
        _service = service;
    }

    [HttpGet("statement")]
    public async Task<IActionResult> GetDetails([FromQuery] string key, [FromQuery] string hash)
    {
        var result = await _service.GetCustomerDetailsAsync(key, hash);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions([FromQuery] string key, [FromQuery] string hash)
    {
        var result = await _service.GetCustomerTransactionsAsync(key, hash);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    // Added endpoint for statement totals
    [HttpGet("totals")]
    public async Task<IActionResult> GetStatementTotals([FromQuery] string key, [FromQuery] string hash)
    {
        var result = await _service.GetCustomerStatementTotalsAsync(key, hash);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}