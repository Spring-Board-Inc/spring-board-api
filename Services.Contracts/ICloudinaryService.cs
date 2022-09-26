using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface ICloudinaryService
    {
        Task<PhotoUploadResultDto> UploadPhoto(IFormFile photo);
        Task<bool> DeleteFile(string id);
    }
}
