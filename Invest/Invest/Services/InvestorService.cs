using Invest.Data;
using InvestLibrary.Entities;

namespace InvestLibrary.Services;

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
}
