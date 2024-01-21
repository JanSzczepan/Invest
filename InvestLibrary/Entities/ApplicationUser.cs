using Microsoft.AspNetCore.Identity;

namespace InvestLibrary.Entities;

public class ApplicationUser : IdentityUser
{
    public Investor Investor { get; set; }
}
