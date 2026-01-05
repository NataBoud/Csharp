using Microsoft.EntityFrameworkCore;
using PrescriptionService.Domain.Entities;
using PrescriptionService.Domain.Ports;
using PrescriptionService.Infrastructure.Data;

namespace PrescriptionService.Infrastructure.Persistence
{
    public class EfPrescriptionRepository : IPrescriptionRepository
    {
        private readonly AppDbContext _context;

        public EfPrescriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prescription>> GetAllAsync()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        public async Task<Prescription?> GetByIdAsync(Guid id)
        {
            return await _context.Prescriptions.FindAsync(id);
        }

        public async Task<IEnumerable<Prescription>> GetByConsultationIdAsync(Guid consultationId)
        {
            return await _context.Prescriptions
                                 .Where(p => p.ConsultationId == consultationId)
                                 .ToListAsync();
        }

        public async Task<Prescription> CreateAsync(Prescription prescription)
        {
            prescription.Id = Guid.NewGuid();
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<Prescription?> UpdateAsync(Guid id, Prescription prescription)
        {
            var existing = await _context.Prescriptions.FindAsync(id);
            if (existing == null) return null;

            existing.Medicament = prescription.Medicament;
            existing.Dosage = prescription.Dosage;
            existing.Frequence = prescription.Frequence;
            existing.DureeJours = prescription.DureeJours;
            existing.Renouvelable = prescription.Renouvelable;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return false;

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
