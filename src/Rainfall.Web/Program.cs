
using Rainfall.Core.Common;
using Rainfall.Service;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var rainfallBaseAddress = builder.Configuration.GetValue<string>("RainfallApiBaseAddress");
// Add services to the container.

builder.Services.AddScoped<IEnvironmentDataService, EnvironmentDataService>();
//builder.Services.AddScoped<IValidator<RainFallRequestDto>, RainFallQueryDtoValidator>()
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.DocumentFilter<DescriptionsDocumentFilter>();

    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Rainfall Api",
            Version = "1.0",
            Contact = new OpenApiContact
            {
                Name = "Sorted",
                Url = new Uri("https://www.sorted.com")
            },
            Description = "An API which provides rainfall reading data",
           
        });
    c.AddServer(new OpenApiServer { Url = "https://localhost:7259/", Description = "Rainfall Api" });
});

builder.Services.AddHttpClient("RainfallApi", httpClient =>
{
    httpClient.BaseAddress = new Uri(rainfallBaseAddress ?? "");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
