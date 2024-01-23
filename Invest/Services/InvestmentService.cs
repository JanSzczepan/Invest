using Invest.Domain.Entities;
using Invest.Infrastructure.Data;
using Invest.Services.DTOs;

namespace Invest.Services;

public class InvestmentService(BusinessDbContext context)
{
    public async Task AddInvestmentAsync(InvestmentDto investmentDto)
    {
        var investment = new Investment
        {
            InvestorId = investmentDto.InvestorId,
            InstrumentId = investmentDto.InstrumentId,
            PurchaseDate = investmentDto.PurchaseDate,
            Amount = investmentDto.Amount,
            PurchasePrice = investmentDto.PurchasePrice
        };

        context.Investments.Add(investment);
        await context.SaveChangesAsync();
    }
}
