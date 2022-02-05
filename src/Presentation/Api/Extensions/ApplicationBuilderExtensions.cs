using Api.Middlewares;
using Infrastructure;
using Infrastructure.Data.DataInitializer;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }

    public static void IntializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

        dbContext?.Database.Migrate();

        var dataInitializer = scope.ServiceProvider.GetService<DataInitializer>();
        dataInitializer?.InstallRequierdData();

        scope.Dispose();
    }
}
