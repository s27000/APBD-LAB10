using MedicamentApp.Models.ContextModels;
using MedicamentApp.Models.RequestModels;
using MedicamentApp.Models.ResultModels;
using MedicationApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicationApp.Controllers
{
    [ApiController]
    [Route("api/")]
    public class MedicamentController : ControllerBase
    {
        public MedicamentController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(PrescriptionRequest prescriptionRequest)
        {
            var dbContext = new MedicamentContext();

            if (prescriptionRequest.Medicaments.Count > 10)
            {
                return BadRequest("The amount of Medicaments cannot be greater than 10");
            }

            var findDoctor = await dbContext.Doctors
                .Where(e => e.IdDoctor.Equals(prescriptionRequest.IdDoctor))
                .FirstOrDefaultAsync();

            if (prescriptionRequest.DueDate <= prescriptionRequest.Date)
            {
                return BadRequest("DueDate must be greater than Date");
            }

            if (findDoctor == null)
            {
                return BadRequest("A doctor with such IdDoctor does not exist");
            }
            foreach(var medicament in prescriptionRequest.Medicaments)
            {
                var findMedicament = await dbContext.Medicaments
                    .Where(e => e.IdMedicament.Equals(medicament.IdMedicament))
                    .FirstOrDefaultAsync();

                if (findMedicament == null)
                {
                    return BadRequest("Medicament with IdMedicament = " + medicament.IdMedicament + " does not exist");
                }
            }

            var findPatient = await dbContext.Patients
                .Where(e => e.FirstName.Equals(prescriptionRequest.Patient.FirstName) &&
                    e.LastName.Equals(prescriptionRequest.Patient.LastName) &&
                    e.BirthDate.Equals(prescriptionRequest.Patient.BirthDate))
                .FirstOrDefaultAsync();

            int patientId;

            if (findPatient == null)
            {
                var newPatient = new Patient
                {
                    FirstName = prescriptionRequest.Patient.FirstName,
                    LastName = prescriptionRequest.Patient.LastName,
                    BirthDate = prescriptionRequest.Patient.BirthDate
                };

                await dbContext.Patients.AddAsync(newPatient);
                await dbContext.SaveChangesAsync();

                patientId = newPatient.IdPatient;
            }
            else
            {
                patientId = findPatient.IdPatient;
            }

            var newPrescription = new Prescription
            {
                Date = prescriptionRequest.Date,
                DueDate = prescriptionRequest.DueDate,
                IdPatient = patientId,
                IdDoctor = findDoctor.IdDoctor
            };
            await dbContext.Prescriptions.AddAsync(newPrescription);
            await dbContext.SaveChangesAsync();

            var newPrescriptionId = newPrescription.IdPrescription;

            foreach(var medicament in prescriptionRequest.Medicaments)
            {
                await dbContext.Prescription_Medicaments.AddAsync(new Prescription_Medicament
                {
                    IdMedicament = medicament.IdMedicament,
                    IdPrescription = newPrescriptionId,
                    Dose = medicament.Dose,
                    Details = medicament.Details
                });
                await dbContext.SaveChangesAsync();
            }

            return Ok("Prescription has been added");
        }

        [HttpGet("patient/{idPatient}")]
        public async Task<IActionResult> GetPatientData(int idPatient)
        {
            var result = new PatientResult();
            var dbContext = new MedicamentContext();

            var patient = await dbContext.Patients
                .Where(e => e.IdPatient.Equals(idPatient))
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return BadRequest("Patient does not exist");
            }

            result.IdPatient = patient.IdPatient;
            result.FirstName = patient.FirstName;
            result.LastName = patient.LastName;
            result.BirthDate = patient.BirthDate;

            var prescriptions = await dbContext.Prescriptions
                .Where(e => e.IdPatient
                .Equals(idPatient))
                .OrderBy(e => e.DueDate)
                .ToListAsync();

            var prescriptionsResultList = new List<PrescriptionResult>();
            foreach(var prescription in prescriptions)
            {
                var newPrescriptionResult = new PrescriptionResult()
                {
                    IdPrescription = prescription.IdPrescription,
                    Date = prescription.Date,
                    DueDate = prescription.DueDate,
                    Medicaments = await (from pm in dbContext.Prescription_Medicaments
                                         join m in dbContext.Medicaments
                                         on pm.IdMedicament equals m.IdMedicament
                                         where pm.IdPrescription == prescription.IdPrescription
                                         select new MedicamentResult
                                         {
                                             IdMedicament = pm.IdMedicament,
                                             Name = m.Name,
                                             Description = m.Description,
                                             Type = m.Type,
                                             Dose = pm.Dose,
                                             Details = pm.Details
                                         }).ToListAsync(),
                    Doctor = await dbContext.Doctors
                        .Where(e => e.IdDoctor.Equals(prescription.IdDoctor))
                        .FirstOrDefaultAsync()
                };

                prescriptionsResultList.Add(newPrescriptionResult);
            }
            result.Prescriptions = prescriptionsResultList;

            return Ok(result);
        }
    }
}
