using Bira.App.SchoolManager.Api.Configuration;
using Bira.App.SchoolManager.Infra.Repositories.BaseContext;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var assembly = AppDomain.CurrentDomain.Load("Bira.App.SchoolManager.Application");

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// ConfigureServices
builder.Services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("MemoryDb"));

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddMediatR(assembly);

builder.Services.AddAutoMapper(assembly);

builder.Services.AddApiConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfig();

builder.Services.ResolveDependencies();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure
app.UseApiConfig(app.Environment);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.MapControllers();

app.Run();