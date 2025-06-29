using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RevenueRecognitionSystem.Auth;
using RevenueRecognitionSystem.Features.Clients.Models;
using RevenueRecognitionSystem.Features.Discounts.Models;
using RevenueRecognitionSystem.Features.Payments.Models;
using RevenueRecognitionSystem.Modules.Licence.Models;
using RevenueRecognitionSystem.Modules.Software.Modules;
using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Infrastructure.DAL;

public class RevenueRecognitionDbContext : DbContext
{
    public RevenueRecognitionDbContext(DbContextOptions<RevenueRecognitionDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<CompanyClient> CompanyClients { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Licence> Licences { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Software> Software { get; set; }

    public DbSet<Discount> Discounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Employee>()
        .Property(e => e.Role)
        .HasConversion<string>();
    
    modelBuilder.Entity<Client>()
        .HasDiscriminator<string>("ClientType")
        .HasValue<IndividualClient>("Individual")
        .HasValue<CompanyClient>("Company");

    modelBuilder.Entity<Software>()
        .Property(s => s.Category)
        .HasConversion<string>();
    
    modelBuilder.Entity<Software>()
        .Property(s => s.BaseUpfrontPrice)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Discount>()
        .Property(d => d.Type)
        .HasConversion<string>();
    
    modelBuilder.Entity<Discount>()
        .Property(d => d.Percentage)
        .HasPrecision(5, 2);
    
    modelBuilder.Entity<Payment>()
        .Property(p => p.Amount)
        .HasPrecision(18, 2);
    
    modelBuilder.Entity<Licence>()
        .Property(l => l.FinalPrice)
        .HasPrecision(18, 2);
    
    modelBuilder.Entity<IndividualClient>()
        .Property(i => i.Pesel)
        .IsRequired()
        .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

    modelBuilder.Entity<CompanyClient>()
        .Property(c => c.KRS)
        .IsRequired()
        .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

    modelBuilder.Entity<Software>().HasData(
        new Software
        {
            Id = 1,
            Name = "EduMaster",
            Description = "Educational suite for remote learning",
            Version = "1.0.0",
            Category = SoftwareCategory.Education,
            IsAvailableAsSubscription = true,
            IsAvailableAsUpfront = true
        },
        new Software
        {
            Id = 2,
            Name = "FinTracker",
            Description = "Financial analytics and forecasting",
            Version = "3.2.1",
            Category = SoftwareCategory.Finance,
            IsAvailableAsSubscription = true,
            IsAvailableAsUpfront = false
        },
        new Software
        {
            Id = 3,
            Name = "SalesBooster",
            Description = "CRM tool for growing health businesses",
            Version = "2.0.0",
            Category = SoftwareCategory.Health,
            IsAvailableAsSubscription = true,
            IsAvailableAsUpfront = true
        },
        new Software
        {
            Id = 4,
            Name = "MediSys",
            Description = "Medical records and appointment management",
            Version = "1.3.0",
            Category = SoftwareCategory.Health,
            IsAvailableAsSubscription = false,
            IsAvailableAsUpfront = true
        },
        new Software
        {
            Id = 5,
            Name = "CodeFlow",
            Description = "DevOps and continuous delivery toolkit",
            Version = "4.5.2",
            Category = SoftwareCategory.Technology,
            IsAvailableAsSubscription = true,
            IsAvailableAsUpfront = true
        },
        new Software
        {
            Id = 6,
            Name = "TravelPro",
            Description = "Travel booking and itinerary management system",
            Version = "3.0.1",
            Category = SoftwareCategory.Utilities,
            IsAvailableAsSubscription = false,
            IsAvailableAsUpfront = true
        }
    );

    modelBuilder.Entity<IndividualClient>().HasData(
        new IndividualClient("99010112345")
        {
            Id = 1,
            FirstName = "Anna",
            LastName = "Nowak",
            Email = "anna.nowak@example.com",
            Address = "Warsaw, Poland",
            PhoneNumber = "123456789"
        }
    );

    modelBuilder.Entity<CompanyClient>().HasData(
        new CompanyClient
        {
            Id = 2,
            Name = "TechCorp",
            KRS = "1234567890",
            Email = "contact@techcorp.com",
            Address = "Krakow, Poland",
            PhoneNumber = "987654321"
        }
    );

    modelBuilder.Entity<Discount>().HasData(
        new Discount
        {
            Id = 1,
            Name = "Summer Upfront Deal",
            Type = DiscountType.Upfront,
            Percentage = 0.15m,
            StartDate = new DateTime(2025, 6, 1),
            EndDate = new DateTime(2025, 8, 31)
        },
        new Discount
        {
            Id = 2,
            Name = "Back to School Sub Discount",
            Type = DiscountType.Subscription,
            Percentage = 0.10m,
            StartDate = new DateTime(2025, 8, 15),
            EndDate = new DateTime(2025, 10, 15)
        },
        new Discount
        {
            Id = 3,
            Name = "Black Friday Upfront",
            Type = DiscountType.Upfront,
            Percentage = 0.20m,
            StartDate = new DateTime(2025, 11, 24),
            EndDate = new DateTime(2025, 11, 30)
        }
    );

    modelBuilder.Entity<Licence>().HasData(
        new Licence
        {
            Id = 1,
            ClientId = 1,
            SoftwareId = 1,
            StartDate = new DateTime(2025, 6, 10),
            EndDate = new DateTime(2026, 6, 10),
            PaymentDeadline = new DateTime(2025, 6, 20),
            FinalPrice = 8500,
            IsSigned = true,
            SupportYears = 2
        }
    );

    modelBuilder.Entity<Payment>().HasData(
        new Payment
        {
            Id = 1,
            LicenceId = 1,
            Amount = 8500,
            PaymentDate = new DateTime(2025, 6, 15),
            PaymentMethod = PaymentMethod.BankTransfer,
            TransactionReference = "TX123456789",
            Confirmed = true
        }
    );

    base.OnModelCreating(modelBuilder);
}

}