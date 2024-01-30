namespace Invest.Services.DTOs;

public class InvestmentInfoDto
{
    public DateTime PurchaseDate { get; set; }
    public string InstrumentTicker { get; set; }
    public string InstrumentName { get; set; }
    public decimal InitialAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal TotalProfitInUsd { get; set; }
    public decimal TotalProfitPercentage { get; set; }
    public List<SaleDto> SalesHistory { get; set; } = [ ];
}
