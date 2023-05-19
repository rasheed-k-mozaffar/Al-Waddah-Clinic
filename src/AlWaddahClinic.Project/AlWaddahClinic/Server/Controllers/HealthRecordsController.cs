using AlWaddahClinic.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlWaddahClinic.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HealthRecordsController : BaseController
    {
        private readonly IHealthRecordsRepository _healthRecordsRepository;
        private readonly ILogger<HealthRecordsController> _logger;

        public HealthRecordsController(IHealthRecordsRepository healthRecordsRepository, ILogger<HealthRecordsController> logger)
        {
            _healthRecordsRepository = healthRecordsRepository;
            _logger = logger;
        }

        // GET
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetAllForPatient(int patientId)
        {
            try
            {
                var recordsForPatient = await _healthRecordsRepository.GetHealthRecordsForPatientAsync(patientId);

                var recordsAsDtos = recordsForPatient.Select(hr => hr.ToHealthRecordDto());

                return Ok(new ApiResponse<IEnumerable<HealthRecordDto>>
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
        [HttpGet("/{patientId}/{recordId}")]
        public async Task<IActionResult> GetRecordById(int patientId, int recordId)
        {
            try
            {
                var recordForPatient = await _healthRecordsRepository.GetHealthRecordByIdAsync(patientId, recordId);

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
        public async Task<IActionResult> CreateRecord(int patientId, HealthRecordDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var healthRecord = model.ToHealthRecord();

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
        public async Task<IActionResult> UpdateRecord(int id)
        {
            throw new NotImplementedException();
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

