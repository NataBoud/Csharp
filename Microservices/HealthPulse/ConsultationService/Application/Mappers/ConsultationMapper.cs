using ConsultationService.Application.DTOs;
using ConsultationService.Domain.Entities;

namespace ConsultationService.Application.Mappers
{
    public class ConsultationMapper
    {
        public static ConsultationResponseDto ToDto(Consultation c)
        {
            return new ConsultationResponseDto
            {
                Id = c.Id,
                PatientId = c.PatientId,
                Motif = c.Motif.ToString(),
                DateConsultation = c.DateConsultation,
                DureeMinutes = c.DureeMinutes,
                Tarif = c.Tarif
            };
        }

        public static Consultation ToEntity(ConsultationRequestDto dto)
        {
            return new Consultation
            {
                PatientId = dto.PatientId,
                Motif = Enum.Parse<MotifConsultation>(dto.Motif),
                DateConsultation = dto.DateConsultation,
                DureeMinutes = dto.DureeMinutes,
                Tarif = dto.Tarif
            };
        }

        public static CoutHoraireResponseDto ToCoutHoraireDto(Consultation c)
        {
            return new CoutHoraireResponseDto
            {
                ConsultationId = c.Id,
                Tarif = c.Tarif,
                DureeMinutes = c.DureeMinutes,
                CoutHoraire = c.CalculerCoutHoraire()
            };
        }

        public static IEnumerable<ConsultationResponseDto> ToDtoList(IEnumerable<Consultation> list)
        {
            return list.Select(ToDto);
        }


    }
}
