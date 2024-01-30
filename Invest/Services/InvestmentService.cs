using Invest.Domain.Entities;
using Invest.Infrastructure.Data;
using Invest.Services.DTOs;
using Microsoft.EntityFrameworkCore;

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
            InitialAmount = investmentDto.InitialAmount,
            PurchasePrice = investmentDto.PurchasePrice
        };

        context.Investments.Add(investment);
        await context.SaveChangesAsync();
    }

    public async Task<List<InvestmentSummaryDto>> GetActiveInvestmentsSummaryAsync(Guid investorId)
    {
        var investments = await context
            .Investments
            .Include(investment => investment.Instrument)
            .Where(
                investment =>
                    investment.InvestorId == investorId
                    && investment.SalesHistory.Sum(sh => sh.Amount) < investment.InitialAmount
            )
            .ToListAsync();

        // TODO: calculate profit

        var result = investments
            .GroupBy(investment => new { investment.Instrument.Ticker, investment.Instrument.Name })
            .Select(
                grouping =>
                    new InvestmentSummaryDto
                    {
                        InstrumentTicker = grouping.Key.Ticker,
                        InstrumentName = grouping.Key.Name,
                        TotalAmount = grouping.Sum(
                            investment =>
                                investment.InitialAmount
                                - investment.SalesHistory.Sum(saleHistory => saleHistory.Amount)
                        )
                    }
            )
            .ToList();

        return result;
    }

    public async Task<List<InvestmentSummaryDto>> GetPastInvestmentsSummaryAsync(Guid investorId)
    {
        var investments = await context
            .Investments
            .Include(investment => investment.Instrument)
            .Where(
                investment =>
                    investment.InvestorId == investorId
                    && investment.SalesHistory.Sum(saleHistory => saleHistory.Amount)
                        == investment.InitialAmount
            )
            .ToListAsync();

        // TODO: calculate profit

        var result = investments
            .GroupBy(investment => new { investment.Instrument.Ticker, investment.Instrument.Name })
            .Select(
                grouping =>
                    new InvestmentSummaryDto
                    {
                        InstrumentTicker = grouping.Key.Ticker,
                        InstrumentName = grouping.Key.Name,
                        TotalAmount = grouping.Sum(
                            investment =>
                                investment.InitialAmount
                                - investment.SalesHistory.Sum(saleHistory => saleHistory.Amount)
                        )
                    }
            )
            .ToList();

        return result;
    }

    public async Task<List<InvestmentDetailsDto>> GetInvestmentsByTickerAsync(
        Guid investorId,
        string ticker
    )
    {
        var investments = await context
            .Investments
            .Include(investment => investment.Instrument)
            .Include(investment => investment.SalesHistory)
            .Where(
                investment =>
                    investment.InvestorId == investorId && investment.Instrument.Ticker == ticker
            )
            .ToListAsync();

        // TODO: calculate profit

        var notFullySoldInvestments = investments
            .Where(
                investment =>
                    investment.SalesHistory.Sum(sh => sh.Amount) < investment.InitialAmount
            )
            .OrderBy(investment => investment.PurchaseDate);

        var fullySoldInvestments = investments
            .Where(
                investment =>
                    investment.SalesHistory.Sum(sh => sh.Amount) == investment.InitialAmount
            )
            .OrderBy(investment => investment.PurchaseDate);

        var sortedInvestments = notFullySoldInvestments
            .Concat(fullySoldInvestments)
            .Select(
                investment =>
                    new InvestmentDetailsDto
                    {
                        Id = investment.Id,
                        PurchaseDate = investment.PurchaseDate,
                        InstrumentTicker = investment.Instrument.Ticker,
                        InstrumentName = investment.Instrument.Name,
                        InitialAmount = investment.InitialAmount,
                        CurrentAmount =
                            investment.InitialAmount - investment.SalesHistory.Sum(sh => sh.Amount)
                    }
            )
            .ToList();

        return sortedInvestments;
    }
}
