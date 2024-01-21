using InvestLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invest.Data;

public class BusinessDbContext(DbContextOptions<BusinessDbContext> options) : DbContext(options)
{
    public DbSet<Investor> Investors { get; set; }
    public DbSet<Investment> Investments { get; set; }
    public DbSet<Instrument> Instruments { get; set; }
    public DbSet<SaleHistory> SaleHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Investor>(entity =>
        {
            entity.Property(investor => investor.Name).IsRequired().HasMaxLength(50);

            entity.Property(investor => investor.Surname).IsRequired().HasMaxLength(50);

            entity.Property(investor => investor.UserId).IsRequired();

            entity
                .HasMany(investor => investor.Investments)
                .WithOne(investment => investment.Investor)
                .HasForeignKey(investment => investment.InvestorId);
        });

        modelBuilder.Entity<Investment>(entity =>
        {
            entity.Property(investment => investment.PurchaseDate).IsRequired();
            entity
                .Property(investment => investment.Amount)
                .IsRequired()
                .HasColumnType("decimal(20,10)");
            entity
                .Property(investment => investment.PurchasePrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity
                .HasMany(investment => investment.SalesHistory)
                .WithOne(saleHistory => saleHistory.Investment)
                .HasForeignKey(saleHistory => saleHistory.InvestmentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(investment => investment.Instrument)
                .WithMany(instrument => instrument.Investments)
                .HasForeignKey(investment => investment.InstrumentId);

            entity
                .HasOne(investment => investment.Investor)
                .WithMany(investor => investor.Investments)
                .HasForeignKey(investment => investment.InvestorId);
        });

        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.Property(instrument => instrument.Name).IsRequired().HasMaxLength(50);

            entity.Property(instrument => instrument.Ticker).IsRequired().HasMaxLength(5);

            entity
                .HasMany(instrument => instrument.Investments)
                .WithOne(investment => investment.Instrument)
                .HasForeignKey(investment => investment.InstrumentId);
        });

        modelBuilder.Entity<SaleHistory>(entity =>
        {
            entity.Property(saleHistory => saleHistory.SaleDate).IsRequired();

            entity
                .Property(saleHistory => saleHistory.Amount)
                .IsRequired()
                .HasColumnType("decimal(20,10)");

            entity.Property(sh => sh.SalePrice).IsRequired().HasColumnType("decimal(18,2)");

            entity
                .HasOne(saleHistory => saleHistory.Investment)
                .WithMany(investment => investment.SalesHistory)
                .HasForeignKey(saleHistory => saleHistory.InvestmentId);
        });
    }
}
