using Invest.Components;
using Invest.Components.Account;
using Invest.Data;
using InvestLibrary.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder
    .Services
    .AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder
    .Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var authConnectionString =
    builder.Configuration.GetConnectionString("AuthenticationConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'AuthenticationConnection' not found."
    );
var businessConnectionString =
    builder.Configuration.GetConnectionString("BusinessConnection")
    ?? throw new InvalidOperationException("Connection string 'BusinessConnection' not found.");
builder
    .Services
    .AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(authConnectionString));
builder
    .Services
    .AddDbContext<BusinessDbContext>(options => options.UseSqlServer(businessConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder
    .Services
    .AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuthenticationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Invest.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
