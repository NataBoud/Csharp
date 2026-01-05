using HealthDashboardService.Application.DTOs;
using HealthDashboardService.Infrastructure.HttpClients;
using static HealthDashboardService.Application.DTOs.ExternalDtos;

namespace HealthDashboardService.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IPatientServiceClient _patientClient;
        private readonly IConsultationServiceClient _consultationClient;
        private readonly IPrescriptionServiceClient _prescriptionClient;

        public DashboardService(
        IPatientServiceClient patientClient,
        IConsultationServiceClient consultationClient,
        IPrescriptionServiceClient prescriptionClient)
        {
            _patientClient = patientClient;
            _consultationClient = consultationClient;
            _prescriptionClient = prescriptionClient;
        }

        public async Task<DashboardResponseDto> GetDashboardAsync()
        {
            // Récupération parallèle des données
            var patientsTask = _patientClient.GetAllPatientsAsync();
            var consultationsTask = _consultationClient.GetAllConsultationsAsync();
            var prescriptionsTask = _prescriptionClient.GetAllPrescriptionsAsync();

            await Task.WhenAll(patientsTask, consultationsTask, prescriptionsTask);

            var patients = await patientsTask;
            var consultations = await consultationsTask;
            var prescriptions = await prescriptionsTask;

            return new DashboardResponseDto
            {
                TotalPatients = patients.Count,
                ConsultationsParType = consultations
                    .GroupBy(c => c.Motif)
                    .ToDictionary(g => g.Key, g => g.Count()),
                ChiffreAffairesTotal = consultations.Sum(c => c.Tarif),
                TotalPrescriptions = prescriptions.Count,
                PatientsParGroupeSanguin = patients
                    .GroupBy(p => p.GroupeSanguin)
                    .ToDictionary(g => g.Key, g => g.Count()),
                GenereLe = DateTime.UtcNow
            };
        }

        public async Task<PatientHistoriqueResponseDto?> GetPatientHistoriqueAsync(Guid patientId)
        {
            var patient = await _patientClient.GetPatientByIdAsync(patientId);
            if (patient == null) return null;

            var consultations = await _consultationClient.GetConsultationsByPatientIdAsync(patientId);

            var prescriptions = new List<PrescriptionDto>();
            foreach (var consultation in consultations)
            {
                var consultPrescriptions = await _prescriptionClient
                    .GetPrescriptionsByConsultationIdAsync(consultation.Id);
                prescriptions.AddRange(consultPrescriptions);
            }

            return new PatientHistoriqueResponseDto
            {
                Patient = patient,
                Consultations = consultations,
                Prescriptions = prescriptions
            };
        }
    }
}
