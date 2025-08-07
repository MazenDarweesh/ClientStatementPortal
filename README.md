# Client Statement Portal

A .NET 9 Web API for retrieving client account statements from multiple company databases using stored procedures.

## Project Structure

```
ClientStatementPortal  
├── Controllers  
│   └── ClientStatementController.cs  
		SupplierStatementController.cs
├── Services  
	IStatementRetrievalService
│   └── StatementRetrievalService.cs  
├── Interfaces  
│   └── IStatementRetrievalService.cs  
├── Helpers  
│   └── StoredProcedureRunner.cs  
├── Models  
│   └── CompanyConnection.cs  
├── DTOs  
│   ├── ClientAccountStatementDto.cs  
│   └── AccountTransactionDto.cs  
		SupplierAccountStatementDto
├── Data  
│   └── DbMapperContext.cs  
├── Responses  
│   └── ApiResponse.cs  
├── appsettings.json  
├── Program.cs  
├── ClientStatementPortal.csproj  
└── README.md
```

## Setup

1. Update `appsettings.json` with your SQL Server connection string.
2. Run `dotnet restore` to install dependencies.
3. Run `dotnet ef database update` to apply migrations (if any).
4. Start the API with `dotnet run`.

## Endpoints

- `GET /api/ClientStatement/statement?companyKey=...&clientId=...&companyId=...`

Returns the client account statement for the specified company and client.
