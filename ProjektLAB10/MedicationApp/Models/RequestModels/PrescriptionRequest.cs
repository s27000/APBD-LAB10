namespace MedicamentApp.Models.RequestModels
{
    public class PrescriptionRequest
    {
        public PatientRequest Patient { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List<MedicamentRequest> Medicaments { get; set; }
        public int IdDoctor { get; set; }
    }
}
