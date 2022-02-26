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

        public async Task<PinataData?> SubmitNFTImage(byte[] fileBytes, string fileName)
        {
            var request = new RestRequest("pinning/pinFileToIPFS");
            request.AddHeader("pinata_api_key", "023bb4f9c5955b149ad5");
            request.AddHeader("pinata_secret_api_key", "9ecc4d9ecb5bd4e8560f6b3521d0538f833bf4dd7d7b4189e54ac9abc8a3c50b");
            request.AddFile("file", fileBytes, fileName);
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
    }
}
