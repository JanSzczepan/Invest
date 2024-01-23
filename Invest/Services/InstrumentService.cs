using Invest.Infrastructure.Data;
using Invest.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Invest.Services;

public class InstrumentService(BusinessDbContext context)
{
    public async Task<List<InstrumentDto>> GetAllInstrumentsAsync()
    {
        return await context
            .Instruments
            .Select(
                instrument =>
                    new InstrumentDto
                    {
                        Id = instrument.Id,
                        Name = instrument.Name,
                        Ticker = instrument.Ticker
                    }
            )
            .ToListAsync();
    }
}
