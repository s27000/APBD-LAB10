using MedicamentApp.Models.ContextModels;

namespace MedicamentApp.Models.ResultModels
{
    public class PrescriptionResult
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List<MedicamentResult> Medicaments { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
