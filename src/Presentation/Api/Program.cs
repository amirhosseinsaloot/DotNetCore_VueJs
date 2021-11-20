using Api.Extensions;
using Microsoft.AspNetCore;
using Serilog;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        IConfigurationRoot configuration =
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        SerilogExtensions.Register(configuration);

        BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
                      .UseStartup<Startup>()
                      .UseSerilog()
                      .Build();
    }
}
