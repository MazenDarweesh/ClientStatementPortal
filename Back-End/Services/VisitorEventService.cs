// Services/VisitorEventService.cs
using ClientStatementPortal.DTOs;
using ClientStatementPortal.Interfaces;
using ClientStatementPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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

            // Fill IP if not provided
            if (string.IsNullOrWhiteSpace(dto.IpAddress))
                dto.IpAddress = httpContext.Connection.RemoteIpAddress?.ToString();

            // Fill UserAgent if not provided
            if (string.IsNullOrWhiteSpace(dto.UserAgent) && httpContext.Request.Headers.ContainsKey("User-Agent"))
                dto.UserAgent = httpContext.Request.Headers["User-Agent"].ToString();

            // Lookup CompanyConnectionId based on CompanyKey
            var companyConnection = await _context.CompanyConnections
                .FirstOrDefaultAsync(cc => cc.CompanyKey == dto.CompanyKey);

            int? companyConnectionId = companyConnection?.Id;

            // Map DTO to entity
            var entity = new VisitorEvent
            {
                EventType = dto.EventType,
                CompanyKey = dto.CompanyKey,
                IpAddress = dto.IpAddress,
                UserAgent = dto.UserAgent,
                EventDate = dto.EventDate ?? DateTime.UtcNow,
                AccountName = dto.AccountName,
                AccountType = dto.AccountType,
                CompanyConnectionId = companyConnectionId
            };

            _context.VisitorEvents.Add(entity);
            await _context.SaveChangesAsync();

            return ApiResponse<VisitorEventDto>.Ok(dto, "Event logged successfully");
        }
    }
}