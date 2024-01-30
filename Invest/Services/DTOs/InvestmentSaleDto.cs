namespace Invest.Services.DTOs;

public class InvestmentSaleDto
{
    public Guid InvestmentId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal Amount { get; set; }
    public decimal SalePrice { get; set; }
}
