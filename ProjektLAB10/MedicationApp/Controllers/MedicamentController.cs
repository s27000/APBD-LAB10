using MedicamentApp.Models.ContextModels;
using MedicamentApp.Models.RequestModels;
using MedicamentApp.Models.ResultModels;
using MedicamentApp.Services;
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
        private readonly IMedicamentService _medicamentService;
        public MedicamentController(IMedicamentService medicamentService)
        {
            _medicamentService = medicamentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(PrescriptionRequest prescriptionRequest)
        {
            try
            {
                await _medicamentService.AddPrescription(prescriptionRequest);
                return Ok("Prescription has been added");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("patient/{idPatient}")]
        public async Task<IActionResult> GetPatientData(int idPatient)
        {
            try
            {
                var result = await _medicamentService.GetPatientData(idPatient);
                return Ok(result);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
