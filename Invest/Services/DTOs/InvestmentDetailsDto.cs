namespace Invest.Services.DTOs;

public class InvestmentDetailsDto
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string InstrumentTicker { get; set; }
    public string InstrumentName { get; set; }
    public decimal InitialAmount { get; set; }
    public decimal CurrentAmount { get; set; }
}
