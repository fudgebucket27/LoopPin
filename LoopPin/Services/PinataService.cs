using LoopPin.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LoopPin.Services
{
    public class PinataService
    {
        const string _baseUrl = "https://api.pinata.cloud";

        readonly RestClient _client;

        public PinataService()
        {
            _client = new RestClient(_baseUrl);
        }

        public async Task<PinataData> SubmitNFTImage(byte[] fileBytes, string fileName)
        {
            var request = new RestRequest("pinning/pinFileToIPFS");
            request.AddHeader("pinata_api_key", Environment.GetEnvironmentVariable("APPSETTING_APIKEY"));
            request.AddHeader("pinata_secret_api_key", Environment.GetEnvironmentVariable("APPSETTING_APISECRET"));
            request.AddFile("file", fileBytes, fileName);
            var response = await _client.PostAsync(request);
            var data = JsonConvert.DeserializeObject<PinataData>(response.Content!);
            return data;
        }
    }
}
