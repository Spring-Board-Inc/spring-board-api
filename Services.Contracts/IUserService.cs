using Entities.Response;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IUserService
    {
        public string? GetUserId();
        public string? GetUserEmail();
        Task<IList<string>>? GetUserRoles(string userId);
        Task<UserClaimsDto> GetUserClaims();
        Task<bool> IsInRole(string userId, string role);
        Task<ApiBaseResponse> Activate(string userid);
        Task<ApiBaseResponse> Deactivate(string UserId);
        Task<ApiBaseResponse> Reactivate(string userId); 
        Task<ApiBaseResponse> Suspend(string UserId);
        Task<ApiBaseResponse> UploadUserPhoto(PhotoToUploadDto photoToUpload, string userId);
        Task<ApiBaseResponse> UpdateUserPhoto(PhotoToUploadDto model, string userId);
        Task<ApiBaseResponse> RemoveProfilePhoto(string userId);
        Task<ApiBaseResponse> Get(string id);
        ApiBaseResponse GetDetails(Guid id);
        ApiBaseResponse Get(SearchParameters searchParameters);
        Task<ApiBaseResponse> UpdateUserNames(string userId, UserNamesForUpdateDto request);
        Task<ApiBaseResponse> UpdateUserAddress(string userId, UserAddressForUpdateDto request);
    }
}
