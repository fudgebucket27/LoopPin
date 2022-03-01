using LoopPin.Models;

namespace LoopPin.Services
{
    public interface IPinataService
    {
        Task<PinataData?> SubmitPin(byte[] fileBytes, string fileName, bool wrapDirectory = false, string metadataGuid = null);
    }
}
