using LoopPin.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LoopPin.Services
{
    public class PinataService : IPinataService, IDisposable
    {
        const string _baseUrl = "https://api.pinata.cloud";

        readonly RestClient _client;

        public PinataService()
        {
            _client = new RestClient(_baseUrl);
        }

        public async Task<PinataData?> SubmitPin(byte[] fileBytes, string fileName, bool wrapDirectory = false, string metadataGuid = null)
        {
            var request = new RestRequest("pinning/pinFileToIPFS");
            request.AddHeader("pinata_api_key", ApiKeyHelper._apiKey);
            request.AddHeader("pinata_secret_api_key", ApiKeyHelper._apiKeySecret);
            request.AddFile("file", fileBytes, fileName);
            if(wrapDirectory == true)
            {
                request.AddParameter("pinataOptions", "{\"wrapWithDirectory\" :true}");
                request.AddParameter("pinataMetadata", metadataGuid);
            }
            try
            {
                var response = await _client.PostAsync(request);
                var data = JsonConvert.DeserializeObject<PinataData>(response.Content!);
                return data;
            }
            catch(HttpRequestException httpException)
            {
                return null;
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
