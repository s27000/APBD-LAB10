using MedicamentApp.Models.RequestModels;
using MedicamentApp.Models.ResultModels;
using Microsoft.AspNetCore.Mvc;

namespace MedicamentApp.Services
{
    public interface IMedicamentService
    {
        Task AddPrescription(PrescriptionRequest prescriptionRequest);
        Task<PatientResult> GetPatientData(int idPatient);
    }
}
