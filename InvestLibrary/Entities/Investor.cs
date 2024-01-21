﻿namespace InvestLibrary.Entities;

public class Investor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string UserId { get; set; }

    public ICollection<Investment> Investments { get; set; } = new List<Investment>();
}
