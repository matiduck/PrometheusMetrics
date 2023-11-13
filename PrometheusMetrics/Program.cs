using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using PrometheusMetrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(options =>
    {
        options.AddService("PrometheusMetrics");
    })
    .WithMetrics(options => 
    {
        var metrics = new ServiceMetrics();
        builder.Services.AddSingleton(metrics);

        options
            .AddMeter(metrics.Name)
            .AddAspNetCoreInstrumentation()
            .AddPrometheusExporter();
    });

var app = builder.Build();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
