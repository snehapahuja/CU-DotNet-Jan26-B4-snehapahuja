using System.Net.Http.Json;
using VagaBond.Frontend.Services;
using VagaBond.Frontend.Models;

namespace VagaBond.Web.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly HttpClient _httpClient;

        public DestinationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Destination>>("api/destinations");
        }
    }
}