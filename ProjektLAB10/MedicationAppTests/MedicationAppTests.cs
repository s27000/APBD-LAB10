using MedicamentApp.Models.RequestModels;
using MedicamentApp.Services;
using MedicationApp.Controllers;

namespace MedicationAppTests
{
    [TestClass]
    public class MedicationAppTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task AddPrescription_Should_Throw_Exception_When_More_Than_10_Medicaments()
        {
            var service = new MedicamentService();
            await service.AddPrescription(new PrescriptionRequest
            {
                Patient = new PatientRequest
                {
                    FirstName = "string",
                    LastName = "string",
                    BirthDate = DateTime.Now
                },
                Date = DateTime.Now,
                DueDate = DateTime.Now,
                Medicaments = new List<MedicamentRequest>
                {
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                    new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                    },
                },
                IdDoctor = 1
            });
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task AddPrescription_Should_Throw_Exception_When_DueDate_Smaller_Than_Date()
        {
            var service = new MedicamentService();

            await service.AddPrescription(new PrescriptionRequest
            {
                Patient = new PatientRequest
                {
                    FirstName = "string",
                    LastName = "string",
                    BirthDate = DateTime.Now
                },
                Date = DateTime.Parse("2024-03-03"),
                DueDate = DateTime.Parse("2023-03-03"),
                Medicaments = new List<MedicamentRequest>
                    {
                        new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                        }
                    },
                IdDoctor = 1
            });
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task AddPresciption_Should_Throw_Exception_When_Doctor_Doesnt_Exist()
        {
            var service = new MedicamentService();

            await service.AddPrescription(new PrescriptionRequest
            {
                Patient = new PatientRequest
                {
                    FirstName = "string",
                    LastName = "string",
                    BirthDate = DateTime.Now
                },
                Date = DateTime.Now,
                DueDate = DateTime.Now,
                Medicaments = new List<MedicamentRequest>
                    {
                        new MedicamentRequest{
                        IdMedicament = 1,
                        Dose = 0,
                        Details = "string"
                        }
                    },
                IdDoctor = -1
            });
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task AddPresciption_Should_Throw_Exception_When_Medicament_Doesnt_Exist()
        {
            var service = new MedicamentService();

            await service.AddPrescription(new PrescriptionRequest
            {
                Patient = new PatientRequest
                {
                    FirstName = "string",
                    LastName = "string",
                    BirthDate = DateTime.Now
                },
                Date = DateTime.Now,
                DueDate = DateTime.Now,
                Medicaments = new List<MedicamentRequest>
                    {
                        new MedicamentRequest{
                        IdMedicament = -1,
                        Dose = 0,
                        Details = "string"
                        }
                    },
                IdDoctor = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetPatientData_Should_Throw_Exception_When_Patient_Doesn_Exist()
        {
            var service = new MedicamentService();

            await service.GetPatientData(-1);
        }
    }
}