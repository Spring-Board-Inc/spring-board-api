using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IUserService
    {
        public string? GetUserId();
        public string? GetUserEmail();
        Task<bool> IsInRole(string userId, string role);
        Task<ApiBaseResponse> Activate(string userid);
        Task<ApiBaseResponse> Deactivate(string UserId);
        Task<ApiBaseResponse> UploadUserPhoto(IFormFile photoToUpload, string userId);
        Task<ApiBaseResponse> UpdateUserPhoto(IFormFile model, string userId);
        Task<ApiBaseResponse> RemoveProfilePhoto(string userId);
        Task<ApiBaseResponse> Get(string id);
        Task<ApiBaseResponse> Get(SearchParameters searchParameters);
        Task<ApiBaseResponse> UpdateUserNames(string userId, UserNamesForUpdateDto request);
    }
}
