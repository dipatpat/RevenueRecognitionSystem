using RevenueRecognitionSystem.Exceptions;
using RevenueRecognitionSystem.Features.Contracts.Repositories;
using RevenueRecognitionSystem.Features.Payments.Models;
using RevenueRecognitionSystem.Features.Payments.Repositories;
using RevenueRecognitionSystem.Modules.Payments.DTOs.Requests;

namespace RevenueRecognitionSystem.Features.Payments.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILicenseRepository _licenceRepository;

    public PaymentService(IPaymentRepository paymentRepository, ILicenseRepository licenseRepository)
    {
        _paymentRepository = paymentRepository;
        _licenceRepository = licenseRepository;
    }
    public async Task AddPaymentAsync(CreatePaymentDto dto)
    {
        var licence = await _licenceRepository.GetByIdWithClientAndSoftwareAsync(dto.LicenceId);
        if (licence == null)
            throw new NotFoundException("Licence not found");

        if (licence.IsCancelled)
            throw new ConflictException("Cannot pay for a cancelled licence");

        if (licence.PaymentDeadline < DateTime.UtcNow)
        {
            licence.IsCancelled = true;
            await _licenceRepository.UpdateAsync(licence); // Optional: persist cancellation
            throw new ConflictException("Payment deadline passed. Licence cancelled.");
        }

        var totalPaid = licence.Payments.Sum(p => p.Amount);
        var newTotal = totalPaid + dto.Amount;

        if (newTotal > licence.FinalPrice)
            throw new BadRequestException("Payment exceeds required amount.");

        var payment = new Payment
        {
            LicenceId = dto.LicenceId,
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod,
            PaymentDate = DateTime.UtcNow
        };

        await _paymentRepository.AddAsync(payment);

        if (newTotal == licence.FinalPrice)
        {
            licence.IsSigned = true;
            await _licenceRepository.UpdateAsync(licence);
        }
    }

}