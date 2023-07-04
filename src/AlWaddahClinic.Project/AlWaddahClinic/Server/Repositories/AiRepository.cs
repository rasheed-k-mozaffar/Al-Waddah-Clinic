using System;

namespace AlWaddahClinic.Server.Repositories
{
	public class AiRepository : IAiRepository
	{
        private const string baseUri = "https://func-waddah-clicnic-ai.azurewebsites.net/api/";
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AiRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }        

        /// <summary>
        /// This method gets all provided information for a particular case
        /// </summary>
        /// <param name="caseDescription"></param>
        /// <returns>A deserialized string containing the case insight</returns>
        /// <exception cref="UndefinedCaseException"></exception>
        public async Task<string[]> GetCaseInsightsAsync(string caseDescription)
        {
            //Check if the case provided is not an empty string
            if(string.IsNullOrEmpty(caseDescription))
            {
                throw new UndefinedCaseException("No case or case description was provided to complete the research");
            }

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{baseUri}/GetIllnessInfo?{_configuration["SmartInsightsCodes:CaseInsights"]}", new
            {
                message = caseDescription
            }) ;

            if(response.IsSuccessStatusCode)
            {
                string[]? results = await response.Content.ReadFromJsonAsync<string[]>();
                return results;
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        public async Task<string[]> GetRelatedMedicalCasesAsync(string caseDescription)
        {
            //Check if the case provided is not an empty string
            if (string.IsNullOrEmpty(caseDescription))
            {
                throw new UndefinedCaseException("No case or case description was provided to complete the research");
            }

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{baseUri}/TopRelatedMedicalCases?{_configuration["SmartInsightsCodes:RelatedMedicalCases"]}", new
            {
                message = caseDescription
            });

            if(response.IsSuccessStatusCode)
            {
                string[]? results = await response.Content.ReadFromJsonAsync<string[]>();
                return results;
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        public async Task<string[]> GetSuggestedMedicalTestsAsync(string caseDescription)
        {
            //Check if the case provided is not an empty string
            if (string.IsNullOrEmpty(caseDescription))
            {
                throw new UndefinedCaseException("No case or case description was provided to complete the research");
            }

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{baseUri}/GetTopMedicalTests?{_configuration["SmartInsightsCodes:SuggestedTests"]}", new
            {
                message = caseDescription
            });

            if (response.IsSuccessStatusCode)
            {
                string[]? results = await response.Content.ReadFromJsonAsync<string[]>();
                return results;
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        public async Task<string[]> GetSuggestionsForPatientAsync(string caseDescription)
        {
            //Check if the case provided is not an empty string
            if (string.IsNullOrEmpty(caseDescription))
            {
                throw new UndefinedCaseException("No case or case description was provided to complete the research");
            }

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{baseUri}/PatientSuggestions?{_configuration["SmartInsightsCodes:PatientSuggestions"]}", new
            {
                message = caseDescription
            });

            if (response.IsSuccessStatusCode)
            {
                string[]? results = await response.Content.ReadFromJsonAsync<string[]>();
                return results;
            }
            else
            {
                return Array.Empty<string>();
            }
        }
    }
}

