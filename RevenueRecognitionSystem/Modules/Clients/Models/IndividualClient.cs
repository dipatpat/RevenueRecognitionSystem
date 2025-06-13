namespace RevenueRecognitionSystem.Features.Clients.Models;

public class IndividualClient : Client
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pesel { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    
    public IndividualClient(string pesel)
    {
        Pesel = pesel;
    }

    public override string GetClientType()
    {
        return "Individual";
    }
    
    public void SoftDelete()
    {
        IsDeleted = true;

        FirstName = "[DELETED]";
        LastName = "[DELETED]";
        Email = string.Empty;
        Address = string.Empty;
        PhoneNumber = string.Empty;
    }
    
}