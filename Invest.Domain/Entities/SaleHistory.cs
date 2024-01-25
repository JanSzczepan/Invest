namespace Invest.Domain.Entities;

public class SaleHistory
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal Amount { get; set; }
    public decimal SalePrice { get; set; }
    public decimal ProfitInUSD { get; set; }
    public decimal ProfitPercentage { get; set; }

    public Investment Investment { get; set; }
    public Guid InvestmentId { get; set; }
}
