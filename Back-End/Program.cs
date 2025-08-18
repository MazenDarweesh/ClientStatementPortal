using ClientStatementPortal.Interfaces;
using ClientStatementPortal.Models;
using ClientStatementPortal.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDev", cors =>
        cors.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});
builder.Services.AddControllers().AddJsonOptions(o =>
    o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);

//var AllowSpecificOrigins = "_AllowSpecificOrigins";
//builder.Services.AddCors(optinos => {
//    optinos.AddPolicy(name: AllowSpecificOrigins, policy =>
//    {
//        policy.WithOrigins("http://localhost:4200")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});
// Register DbContext
builder.Services.AddDbContext<DbMapperContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IStatementRetrievalService, StatementRetrievalService>();
builder.Services.AddScoped<IVisitorEventService, VisitorEventService>();
builder.Services.AddScoped<StoredProcedureRunner>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("LocalDev");
app.MapControllers();

app.Run();

