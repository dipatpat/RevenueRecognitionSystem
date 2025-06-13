namespace RevenueRecognitionSystem.Features.Clients.Models;

public class CompanyClient : Client
{
    public string Name { get; set; } = null!;
    public string KRS { get; set; } = null!;

    public override string GetClientType()
    {
        return "Company";
    }
    
}