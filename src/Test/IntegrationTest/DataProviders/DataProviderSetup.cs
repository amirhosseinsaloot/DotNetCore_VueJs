using Api.AutoMapperConfiguration.Profiles.Tickets;
using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTest.DataProviders;

public static class DataProviderSetup
{
    public static DbContextOptions<ApplicationDbContext> GetDbContextOptions()
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestGenericDataProvider")
            .Options;
    }

    public static Mapper GetAutoMapper()
    {
        var ticketTypeProfiles = new TicketTypeProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(ticketTypeProfiles));
        return new Mapper(configuration);
    }
}
