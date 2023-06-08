using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlWaddahClinic.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AlWaddahClinic.Server.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlWaddahClinic.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PatientsController : BaseController
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController
            (IPatientsRepository patientsRepository, ILogger<PatientsController> logger, ClinicDbContext context) : base(context)
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

            return Ok(new ApiResponse<IEnumerable<PatientSummaryDto>>
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
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse { Message = ex.Message });
            }
        }

        [HttpGet("find")]
        public async Task<IActionResult> SearchPatients(string searchText)
        {
            try
            {
                var patients = await _patientsRepository.SearchAsync(searchText);
                var patientsAsDtos = patients.Select(p => p.ToPatientSummaryDto());

                return Ok(new ApiResponse<IEnumerable<PatientSummaryDto>>
                {
                    Message = "Patients retrieved successfully",
                    Value = patientsAsDtos,
                    IsSuccess = true
                });
            }
            catch(NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse
                {
                    Message = ex.Message
                });
            }
        }

        // POST 
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var patient = model.ToPatientCreate();

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
        public async Task<IActionResult> Put(int id, [FromBody] PatientUpdateDto model)
        {
            //TODO: Implement this endpoitn.
            if(ModelState.IsValid)
            {
                try
                {
                    var patientToUpdate = await _patientsRepository.GetPatientByIdAsync(id);

                    patientToUpdate.FullName = model.FullName;
                    patientToUpdate.EmailAddress = model.EmailAddress;
                    patientToUpdate.Address = model.Address;
                    patientToUpdate.PhoneNumber = model.PhoneNumber;
                    patientToUpdate.DateOfBirth = model.DateOfBirth;
                    patientToUpdate.Gender = model.Gender;
                    patientToUpdate.ModifiedByUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    patientToUpdate.ModifiedOn = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return NoContent(); //Return 204 STATUS CODE
                }
                catch(NotFoundException ex)
                {
                    return BadRequest(new ApiErrorResponse
                    {
                        Message = ex.Message,
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                await _patientsRepository.RemovePatient(id);
                _logger.LogInformation($"The patient with the ID: {id} was removed by the user with the ID: {HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value}");
                return Ok(new ApiResponse { Message = "Patient removed successfully", IsSuccess = true });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse { Message = ex.Message });
            }
        }
    }
}

