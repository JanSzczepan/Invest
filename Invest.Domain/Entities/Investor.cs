namespace Invest.Domain.Entities;

public class Investor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal TotalProfitInUSD { get; set; }
    public decimal TotalProfitPercentage { get; set; }

    public string UserId { get; set; }

    public ICollection<Investment> Investments { get; set; } = new List<Investment>();
}
