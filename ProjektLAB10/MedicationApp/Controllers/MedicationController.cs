using MedicationApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicationApp.Controllers
{
    [ApiController]
    [Route("api/")]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationService _medicationService;
        public MedicationController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }
    }
}
