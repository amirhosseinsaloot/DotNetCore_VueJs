using Api.Extensions;
using Core.Setting;
using Core.Utilities;
using FluentValidation.AspNetCore;
using Serilog;
using Service.Identity.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var services = builder.Services;

var configuration = builder.Configuration;
ConfigureConfiguration(services, configuration);

var applicationSettings = configuration
                           .GetSection(nameof(ApplicationSettings))
                           .Get<ApplicationSettings>();
applicationSettings.ValidateApplicationSettings();

const string CORS_POLICY = "CorsPolicy";
SerilogExtensions.Register(configuration);

// Add services to the container.
ConfigureServices(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigurePipeline(app);

app.Run();

void ConfigureConfiguration(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddOptions<ApplicationSettings>()
            .Bind(configuration.GetSection(nameof(ApplicationSettings)))
            .ValidateDataAnnotations()
            .Validate(config => config.ValidateApplicationSettings())
            .ValidateOnStart();
}

void ConfigureServices(IServiceCollection services)
{
    // Database services
    services.AddDbContext(applicationSettings.DatabaseSetting);

    services.AddCustomIdentity(applicationSettings.IdentitySetting);

    services.AutoMapperRegistration();

    services.AddJwtAuthentication(applicationSettings.JwtSetting);

    services.AddApplicationDependencyRegistration(applicationSettings);

    services.AddSwagger();

    services.AddHttpContextAccessor();

    // Add service and create Policy with options
    services.AddCors(options =>
    {
        options.AddPolicy(name: CORS_POLICY,
            builder => builder
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .SetIsOriginAllowed(origin => true)  // Allow any origin
                      .AllowCredentials());                // Allow credentials
    });

    services.AddMvc();
    services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TokenRequestValidator>());
}

void ConfigurePipeline(IApplicationBuilder app)
{
    app.IntializeDatabase();

    app.UseGlobalExceptionHandler();

    app.UseRouting();

    // Enable Cors
    app.UseCors(CORS_POLICY);

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.RegisterSwaggerMidlleware();
}