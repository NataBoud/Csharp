using ConsultationService.Domain.Entities;
using ConsultationService.Domain.Ports;
using ConsultationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsultationService.Infrastructure.Persistence
{
    public class EfConsultationRepository : IConsultationRepository
    {
        private readonly AppDbContext _context;

        public EfConsultationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consultation>> GetAllAsync()
        {
            return await _context.Consultations.ToListAsync();
        }

        public async Task<Consultation?> GetByIdAsync(Guid id)
        {
            return await _context.Consultations.FindAsync(id);
        }

        public async Task<IEnumerable<Consultation>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.Consultations
                                 .Where(c => c.PatientId == patientId)
                                 .ToListAsync();
        }

        public async Task<Consultation> CreateAsync(Consultation consultation)
        {
            consultation.Id = Guid.NewGuid();
            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();
            return consultation;
        }

        public async Task<Consultation?> UpdateAsync(Guid id, Consultation consultation)
        {
            var existing = await _context.Consultations.FindAsync(id);
            if (existing == null) return null;

            existing.PatientId = consultation.PatientId;
            existing.Motif = consultation.Motif;
            existing.DateConsultation = consultation.DateConsultation;
            existing.DureeMinutes = consultation.DureeMinutes;
            existing.Tarif = consultation.Tarif;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null) return false;

            _context.Consultations.Remove(consultation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
