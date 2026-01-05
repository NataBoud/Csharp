using PrescriptionService.Application.DTOs;
using PrescriptionService.Application.Mappers;
using PrescriptionService.Domain.Ports;

namespace PrescriptionService.Application.Services
{
    public class PrescriptionAppService : IPrescriptionAppService
    {
        private readonly IPrescriptionRepository _repository;

        public PrescriptionAppService(IPrescriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PrescriptionResponseDto>> GetAllAsync()
        {
            var prescriptions = await _repository.GetAllAsync();
            return prescriptions.Select(PrescriptionMapper.ToDto);
        }

        public async Task<PrescriptionResponseDto?> GetByIdAsync(Guid id)
        {
            var prescription = await _repository.GetByIdAsync(id);
            return prescription == null ? null : PrescriptionMapper.ToDto(prescription);
        }

        public async Task<IEnumerable<PrescriptionResponseDto>> GetByConsultationIdAsync(Guid consultationId)
        {
            var prescriptions = await _repository.GetByConsultationIdAsync(consultationId);
            return prescriptions.Select(PrescriptionMapper.ToDto);
        }

        public async Task<PrescriptionResponseDto> CreateAsync(PrescriptionRequestDto dto)
        {
            var prescription = PrescriptionMapper.ToEntity(dto);
            var created = await _repository.CreateAsync(prescription);
            return PrescriptionMapper.ToDto(created);
        }

        public async Task<PrescriptionResponseDto?> UpdateAsync(Guid id, PrescriptionRequestDto dto)
        {
            var prescription = PrescriptionMapper.ToEntity(dto);
            var updated = await _repository.UpdateAsync(id, prescription);
            return updated == null ? null : PrescriptionMapper.ToDto(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<TotalPrisesResponseDto?> GetTotalPrisesAsync(Guid id)
        {
            var prescription = await _repository.GetByIdAsync(id);
            return prescription == null ? null : PrescriptionMapper.ToTotalPrisesDto(prescription);
        }
    }
}
