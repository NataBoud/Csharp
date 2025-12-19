using System.Text;
using System.Text.Json;

namespace EcoDashboardService.Infrastructure.Clients
{
    public class RestClient<TGet, TPost>
    {
        private readonly string _BaseUrl;
        private readonly HttpClient _client;

        public RestClient(string baseUrl)
        {
            this._BaseUrl = baseUrl;
            _client = new HttpClient();
        }

        public async Task<TGet> GetRequest(string url)
        {
            var response = await _client.GetAsync(_BaseUrl + url);
            if (!response.IsSuccessStatusCode) throw new Exception("Error while fetching ressource");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TGet>(json);
            return result ?? throw new Exception("Result Null");
        }


        public async Task<List<TGet>> GetListRequest(string url)
        {
            var response = await _client.GetAsync(_BaseUrl + url);
            if (!response.IsSuccessStatusCode) throw new Exception("Error while fetching ressource");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<TGet>>(json);
            return result ?? throw new Exception("Result Null");
        }

        public async Task<TGet> PostRequest(string url, TPost postElement)
        {

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(postElement),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(_BaseUrl + url, jsonContent);

            if (!response.IsSuccessStatusCode) throw new Exception("Error while fetching ressource");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TGet>(json);
            return result ?? throw new Exception("Result null");
        }
    }
}
