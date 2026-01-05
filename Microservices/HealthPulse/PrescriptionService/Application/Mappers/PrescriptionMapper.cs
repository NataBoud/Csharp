using PrescriptionService.Domain.Entities;
using PrescriptionService.Application.DTOs;

namespace PrescriptionService.Application.Mappers
{
    public static class PrescriptionMapper
    {
        public static Prescription ToEntity(PrescriptionRequestDto dto)
        {
            return new Prescription
            {
                ConsultationId = dto.ConsultationId,
                Medicament = dto.Medicament,
                Dosage = dto.Dosage,
                Frequence = dto.Frequence,
                DureeJours = dto.DureeJours,
                Renouvelable = dto.Renouvelable
            };
        }

        public static PrescriptionResponseDto ToDto(Prescription prescription)
        {
            return new PrescriptionResponseDto
            {
                Id = prescription.Id,
                ConsultationId = prescription.ConsultationId,
                Medicament = prescription.Medicament,
                Dosage = prescription.Dosage,
                Frequence = prescription.Frequence,
                DureeJours = prescription.DureeJours,
                Renouvelable = prescription.Renouvelable
            };
        }

        public static TotalPrisesResponseDto ToTotalPrisesDto(Prescription prescription)
        {
            return new TotalPrisesResponseDto
            {
                PrescriptionId = prescription.Id,
                Medicament = prescription.Medicament,
                Frequence = prescription.Frequence,
                DureeJours = prescription.DureeJours,
                TotalPrises = prescription.CalculerTotalPrises()
            };
        }
    }
}
