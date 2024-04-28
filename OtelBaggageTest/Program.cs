using OtelBaggageTest;
using OpenTelemetry.Logs;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(mvcOptions =>
{
    mvcOptions.Filters.Add<HeaderToBaggageFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(builder =>
{
    builder.AddOpenTelemetry(options =>
    {
        options.AddConsoleExporter();
    });
});

builder.Services.AddOpenTelemetry()
    .WithMetrics(builder =>
    {
        builder.AddAspNetCoreInstrumentation();
        builder.AddConsoleExporter();
    })
    .WithTracing(builder =>
    {
        builder.AddAspNetCoreInstrumentation();
        builder.AddConsoleExporter();
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