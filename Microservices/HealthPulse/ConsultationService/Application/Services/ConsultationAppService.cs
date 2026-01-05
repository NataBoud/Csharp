using ConsultationService.Domain.Ports;
using ConsultationService.Application.DTOs;
using ConsultationService.Application.Mappers;

namespace ConsultationService.Application.Services
{
    public class ConsultationAppService : IConsultationAppService
    {
        private readonly IConsultationRepository _repository;

        public ConsultationAppService(IConsultationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConsultationResponseDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return ConsultationMapper.ToDtoList(list);
        }

        public async Task<ConsultationResponseDto?> GetByIdAsync(Guid id)
        {
            var c = await _repository.GetByIdAsync(id);
            return c != null ? ConsultationMapper.ToDto(c) : null;
        }

        public async Task<IEnumerable<ConsultationResponseDto>> GetByPatientIdAsync(Guid patientId)
        {
            var list = await _repository.GetByPatientIdAsync(patientId);
            return ConsultationMapper.ToDtoList(list);
        }

        public async Task<ConsultationResponseDto> CreateAsync(ConsultationRequestDto dto)
        {
            var c = ConsultationMapper.ToEntity(dto);
            var created = await _repository.CreateAsync(c);
            return ConsultationMapper.ToDto(created);
        }

        public async Task<ConsultationResponseDto?> UpdateAsync(Guid id, ConsultationRequestDto dto)
        {
            var c = ConsultationMapper.ToEntity(dto);
            var updated = await _repository.UpdateAsync(id, c);
            return updated != null ? ConsultationMapper.ToDto(updated) : null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<CoutHoraireResponseDto?> GetCoutHoraireAsync(Guid id)
        {
            var c = await _repository.GetByIdAsync(id);
            return c != null ? ConsultationMapper.ToCoutHoraireDto(c) : null;
        }
    }
}
