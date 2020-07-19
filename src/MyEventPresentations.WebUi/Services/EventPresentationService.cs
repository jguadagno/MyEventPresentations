using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MyEventPresentations.Domain.Models;
using MyEventPresentations.WebUi.Models;

namespace MyEventPresentations.WebUi.Services
{
    public class EventPresentationService : IEventPresentationService
    {

        private readonly HttpClient _httpClient;
        private readonly Settings _settings;

        private readonly string _apiRoot;
        
        public EventPresentationService(HttpClient httpClient, Settings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            
            _apiRoot = _settings.ApiRootUri + "presentations/";
        }

        public async Task<Presentation> SavePresentationAsync(Presentation presentation)
        {
            var url = $"{_apiRoot}";
            var jsonRequest = JsonSerializer.Serialize(presentation);
            var jsonContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);

            if (response.StatusCode != HttpStatusCode.Created)
                throw new HttpRequestException(
                    $"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            presentation = JsonSerializer.Deserialize<Presentation>(content, options);
            return presentation;
        }

        public async Task<Presentation> GetPresentationAsync(int presentationId)
        {
            var url = $"{_apiRoot}{presentationId}";
            return await ExecuteGetAsync<Presentation>(url);
        }

        public async Task<IEnumerable<Presentation>> GetPresentationsAsync()
        {
            var url = $"{_apiRoot}";
            return await ExecuteGetAsync<List<Presentation>>(url);
        }

        public async Task<IEnumerable<ScheduledPresentation>> GetScheduledPresentationsForPresentationAsync(
            int presentationId)
        {
            var url = $"{_apiRoot}{presentationId}/schedules";
            return await ExecuteGetAsync<List<ScheduledPresentation>>(url);
        }

        public async Task<bool> DeletePresentationAsync(int id)
        {
            var url = $"{_apiRoot}{id}";
            var response = await _httpClient.DeleteAsync(url);
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<ScheduledPresentation> GetScheduledPresentationAsync(int scheduledPresentationId)
        {
            var url = $"{_settings.ApiRootUri}scheduledPresentations/{scheduledPresentationId}";
            return await ExecuteGetAsync<ScheduledPresentation>(url);
        }

        public async Task<ScheduledPresentation> SaveScheduledPresentationAsync(ScheduledPresentation scheduledPresentation)
        {
            var url = $"{_settings.ApiRootUri}scheduledPresentations/";
            var jsonRequest = JsonSerializer.Serialize(scheduledPresentation);
            var jsonContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);

            if (response.StatusCode != HttpStatusCode.Created)
                throw new HttpRequestException(
                    $"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            scheduledPresentation = JsonSerializer.Deserialize<ScheduledPresentation>(content, options);
            return scheduledPresentation;
        }

        private async Task<T> ExecuteGetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return default;
            }
            
            // Parse the Results
            var content = await response.Content.ReadAsStringAsync();
                
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var results = JsonSerializer.Deserialize<T>(content, options);

            return results;
        }
    }
}