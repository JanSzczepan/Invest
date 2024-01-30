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

    public async Task AddSaleAsync(InvestmentSaleDto saleDto)
    {
        var investment =
            await context
                .Investments
                .Include(i => i.SalesHistory)
                .FirstOrDefaultAsync(i => i.Id == saleDto.InvestmentId)
            ?? throw new InvalidOperationException("Investment not found.");

        var totalSoldAmount = investment.SalesHistory.Sum(sh => sh.Amount);
        if (totalSoldAmount + saleDto.Amount > investment.InitialAmount)
        {
            throw new InvalidOperationException(
                "Sale amount exceeds the available investment amount."
            );
        }

        var profitInUsd = (saleDto.SalePrice - investment.PurchasePrice) * saleDto.Amount;
        var profitPercentage = (saleDto.SalePrice / investment.PurchasePrice - 1) * 100;

        var saleHistory = new SaleHistory
        {
            InvestmentId = saleDto.InvestmentId,
            SaleDate = saleDto.SaleDate,
            Amount = saleDto.Amount,
            SalePrice = saleDto.SalePrice,
            ProfitInUSD = profitInUsd,
            ProfitPercentage = profitPercentage
        };

        context.SaleHistories.Add(saleHistory);
        await context.SaveChangesAsync();
    }

    public async Task<decimal> GetAvailableAmountForSaleAsync(Guid investmentId)
    {
        var investment =
            await context
                .Investments
                .Include(i => i.SalesHistory)
                .FirstOrDefaultAsync(i => i.Id == investmentId)
            ?? throw new InvalidOperationException("Investment not found.");

        var totalSoldAmount = investment.SalesHistory.Sum(sh => sh.Amount);
        return investment.InitialAmount - totalSoldAmount;
    }

    public async Task<InvestmentInfoDto> GetInvestmentInfoAsync(Guid investmentId)
    {
        var investment =
            await context
                .Investments
                .Include(i => i.Instrument)
                .Include(i => i.SalesHistory)
                .FirstOrDefaultAsync(i => i.Id == investmentId)
            ?? throw new InvalidOperationException("Investment not found.");

        var salesHistoryDto = investment
            .SalesHistory
            .Select(
                sh =>
                    new SaleDto
                    {
                        SaleDate = sh.SaleDate,
                        Amount = sh.Amount,
                        SalePrice = sh.SalePrice,
                        ProfitInUSD = sh.ProfitInUSD,
                        ProfitPercentage = sh.ProfitPercentage
                    }
            )
            .ToList();

        var totalProfitInUsd = salesHistoryDto.Sum(sh => sh.ProfitInUSD);
        var totalProfitPercentage = salesHistoryDto.Any()
            ? salesHistoryDto.Average(sh => sh.ProfitPercentage)
            : 0;

        return new InvestmentInfoDto
        {
            PurchaseDate = investment.PurchaseDate,
            InstrumentTicker = investment.Instrument.Ticker,
            InstrumentName = investment.Instrument.Name,
            InitialAmount = investment.InitialAmount,
            CurrentAmount = investment.InitialAmount - investment.SalesHistory.Sum(sh => sh.Amount),
            PurchasePrice = investment.PurchasePrice,
            TotalProfitInUsd = totalProfitInUsd,
            TotalProfitPercentage = totalProfitPercentage,
            SalesHistory = salesHistoryDto
        };
    }
}
