namespace Invest.Services.DTOs;

public class InvestmentDto
{
    public Guid InvestorId { get; set; }
    public int InstrumentId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Amount { get; set; }
    public decimal PurchasePrice { get; set; }
}
