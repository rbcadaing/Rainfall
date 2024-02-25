using Rainfall.Service;

var builder = WebApplication.CreateBuilder(args);

var rainfallBaseAddress = builder.Configuration.GetValue<string>("RainfallApiBaseAddress");
// Add services to the container.

builder.Services.AddScoped<IEnvironmentDataService, EnvironmentDataService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
