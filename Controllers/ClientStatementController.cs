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
    public async Task<IActionResult> GetStatement([FromQuery] string companyKey, [FromQuery] string hash)
    {
        var result = await _service.GetCustomerStatementAsync(companyKey, hash);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions([FromQuery] string companyKey, [FromQuery] string hash)
    {
        var result = await _service.GetCustomerTransactionsAsync(companyKey, hash);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}