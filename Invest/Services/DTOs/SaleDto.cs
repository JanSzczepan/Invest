namespace Invest.Services.DTOs;

public class SaleDto
{
    public DateTime SaleDate { get; set; }
    public decimal Amount { get; set; }
    public decimal SalePrice { get; set; }
    public decimal ProfitInUSD { get; set; }
    public decimal ProfitPercentage { get; set; }
}
