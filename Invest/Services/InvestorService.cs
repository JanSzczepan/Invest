using Invest.Domain.Entities;
using Invest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Invest.Services;

public class InvestorService(BusinessDbContext context)
{
    private readonly BusinessDbContext _context = context;

    public async Task AddInvestorAsync(string userId, string firstName, string lastName)
    {
        var investor = new Investor
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            UserId = userId
        };

        await _context.Investors.AddAsync(investor);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveInvestorAsync(string userId)
    {
        var investor = await _context
            .Investors
            .FirstOrDefaultAsync(investor => investor.UserId == userId);
        if (investor != null)
        {
            _context.Investors.Remove(investor);
            await _context.SaveChangesAsync();
        }
    }
}
