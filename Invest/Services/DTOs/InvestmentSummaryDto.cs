namespace Invest.Services.DTOs;

public class InvestmentSummaryDto
{
    public string InstrumentTicker { get; set; }
    public string InstrumentName { get; set; }
    public decimal TotalAmount { get; set; }
}
