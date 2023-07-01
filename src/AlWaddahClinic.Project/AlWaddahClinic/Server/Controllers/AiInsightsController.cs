using System;
using AlWaddahClinic.Server.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace AlWaddahClinic.Server.Controllers
{
	[ApiController]
	[Route("/api/insights")]
	//[Authorize(Roles = "Admin")]
	public class AiInsightsController : BaseController
	{
		private readonly IAiRepository _aiRepository;
        public AiInsightsController(ClinicDbContext context, IAiRepository aiRepository) : base(context)
        {
           _aiRepository = aiRepository;
        }

        [HttpPost("IllnessInfo")]
		public async Task<IActionResult> GetIllnessInfo([FromBody] CaseDto model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//Send a post request with the case description and get the related information to a given case.
					var insights = await _aiRepository.GetCaseInsightsAsync(model.message);

					return Ok(new ApiResponse<string[]>
					{
						Message = "Insights retrieved were successfully",
						Value = insights,
						IsSuccess = true
					});
				}
				catch(UndefinedCaseException ex)
				{
					return BadRequest(new ApiErrorResponse
					{
						Message = ex.Message
					});
				}
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

        [HttpPost("SuggestedTests")]
        public async Task<IActionResult> GetSuggestedMedicalTests([FromBody] CaseDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Send a post request with the case description and get suggestions for medical tests
                    var suggestedTests = await _aiRepository.GetSuggestedMedicalTestsAsync(model.message);

                    return Ok(new ApiResponse<string[]>
                    {
                        Message = "Suggested tests were retrieved successfully",
                        Value = suggestedTests,
                        IsSuccess = true
                    });
                }
                catch (UndefinedCaseException ex)
                {
                    return BadRequest(new ApiErrorResponse
                    {
                        Message = ex.Message
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("PatientSuggestions")]
        public async Task<IActionResult> GetSuggestionsForPatient([FromBody] CaseDto model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //Send the case description and get suggestions for the patient.
                    var suggestions = await _aiRepository.GetSuggestionsForPatientAsync(model.message);

                    return Ok(new ApiResponse<string[]>
                    {
                        Message = "Suggestions for patient were retrieved successfully",
                        Value = suggestions,
                        IsSuccess = true
                    });
                }
                catch(UndefinedCaseException ex)
                {
                    return BadRequest(new ApiErrorResponse
                    {
                        Message = ex.Message
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("RelatedMedicalCases")]
        public async Task<IActionResult> GetRelatedMedicalCases([FromBody] CaseDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Send the case description and get related medical cases to the case described
                    var relatedCases = await _aiRepository.GetRelatedMedicalCasesAsync(model.message);

                    return Ok(new ApiResponse<string[]>
                    {
                        Message = "Suggestions for patient were retrieved successfully",
                        Value = relatedCases,
                        IsSuccess = true
                    });
                }
                catch (UndefinedCaseException ex)
                {
                    return BadRequest(new ApiErrorResponse
                    {
                        Message = ex.Message
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

