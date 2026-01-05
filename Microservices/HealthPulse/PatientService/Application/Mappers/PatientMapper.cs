using PatientService.Application.DTOs;
using PatientService.Domain.Entities;

namespace PatientService.Application.Mappers
{
    public class PatientMapper
    {
        public static PatientResponseDto ToDto(Patient patient)
        {
            return new PatientResponseDto
            {
                Id = patient.Id,
                Nom = patient.Nom,
                DateNaissance = patient.DateNaissance,
                GroupeSanguin = patient.GroupeSanguin.ToString(),
                DateInscription = patient.DateInscription,
                Age = patient.CalculerAge()
            };
        }

        public static Patient ToEntity(PatientRequestDto dto)
        {
            return new Patient
            {
                Nom = dto.Nom,
                DateNaissance = dto.DateNaissance,
                GroupeSanguin = Enum.Parse<GroupeSanguin>(dto.GroupeSanguin.Replace("+", "Positif").Replace("-", "Negatif"))
            };
        }

        public static IEnumerable<PatientResponseDto> ToDtoList(IEnumerable<Patient> patients)
        {
            return patients.Select(ToDto);
        }
    }
}
