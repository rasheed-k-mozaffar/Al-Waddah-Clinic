using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlWaddahClinic.Server.Data;
using Microsoft.AspNetCore.Mvc;
using AlWaddahClinic.Server.Extensions;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlWaddahClinic.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AnalysisController : BaseController
    {
        private readonly IAnalysisRepository _analysisRepository;

        public AnalysisController(ClinicDbContext context, IAnalysisRepository analysisRepository) : base(context)
        {
            _analysisRepository = analysisRepository;
        }

        [HttpGet("count-patients")]
        public async Task<IActionResult> GetPatientsCount() {
            List<double> result = await _analysisRepository.CountPatientsByGenderAsync();

            return Ok(new ApiResponse<List<double>> {
                Message = "Operation performed successfully",
                Value = result,
                IsSuccess = true
            });
        }

        [HttpGet("recently-added-patients/{numToGet}")]
        public async Task<IActionResult> GetRecentlyAddedPatients(int numToGet) {
            try {
                var result = await _analysisRepository.GetRecentlyAddedPatientsAsync(numberOfPatients: numToGet);
                var resultDtos = result.Select(p => p.ToPatientSummaryDto()).ToList();

                return Ok(new ApiResponse<List<PatientSummaryDto>> {
                    Message = "Patients retrieved successfully",
                    Value = resultDtos,
                    IsSuccess = true
                });
            }
            catch(InvalidDataException ex) {
                return BadRequest(new ApiErrorResponse {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("get-upcoming-appointments/{limit}")]
        public async Task<IActionResult> GetUpcomingAppointments(int limit) {
            try {
                var result = await _analysisRepository.GetUpcomingAppointmentsAsync(hoursRange: limit);
                var resultDtos = result.Select(a => a.ToAppointmentSummaryDto()).ToList();

                return Ok(new ApiResponse<List<AppointmentSummaryDto>> {
                    Message = "Appointments retrieved successfully",
                    Value = resultDtos,
                    IsSuccess = true
                });

            } catch(InvalidDataException ex) {
                return BadRequest(new ApiErrorResponse {
                    Message = ex.Message
                });
            }
        }
    }
}

