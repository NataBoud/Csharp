using PrescriptionService.Domain.Entities;

namespace PrescriptionService.Domain.Ports
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task<Prescription?> GetByIdAsync(Guid id);
        Task<IEnumerable<Prescription>> GetByConsultationIdAsync(Guid consultationId);
        Task<Prescription> CreateAsync(Prescription prescription);
        Task<Prescription?> UpdateAsync(Guid id, Prescription prescription);
        Task<bool> DeleteAsync(Guid id);
    }
}
