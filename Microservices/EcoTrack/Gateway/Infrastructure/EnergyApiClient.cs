using Gateway.Application.DTO.EnergyDto;

namespace Gateway.Infrastructure
{
    public class EnergyApiClient
    {
        private readonly RestClient<EnergyDtoSend, EnergyDtoReceive> _client;

        public EnergyApiClient(string baseUrl)
        {
            _client = new RestClient<EnergyDtoSend, EnergyDtoReceive>(baseUrl);
        }

        public async Task<List<EnergyDtoSend>> GetAllAsync()
        {
            return await _client.GetListRequest("");
        }

        public async Task<EnergyDtoSend> GetByIdAsync(Guid id)
        {
            return await _client.GetRequest(id.ToString());
        }


        public async Task<EnergyDtoSend> PostAsync(EnergyDtoReceive receive)
        {
            return await _client.PostRequest("", receive);
        }
    }
}
