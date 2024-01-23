using System.Text.Json;
using Invest.Domain.Entities;

namespace Invest.Infrastructure.Data.Seeds;

public class InstrumentDataSeeder(BusinessDbContext context)
{
    public async Task SeedAsync()
    {
        if (!context.Instruments.Any())
        {
            var jsonData = await File.ReadAllTextAsync(
                "../Invest.Infrastructure/Data/Seeds/instruments.json"
            );
            var instruments = JsonSerializer.Deserialize<List<Instrument>>(jsonData);

            if (instruments != null)
            {
                context.Instruments.AddRange(instruments);
                await context.SaveChangesAsync();
            }
        }
    }
}
