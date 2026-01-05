using PatientService.Application.DTOs;
using PatientService.Application.Mappers;
using PatientService.Domain.Ports;

namespace PatientService.Application.Services
{
    public class PatientAppService : IPatientAppService
    {
        private readonly IPatientRepository _repository;

        public PatientAppService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PatientResponseDto>> GetAllAsync()
        {
            var patients = await _repository.GetAllAsync();
            return PatientMapper.ToDtoList(patients);
        }

        public async Task<PatientResponseDto?> GetByIdAsync(Guid id)
        {
            var patient = await _repository.GetByIdAsync(id);
            return patient != null ? PatientMapper.ToDto(patient) : null;
        }

        public async Task<PatientResponseDto> CreateAsync(PatientRequestDto dto)
        {
            var patient = PatientMapper.ToEntity(dto);
            var created = await _repository.CreateAsync(patient);
            return PatientMapper.ToDto(created);
        }

        public async Task<PatientResponseDto?> UpdateAsync(Guid id, PatientRequestDto dto)
        {
            var patient = PatientMapper.ToEntity(dto);
            var updated = await _repository.UpdateAsync(id, patient);
            return updated != null ? PatientMapper.ToDto(updated) : null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
