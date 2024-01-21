namespace InvestLibrary.Entities;

public class Investor
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public ApplicationUser User { get; set; }
    public Guid UserId { get; set; }

    public ICollection<Investment> Investments { get; set; } = new List<Investment>();
}
