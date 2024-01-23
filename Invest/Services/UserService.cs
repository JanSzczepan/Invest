using Microsoft.AspNetCore.Components.Authorization;

namespace Invest.Services;

public class UserService(AuthenticationStateProvider authenticationStateProvider)
{
    public async Task<Guid?> GetInvestorIdAsync()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        var investorIdClaim = user.FindFirst(claim => claim.Type == "InvestorId")?.Value;

        if (Guid.TryParse(investorIdClaim, out var parsedId))
        {
            return parsedId;
        }

        return null;
    }
}
