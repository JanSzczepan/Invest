namespace Invest.Domain.Entities;

public class Investment
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal InitialAmount { get; set; }
    public decimal Amount { get; set; }
    public decimal PurchasePrice { get; set; }

    public Instrument Instrument { get; set; }
    public int InstrumentId { get; set; }

    public ICollection<SaleHistory> SalesHistory { get; set; } = new List<SaleHistory>();

    public Investor Investor { get; set; }
    public Guid InvestorId { get; set; }
}
