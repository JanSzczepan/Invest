namespace Invest.Domain.Entities;

public class Instrument
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ticker { get; set; }

    public ICollection<Investment> Investments { get; set; } = new List<Investment>();
}
