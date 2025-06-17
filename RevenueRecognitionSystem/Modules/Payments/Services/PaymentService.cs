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
            throw new NotFoundException("Licence not found.");

        if (licence.IsSigned)
        {
            var lastPayment = await _paymentRepository.GetLatestPaymentByLicenceIdAsync(dto.LicenceId);
            var paidDate = lastPayment?.PaymentDate.ToString("yyyy-MM-dd") ?? "unknown date";
            throw new ConflictException($"This licence has already been paid and signed on {paidDate}.");
        }

        if (licence.IsCancelled)
            throw new ConflictException("Cannot pay for a cancelled licence.");

        if (licence.PaymentDeadline < DateTime.UtcNow)
        {
            licence.IsCancelled = true;
            await _licenceRepository.UpdateAsync(licence); // Persist cancellation
            throw new ConflictException("Payment deadline passed. Licence has been cancelled.");
        }

        var payments = await _paymentRepository.GetPaymentByLicenceIdAsync(dto.LicenceId);
        var totalPaid = payments.Sum(p => p.Amount);
        var newTotal = totalPaid + dto.Amount;

        if (newTotal > licence.FinalPrice)
            throw new BadRequestException("Payment exceeds required amount.");

        var payment = new Payment
        {
            LicenceId = dto.LicenceId,
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod,
            PaymentDate = DateTime.UtcNow,
            Confirmed = true
        };

        await _paymentRepository.AddAsync(payment);

        if (newTotal == licence.FinalPrice)
        {
            licence.IsSigned = true;
            await _licenceRepository.UpdateAsync(licence);
        }
    }

}