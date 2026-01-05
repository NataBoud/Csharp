using ConsultationService.Application.DTOs;

namespace ConsultationService.Application.Services
{
    public interface IConsultationAppService
    {
        Task<IEnumerable<ConsultationResponseDto>> GetAllAsync();
        Task<ConsultationResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ConsultationResponseDto>> GetByPatientIdAsync(Guid patientId);
        Task<ConsultationResponseDto> CreateAsync(ConsultationRequestDto dto);
        Task<ConsultationResponseDto?> UpdateAsync(Guid id, ConsultationRequestDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<CoutHoraireResponseDto?> GetCoutHoraireAsync(Guid id);
    }
}
