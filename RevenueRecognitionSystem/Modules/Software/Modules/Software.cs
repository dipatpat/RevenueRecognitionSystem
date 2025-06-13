using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Modules.Software.Modules;

public class Software
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Version { get; set; } = null!;
    
    public SoftwareCategory Category { get; set; }

    public bool IsAvailableAsSubscription { get; set; }
    public bool IsAvailableAsUpfront { get; set; }

    public ICollection<Licence.Models.Licence> Licences { get; set; } = new List<Licence.Models.Licence>();
}