namespace MedicamentApp.Models.ResultModels
{
    public class PatientResult
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<PrescriptionResult> Prescriptions { get; set; }
    }
}
