using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloud;
        public CloudinaryService(CloudinarySettings cloudSettings)
        {
             
            Account cloudAccount = new Account
            {
                ApiKey = cloudSettings.ApiKey,
                ApiSecret = cloudSettings.ApiSecret,
                Cloud = cloudSettings.CloudName
            };
            _cloud = new Cloudinary(cloudAccount);
        }

        public async Task<bool> DeleteFile(string id)
        {
            var deletionParams = new DeletionParams(id);
            deletionParams.ResourceType = ResourceType.Image;
            var delRes = await _cloud.DestroyAsync(deletionParams);
            if (delRes.StatusCode == System.Net.HttpStatusCode.OK && delRes.Result.ToLower() == "ok")
                return true;
            return false;
        }

        public async Task<PhotoUploadResultDto> UploadPhoto(IFormFile photo)
        {
            var imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(photo.FileName, photo.OpenReadStream()),
                Transformation = new Transformation().Width(300).Height(300).Gravity("faces").Crop("fill")
            };
            var res = await _cloud.UploadAsync(imageUploadParams);
            if (!(res.StatusCode == System.Net.HttpStatusCode.OK))
                return null;

            return new PhotoUploadResultDto(res.PublicId, res.Url.ToString());
        }
    }
}