namespace RevenueRecognitionSystem.Modules.Software.Repositories;
using RevenueRecognitionSystem.Modules.Software.Modules;

public interface ISoftwareRepository
{
    Task<Software?> GetByIdAsync(int id);
}