using lab_03.BL.IRepositories;
using lab_03.BL.Services;
using lab_03.DA.dbContext;
using lab_03.DA.dbContext.PostgreSQL;
using lab_03.DA.Repositories;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
const string serviceName = "Hieu-Service";
// Add services to the container.
IConfiguration configuration = new ConfigurationBuilder()
                                       .AddJsonFile("appsettings.json")
                                       .Build();

builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter(options => options.Endpoint = new Uri(configuration.GetSection("OTEL_EXPORTER_OTLP_ENDPOINT").Value)));

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))

    .WithTracing(tracing => tracing
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddEntityFrameworkCoreInstrumentation()
    .AddOtlpExporter(options =>
    {
        // Set the endpoint for the Aspire dashboard
        options.Endpoint = new Uri(configuration.GetSection("OTEL_EXPORTER_OTLP_ENDPOINT").Value); // Adjust the path if necessary
    }))https://git.iu7.bmstu.ru/xf21iu26/testing2024/-/clusters

    .WithMetrics(metrics => metrics
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddRuntimeInstrumentation()
    .AddProcessInstrumentation()
    .AddOtlpExporter(options =>
    {
        // Set the endpoint for the Aspire dashboard
        options.Endpoint = new Uri(configuration.GetSection("OTEL_EXPORTER_OTLP_ENDPOINT").Value); // Adjust the path if necessary
    }));

//builder.Services.AddOpenTelemetry()
//    .WithMetrics(metrics =>
//    {
//        metrics.AddMeter("Microsoft.AspNetCore.Hosting");
//        metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
//        metrics.AddMeter("System.Net.Http");
//        metrics.AddPrometheusExporter();

//        metrics.AddOtlpExporter();
//    });

builder.Services.AddSingleton<dbContextFactory, pgSqlDbContextFactory>();
builder.Services.AddSingleton(configuration);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IMatchRepository, MatchRepository>();
builder.Services.AddTransient<ILeagueRepository, LeagueRepository>();
builder.Services.AddTransient<IClubRepository, ClubRepository>();

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<MatchService>();
builder.Services.AddTransient<LeagueService>();
builder.Services.AddTransient<ClubService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
//app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
