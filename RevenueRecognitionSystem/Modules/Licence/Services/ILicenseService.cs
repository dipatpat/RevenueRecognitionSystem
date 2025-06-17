using RevenueRecognitionSystem.Modules.Licence.DTOs.Requests;
using RevenueRecognitionSystem.Modules.Licence.DTOs.Responses;
using RevenueRecognitionSystem.Modules.Licence.Models;

namespace RevenueRecognitionSystem.Features.Contracts.Services;

public interface ILicenseService
{
    Task<int> CreateUpfrontLicenceAsync(CreateLicenceDto dto);
    Task<LicenceDto> GetByIdAsync(int id);}