using PatientService.Domain.Entities;

namespace PatientService.Domain.Ports
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(Guid id);
        Task<Patient> CreateAsync(Patient patient);
        Task<Patient?> UpdateAsync(Guid id, Patient patient);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Patient>> GetByGroupeSanguinAsync(GroupeSanguin groupe);
    }
}
