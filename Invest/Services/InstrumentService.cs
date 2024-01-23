using Invest.Domain.Entities;
using Invest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Invest.Services;

public class InstrumentService(BusinessDbContext context)
{
    public async Task<List<Instrument>> GetAllInstrumentsAsync()
    {
        return await context.Instruments.ToListAsync();
    }
}
