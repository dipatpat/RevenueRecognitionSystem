using RevenueRecognitionSystem.Modules.Licence.Models;

namespace RevenueRecognitionSystem.Features.Contracts.Repositories;

public interface ILicenseRepository
{
    Task<bool> HasActiveContractAsync(int clientId, int softwareId);
    Task<bool> HasAnyPastContractAsync(int clientId);
    
    Task<int> AddAsync(Licence licence);
    Task<Licence?> GetByIdWithClientAndSoftwareAsync(int id); 
    
    Task UpdateAsync(Licence licence);
    
    

}