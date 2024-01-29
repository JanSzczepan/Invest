using Invest.Domain.Entities;
using Invest.Infrastructure.Data;
using Invest.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Invest.Services;

public class InvestorService(BusinessDbContext context)
{
    public async Task<Guid> AddInvestorAsync(InvestorDto investorDto)
    {
        var investor = new Investor
        {
            FirstName = investorDto.FirstName,
            LastName = investorDto.LastName,
            TotalProfitInUSD = 0,
            TotalProfitPercentage = 0,
            UserId = investorDto.UserId
        };

        await context.Investors.AddAsync(investor);
        await context.SaveChangesAsync();

        return investor.Id;
    }

    public async Task RemoveInvestorAsync(string userId)
    {
        var investor = await context
            .Investors
            .FirstOrDefaultAsync(investor => investor.UserId == userId);
        if (investor != null)
        {
            context.Investors.Remove(investor);
            await context.SaveChangesAsync();
        }
    }
}
