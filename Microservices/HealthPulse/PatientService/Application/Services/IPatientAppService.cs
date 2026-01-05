using PatientService.Application.DTOs;

namespace PatientService.Application.Services
{
    public interface IPatientAppService
    {
        Task<IEnumerable<PatientResponseDto>> GetAllAsync();
        Task<PatientResponseDto?> GetByIdAsync(Guid id);
        Task<PatientResponseDto> CreateAsync(PatientRequestDto dto);
        Task<PatientResponseDto?> UpdateAsync(Guid id, PatientRequestDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
