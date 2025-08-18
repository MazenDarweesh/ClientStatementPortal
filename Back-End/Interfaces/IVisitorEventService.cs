using ClientStatementPortal.Models;

namespace ClientStatementPortal.Interfaces
{
   
    public interface IVisitorEventService
    {
        Task<ApiResponse<VisitorEventDto>> LogEventAsync(VisitorEventDto dto, HttpContext httpContext);
    }
}
