using ConsultationService.Domain.Entities;

namespace ConsultationService.Domain.Ports
{
    public interface IConsultationRepository
    {
        Task<IEnumerable<Consultation>> GetAllAsync();
        Task<Consultation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Consultation>> GetByPatientIdAsync(Guid patientId);
        Task<Consultation> CreateAsync(Consultation consultation);
        Task<Consultation?> UpdateAsync(Guid id, Consultation consultation);
        Task<bool> DeleteAsync(Guid id);
    }
}
