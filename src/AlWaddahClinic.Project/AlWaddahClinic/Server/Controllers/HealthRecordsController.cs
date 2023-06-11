using System.Security.Claims;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlWaddahClinic.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HealthRecordsController : BaseController
    {
        private readonly IHealthRecordsRepository _healthRecordsRepository;
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<HealthRecordsController> _logger;

        public HealthRecordsController
            (IHealthRecordsRepository healthRecordsRepository, ILogger<HealthRecordsController> logger, ClinicDbContext context) : base(context)
        {
            _healthRecordsRepository = healthRecordsRepository;
            _logger = logger;
        }

        // GET
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetAllForPatient(Guid patientId)
        {
            try
            {
                var recordsForPatient = await _healthRecordsRepository.GetHealthRecordsForPatientAsync(patientId);

                var recordsAsDtos = recordsForPatient.Select(hr => hr.ToHealthRecordSummaryDto());

                return Ok(new ApiResponse<IEnumerable<HealthRecordSummaryDto>>
                {
                    Message = "Records retrieved successfully",
                    Value = recordsAsDtos,
                    IsSuccess = true
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse { Message = ex.Message });
            }
        }

        // GET
        [HttpGet("record/{id}")]
        public async Task<IActionResult> GetRecordById(Guid id)
        {
            try
            {
                var recordForPatient = await _healthRecordsRepository.GetHealthRecordByIdAsync(id);

                var recordAsDto = recordForPatient.ToHealthRecordDto();

                return Ok(new ApiResponse<HealthRecordDto>
                {
                    Message = "Record retrieved successfully",
                    Value = recordAsDto,
                    IsSuccess = true
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse { Message = ex.Message });
            }
        }

        // POST
        [HttpPost("{patientId}")]
        public async Task<IActionResult> CreateRecord(Guid patientId, HealthRecordCreateDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var healthRecord = model.ToHealthRecordCreate();

                    //Assign the properties from the base class.
                    AssignAdminstrativeProperties<HealthRecord>(healthRecord);

                    await _healthRecordsRepository.AddHealthRecordAsync(patientId, healthRecord);

                    _logger.LogInformation($"Health record for patient with ID: {patientId} was added by the user with ID: {healthRecord.CreatedByUserId}");


                    return Ok(new ApiResponse
                    {
                        Message = "Health record was added successfully",
                        IsSuccess = true
                    });
                }
                catch (NotFoundException ex)
                {
                    return BadRequest(new ApiErrorResponse { Message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new ApiErrorResponse { Message = "Invalid details" });
            }
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(Guid id, [FromBody] HealthRecordUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var recordToUpdate = await _healthRecordsRepository.GetHealthRecordByIdAsync(id);

                    recordToUpdate.Description = model.Description;

                    if (recordToUpdate.Notes != null)
                    {
                        _context.Notes.RemoveRange(recordToUpdate.Notes);
                        recordToUpdate.Notes = model.Notes.Select(n => n.ToNoteUpdate()).ToList();
                    }
                    else
                    {
                        recordToUpdate.Notes = model.Notes.Select(n => n.ToNoteUpdate()).ToList();
                    }

                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                catch (NotFoundException ex)
                {
                    return BadRequest(new ApiResponse
                    {
                        Message = ex.Message,
                        IsSuccess = false
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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _healthRecordsRepository.RemoveHealthRecord(id);
                _logger.LogInformation($"Health record with ID: {id} was removed by user with ID: {HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)}");
                return Ok(new ApiResponse
                {
                    Message = "Health record was removed successfully",
                    IsSuccess = true
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiResponse
                {
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }
    }
}

