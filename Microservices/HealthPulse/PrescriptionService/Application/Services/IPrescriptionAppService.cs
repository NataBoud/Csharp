using PrescriptionService.Application.DTOs;

namespace PrescriptionService.Application.Services
{
    public interface IPrescriptionAppService
    {
        Task<IEnumerable<PrescriptionResponseDto>> GetAllAsync();
        Task<PrescriptionResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<PrescriptionResponseDto>> GetByConsultationIdAsync(Guid consultationId);
        Task<PrescriptionResponseDto> CreateAsync(PrescriptionRequestDto dto);
        Task<PrescriptionResponseDto?> UpdateAsync(Guid id, PrescriptionRequestDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<TotalPrisesResponseDto?> GetTotalPrisesAsync(Guid id);
    }
}
