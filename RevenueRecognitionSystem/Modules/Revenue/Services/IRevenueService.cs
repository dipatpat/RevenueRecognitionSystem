using RevenueRecognitionSystem.Modules.Revenue.DTOs.Requests;
using RevenueRecognitionSystem.Modules.Revenue.DTOs.Responses;

namespace RevenueRecognitionSystem.Features.Revenue.Services;

public interface IRevenueService
{
    Task<RevenueResponseDto> GetCurrentRevenueAsync(RevenueRequestDto dto);
    Task<RevenueResponseDto> GetPredictedRevenueAsync(RevenueRequestDto dto);
    
    
}