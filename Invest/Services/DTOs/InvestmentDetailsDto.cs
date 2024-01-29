namespace Invest.Services.DTOs;

public class InvestmentDetailsDto
{
    public string InstrumentTicker { get; set; }
    public string InstrumentName { get; set; }
    public decimal InitialAmount { get; set; }
    public decimal CurrentAmount { get; set; }
}
