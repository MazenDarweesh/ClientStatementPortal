using ClientStatementPortal.Interfaces;
using ClientStatementPortal.Models;
using ClientStatementPortal.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers().AddJsonOptions(o =>
    o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDev", cors =>
        cors.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// Register DbContext
builder.Services.AddDbContext<DbMapperContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IStatementRetrievalService, StatementRetrievalService>();
builder.Services.AddScoped<IVisitorEventService, VisitorEventService>();
builder.Services.AddScoped<StoredProcedureRunner>();

var app = builder.Build();

// Global exception handler that customizes the status code based on exception type.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exceptionHandlerFeature != null)
        {
            var exception = exceptionHandlerFeature.Error;
            int statusCode;
            string errorMessage;

            switch (exception)
            {
                case ArgumentNullException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = "A required value is missing.";
                    break;
                case KeyNotFoundException _:
                    statusCode = (int)HttpStatusCode.NotFound;
                    errorMessage = "Resource not found.";
                    break;
                case UnauthorizedAccessException _:
                    statusCode = (int)HttpStatusCode.Forbidden;
                    errorMessage = "Access denied.";
                    break;
                // Add additional cases as needed.
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = "Internal server error. Please try again later.";
                    break;
            }

            // In development, return the real exception message.
            if (app.Environment.IsDevelopment())
            {
                errorMessage = exception.Message;
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
        }
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles(); // Optional: يخلي index.html يشتغل تلقائياً
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot")
    ),
    RequestPath = ""
});
app.UseHttpsRedirection();
app.UseCors("LocalDev");
app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Fallback to Angular index.html for SPA routes
app.MapFallbackToFile(Path.Combine("index.html"));


app.Run();

