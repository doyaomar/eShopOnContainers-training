using Catalog.API.Bootsrap;
using Catalog.API.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = ApiVersion.Default;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
// Add automapper profile
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });
});
// Add user services
builder.Services.AddUserServices();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCustomHealhChecks(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerUI(options => 
    // {
    //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1");
    //     options.RoutePrefix = string.Empty;
    // });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();