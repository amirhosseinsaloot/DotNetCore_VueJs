using Api.Extensions;
using Core.Setting;
using FluentValidation.AspNetCore;
using Service.Identity.Models;

namespace Api;

public class Startup
{
    #region Fields

    private const string CORS_POLICY = "CorsPolicy";

    private readonly IConfiguration _configuration;

    private readonly ApplicationSettings _applicationSettings;

    #endregion Fields

    #region Ctor

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        _applicationSettings = configuration
                               .GetSection(nameof(ApplicationSettings))
                               .Get<ApplicationSettings>();
    }

    #endregion Ctor

    #region Methods

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ApplicationSettings>(_configuration.GetSection(nameof(ApplicationSettings)));

        // Database services
        services.AddDbContext(_applicationSettings.DatabaseSetting);

        services.AddCustomIdentity(_applicationSettings.IdentitySetting);

        services.AutoMapperRegistration();

        services.AddJwtAuthentication(_applicationSettings.JwtSetting);

        services.AddApplicationDependencyRegistration(_applicationSettings);

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

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

    #endregion Methods
}
