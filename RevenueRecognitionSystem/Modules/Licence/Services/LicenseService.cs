using RevenueRecognitionSystem.Exceptions;
using RevenueRecognitionSystem.Features.Clients.Models;
using RevenueRecognitionSystem.Features.Contracts.Repositories;
using RevenueRecognitionSystem.Features.Discounts.Repositories;
using RevenueRecognitionSystem.Modules.Licence.DTOs.Requests;
using RevenueRecognitionSystem.Modules.Licence.DTOs.Responses;
using RevenueRecognitionSystem.Modules.Licence.Models;
using RevenueRecognitionSystem.Modules.Software.Repositories;
using RevenueRecognitionSystem.Shared.Enums;

namespace RevenueRecognitionSystem.Features.Contracts.Services;

public class LicenseService : ILicenseService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILicenseRepository _licenceRepository;
    private readonly ISoftwareRepository _softwareRepository;

    public LicenseService(IDiscountRepository discountRepository, ILicenseRepository licenseRepository, ISoftwareRepository softwareRepository)
    {
        _discountRepository = discountRepository;
        _licenceRepository = licenseRepository;
        _softwareRepository = softwareRepository;
    }
    
    public async Task<LicenceDto> GetByIdAsync(int id)
    {
        var licence = await _licenceRepository.GetByIdWithClientAndSoftwareAsync(id);

        if (licence == null)
            throw new NotFoundException($"Licence with ID {id} not found.");

        return new LicenceDto
        {
            Id = licence.Id,
            FinalPrice = licence.FinalPrice,
            IsSigned = licence.IsSigned,
            IsCancelled = licence.IsCancelled,
            StartDate = licence.StartDate,
            EndDate = licence.EndDate,
            PaymentDeadline = licence.PaymentDeadline,
            SupportYears = licence.SupportYears,
            SoftwareName = licence.Software.Name,
            SoftwareVersion = licence.Software.Version,
            ClientFullName = licence.Client is IndividualClient ic
                ? $"{ic.FirstName} {ic.LastName}"
                : ((CompanyClient)licence.Client).Name
        };
    }

    
    public async Task<int> CreateUpfrontLicenceAsync(CreateLicenceDto dto)
    {
        if (dto.SupportYears < 1 || dto.SupportYears > 3)
            throw new ArgumentException("Support years must be between 1 and 3.");

        var existing = await _licenceRepository.HasActiveContractAsync(dto.ClientId, dto.SoftwareId);
        if (existing)
            throw new InvalidOperationException("Client already has an active contract for this software.");

        var software = await _softwareRepository.GetByIdAsync(dto.SoftwareId);
        if (software == null || !software.IsAvailableAsUpfront)
            throw new ArgumentException("Software is not available for upfront purchase.");

        var basePrice = software.BaseUpfrontPrice;

        var activeDiscounts = await _discountRepository.GetActiveDiscountsAsync(DateTime.UtcNow, DiscountType.Upfront);
        var maxDiscount = activeDiscounts.Max(d => d.Percentage);

        var isReturningClient = await _licenceRepository.HasAnyPastContractAsync(dto.ClientId);
        var returnerDiscount = isReturningClient ? 0.05m : 0;

        var totalDiscount = maxDiscount + returnerDiscount;

        var finalPrice = basePrice * (1 - totalDiscount) + (dto.SupportYears - 1) * 1000;

        var deadline = dto.StartDate.AddDays(10); // default or configurable

        var licence = new Licence
        {
            ClientId = dto.ClientId,
            SoftwareId = dto.SoftwareId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            PaymentDeadline = deadline,
            FinalPrice = finalPrice,
            SupportYears = dto.SupportYears,
            IsSigned = false,
            IsCancelled = false
        };

        return await _licenceRepository.AddAsync(licence);
    }

}