﻿namespace MedicamentApp.Models.ResultModels
{
    public class MedicamentResult
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? Dose { get; set; }
        public string Details { get; set; }
    }
}
