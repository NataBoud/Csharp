using Microsoft.EntityFrameworkCore;
using PatientService.Domain.Entities;
using PatientService.Domain.Ports;
using PatientService.Infrastructure.Data;

namespace PatientService.Infrastructure.Persistence
{
    public class EfPatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public EfPatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            patient.Id = Guid.NewGuid();
            patient.DateInscription = DateTime.UtcNow;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient?> UpdateAsync(Guid id, Patient patient)
        {
            var existing = await _context.Patients.FindAsync(id);
            if (existing == null) return null;

            existing.Nom = patient.Nom;
            existing.DateNaissance = patient.DateNaissance;
            existing.GroupeSanguin = patient.GroupeSanguin;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Patient>> GetByGroupeSanguinAsync(GroupeSanguin groupe)
        {
            return await _context.Patients
                                 .Where(p => p.GroupeSanguin == groupe)
                                 .ToListAsync();
        }
    }
}
