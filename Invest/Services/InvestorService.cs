using Invest.Domain.Entities;
using Invest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Invest.Services;

public class InvestorService(BusinessDbContext context)
{
    public async Task AddInvestorAsync(string userId, string firstName, string lastName)
    {
        var investor = new Investor
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            UserId = userId
        };

        await context.Investors.AddAsync(investor);
        await context.SaveChangesAsync();
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
