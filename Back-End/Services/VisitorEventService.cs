// Services/VisitorEventService.cs
using ClientStatementPortal.Interfaces;
using ClientStatementPortal.Models;
using Microsoft.EntityFrameworkCore;


namespace ClientStatementPortal.Services
{
    public class VisitorEventService : IVisitorEventService
    {
        private readonly DbMapperContext _context;
        private readonly IStatementRetrievalService _statementRetrievalService;

        public VisitorEventService(DbMapperContext context, IStatementRetrievalService statementRetrievalService)
        {
            _context = context;
            _statementRetrievalService = statementRetrievalService;
        }

        public async Task<ApiResponse<VisitorEventDto>> LogEventAsync(VisitorEventDto dto, HttpContext httpContext)
        {
            // Validate input
            if (dto == null)
                return ApiResponse<VisitorEventDto>.Fail("Input data is null", "InvalidData");

            if (string.IsNullOrWhiteSpace(dto.CompanyKey))
                return ApiResponse<VisitorEventDto>.Fail("CompanyKey is required", "ValidationError");

            if (string.IsNullOrWhiteSpace(dto.EventType))
                return ApiResponse<VisitorEventDto>.Fail("EventType is required", "ValidationError");

            // Lookup CompanyConnectionId based on CompanyKey
            var companyConnection = await _context.CompanyConnections
                .FirstOrDefaultAsync(cc => cc.CompanyKey == dto.CompanyKey);

            int? companyConnectionId = companyConnection?.Id;

            // Map DTO to entity
            var entity = new VisitorEvent
            {
                EventType = dto.EventType,
                CompanyKey = dto.CompanyKey,
                AccountType = dto.AccountType,
                AccountName = dto.AccountName,
                EventDate =  DateTime.UtcNow,
                IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = httpContext.Request.Headers["User-Agent"].ToString(),
                CompanyConnectionId = companyConnectionId
            };

            _context.VisitorEvents.Add(entity);
            await _context.SaveChangesAsync();

            return ApiResponse<VisitorEventDto>.Ok(dto, "Event logged successfully");
        }
    }
}