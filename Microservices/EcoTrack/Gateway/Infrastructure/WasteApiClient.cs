using Gateway.Application.DTO.WasteDto;

namespace Gateway.Infrastructure
{
    public class WasteApiClient
    {
        private readonly RestClient<WasteDtoSend, WasteDtoReceive> _client;

        public WasteApiClient(string baseUrl)
        {
            _client = new RestClient<WasteDtoSend, WasteDtoReceive>(baseUrl);
        }

        public async Task<List<WasteDtoSend>> GetAllAsync()
        {
            return await _client.GetListRequest("");
        }

        public async Task<WasteDtoSend> GetByIdAsync(Guid id)
        {
            return await _client.GetRequest(id.ToString());
        }


        public async Task<WasteDtoSend> PostAsync(WasteDtoReceive receive)
        {
            return await _client.PostRequest("", receive);
        }

        public async Task<WasteDtoSend> UpdateAsync(Guid id, WasteDtoReceive receive)
        {
            return await _client.PutRequest(id.ToString(), receive);
        }
    }
}
