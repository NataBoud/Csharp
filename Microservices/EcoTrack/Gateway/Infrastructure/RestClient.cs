using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gateway.Infrastructure
{
    public class RestClient<TGet, TPost>
    {
        private readonly string _BaseUrl;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public RestClient(string baseUrl)
        {
            _BaseUrl = baseUrl;
            _client = new HttpClient();

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _options.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<TGet> GetRequest(string url)
        {
            var response = await _client.GetAsync(_BaseUrl + url);
            if (!response.IsSuccessStatusCode) throw new Exception("Error while fetching ressource");

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TGet>(json, _options);

            return result ?? throw new Exception("Result Null");
        }


        public async Task<List<TGet>> GetListRequest(string url)
        {
            var response = await _client.GetAsync(_BaseUrl + url);
            if (!response.IsSuccessStatusCode) throw new Exception("Error while fetching ressource");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<TGet>>(json, _options);
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
            var result = JsonSerializer.Deserialize<TGet>(json, _options);
            return result ?? throw new Exception("Result null");
        }


        public async Task<TGet> PutRequest(string url, TPost putElement)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(putElement),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PutAsync(_BaseUrl + url, jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error while updating resource");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TGet>(json, _options);

            return result ?? throw new Exception("Result null");
        }

        public async Task<bool> DeleteRequest(string url)
        {
            var response = await _client.DeleteAsync(_BaseUrl + url);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return false;

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error while deleting resource");

            return true;
        }

        // Generic request for custom types
        public async Task<TCustom> GetRequest<TCustom>(string url)
        {
            var response = await _client.GetAsync(_BaseUrl + url);
            if (!response.IsSuccessStatusCode) throw new Exception("Error while fetching resource");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TCustom>(json, _options);
            return result ?? throw new Exception("Result Null");
        }
    }
}
