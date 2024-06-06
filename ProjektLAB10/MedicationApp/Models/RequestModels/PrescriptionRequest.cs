using System.ComponentModel.DataAnnotations;

namespace MedicamentApp.Models.RequestModels
{
    public class PrescriptionRequest
    {
        public PatientRequest Patient { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List<MedicamentRequest> Medicaments { get; set; }
        public int IdDoctor { get; set; }

        public void VerifyBody()
        {
            if (IsMedicamentsGreaterThan10())
            {
                throw new Exception("The amount of Medicaments cannot be greater than 10");
            }
            if (!IsDueDateGreaterThanDate())
            {
                throw new Exception("DueDate must be greater than Date");
            }
        }

        public bool IsMedicamentsGreaterThan10()
        {
            if (Medicaments.Count() > 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsDueDateGreaterThanDate()
        {
            if (DueDate >= Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
