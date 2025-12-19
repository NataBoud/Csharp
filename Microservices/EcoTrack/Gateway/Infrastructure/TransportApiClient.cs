using Gateway.Application.DTO.TransportDto;

namespace Gateway.Infrastructure
{
    public class TransportApiClient
    {
        private readonly RestClient<TransportDtoSend, TransportDtoReceive> _client;
        private readonly RestClient<TransportEmissionDtoSend, object> _emissionClient;

        public TransportApiClient(string baseUrl)
        {
            _client = new RestClient<TransportDtoSend, TransportDtoReceive>(baseUrl);
            _emissionClient = new RestClient<TransportEmissionDtoSend, object>(baseUrl);
        }

        public async Task<List<TransportDtoSend>> GetAllAsync()
        {
            return await _client.GetListRequest("");
        }

        public async Task<TransportDtoSend> GetByIdAsync(Guid id)
        {
            return await _client.GetRequest(id.ToString());
        }


        public async Task<TransportDtoSend> PostAsync(TransportDtoReceive receive)
        {
            return await _client.PostRequest("", receive);
        }

        public async Task<TransportDtoSend> UpdateAsync(Guid id, TransportDtoReceive receive)
        {
            return await _client.PutRequest(id.ToString(), receive);
        }

        // DELETE: api/transport/{id}
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _client.DeleteRequest(id.ToString());
        }

        public async Task<TransportEmissionDtoSend> GetEmissionByIdAsync(Guid id)
        {
            return await _emissionClient.GetRequest<TransportEmissionDtoSend>($"{id}/emission");
        }



    }
}
