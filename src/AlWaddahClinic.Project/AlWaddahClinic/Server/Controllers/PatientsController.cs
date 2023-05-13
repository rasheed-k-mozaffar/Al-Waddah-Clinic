using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlWaddahClinic.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlWaddahClinic.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PatientsController : Controller
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientsRepository patientsRepository, ILogger<PatientsController> logger)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientsRepository.GetAllPatientsAsync();

            var patientsAsDtos = patients.Select(p => p.ToPatientSummaryDto());

            return Ok(new ApiResponse<IEnumerable<PatientSummaryDto>>       // return Ok(patientsAsDtos);
            {
                Message = "Patients retrieved successfully",
                Value = patientsAsDtos,
                IsSuccess = true
            }); 
        }

        // GET 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var patient = await _patientsRepository.GetPatientByIdAsync(id);

                var patientDto = patient.ToPatientDto();

                return Ok(new ApiResponse<PatientDto>
                {
                    Message = "Patient retrieved successfully",
                    Value = patientDto,
                    IsSuccess = true
                });
            }
            catch(NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse { Message = ex.Message });
            }
        }

        // POST 
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody]PatientCreateDto model)
        {
            if(ModelState.IsValid)
            {
                var patient = model.ToPatient();

                AssignAdminstrativeProperties<Patient>(patient);

                _logger.LogInformation($"The patient with the ID: {patient.Id} was addded by the user with the ID: {patient.CreatedByUserId}");

                await _patientsRepository.AddPatientAsync(patient);

                return Ok(new ApiResponse { Message = "Patient added successfully", IsSuccess = true });
            }
            else
            {
                return BadRequest(new ApiErrorResponse { Message = "Invalid details" });
            }
        }

        // PUT 
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                await _patientsRepository.RemovePatient(id);

                return Ok(new ApiResponse { Message = "Patient removed successfully", IsSuccess = true });
            }
            catch(NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse { Message = ex.Message });
            }
        }

        private void AssignAdminstrativeProperties<T>(T obj) where T : Base
        {
            obj.CreatedByUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            obj.CreatedOn = DateTime.Now;
        }
    }
}

