using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.Extensions;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly RepositoryContext _repositoryContext;
        const int PHOTO_MAX_ALLOWABLE_SIZE = 1000000;

        public UserService
        (
            ILoggerManager logger,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IConfiguration configuration,
            IRepositoryManager repositoryManager,
            ICloudinaryService cloudinaryService,
            RepositoryContext repositoryContext
        )
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _repositoryManager = repositoryManager;
            _cloudinaryService = cloudinaryService;
            _repositoryContext = repositoryContext;
        }

        public async Task<ApiBaseResponse> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var userToReturn = _mapper.Map<DetailedUserToReturnDto>(user);
            return new ApiOkResponse<DetailedUserToReturnDto>(userToReturn);
        }

        public async Task<ApiBaseResponse> Get(SearchParameters searchParameters)
        {
            var endDate = searchParameters.EndDate == DateTime.MaxValue ? searchParameters.EndDate : searchParameters.EndDate.AddDays(1);
            var users = await _repositoryContext.Users.AsQueryable()
                                    .Where(u => u.CreatedAt >= searchParameters.StartDate && u.CreatedAt <= endDate)
                                    .Search(searchParameters.SearchBy)
                                    .OrderByDescending(u => u.CreatedAt)
                                    .ThenBy(u => u.FirstName)
                                    .ToListAsync();

            var usersDto = _mapper.Map<IEnumerable<DetailedUserToReturnDto>>(users);
            var pagedDataList = PagedList<DetailedUserToReturnDto>.Paginate(usersDto, searchParameters.PageNumber, searchParameters.PageSize);

            return new ApiOkResponse<PaginatedListDto<DetailedUserToReturnDto>>(pagedDataList);
        }

        public async Task<ApiBaseResponse> Activate(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);
            
            user.IsActive = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new BadRequestResponse(ResponseMessages.UserUpdateFailed);

            return new ApiOkResponse<string>(ResponseMessages.UserActivationSuccessful);
        }

        public async Task<ApiBaseResponse> Deactivate(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new BadRequestResponse(ResponseMessages.UserUpdateFailed);

            return new ApiOkResponse<string>(ResponseMessages.UserDeactivationSuccessful);
        }

        public async Task<ApiBaseResponse> UploadUserPhoto(IFormFile photo, string userId)
        {
            if (photo.Length <= 0)
                return new BadRequestResponse(ResponseMessages.NoFileChosen);

            var validationResult = Commons.ValidateImageFile(photo);
            if (!validationResult.Successful)
                return new BadRequestResponse(validationResult.Message);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var uploadResult = await _cloudinaryService.UploadPhoto(photo);
            if(uploadResult == null)
                return new BadRequestResponse(ResponseMessages.PhotoUploadFailed);

            user.PhotoUrl = uploadResult.Url;
            user.PublicId = uploadResult.PublicId;
            user.UpdatedAt = DateTime.Now;
            await _userManager.UpdateAsync(user);

            return new ApiOkResponse<string>(ResponseMessages.PhotoUploadSuccessful);
        }

        public async Task<ApiBaseResponse> UpdateUserPhoto(IFormFile photo, string userId)
        {
            if (photo.Length <= 0)
                return new BadRequestResponse(ResponseMessages.NoFileChosen);

            var validationResult = Commons.ValidateImageFile(photo);
            if (!validationResult.Successful)
                return new BadRequestResponse(validationResult.Message);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var uploadResult = await _cloudinaryService.UploadPhoto(photo);
            if (uploadResult == null)
                return new BadRequestResponse(ResponseMessages.PhotoUploadFailed);

            await _cloudinaryService.DeleteFile(user.PublicId);

            user.PhotoUrl = uploadResult.Url;
            user.PublicId = uploadResult.PublicId;
            user.UpdatedAt = DateTime.Now;
            await _userManager.UpdateAsync(user);

            return new ApiOkResponse<string>(ResponseMessages.PhotoUpdateSuccessful);
        }

        public async Task<ApiBaseResponse> UpdateUserNames(string userId, UserNamesForUpdateDto request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var userForUpdate = _mapper.Map(request, user);
            await _userManager.UpdateAsync(userForUpdate);

            return new ApiOkResponse<string>(ResponseMessages.UserInfoUpdated);
        }

        public async Task<ApiBaseResponse> RemoveProfilePhoto(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var deleteResult = await _cloudinaryService.DeleteFile(user.PublicId);
            if (!deleteResult)
                return new BadRequestResponse(ResponseMessages.PhotoDeletionFailed);

            user.PhotoUrl = string.Empty;
            user.PublicId = string.Empty;
            user.UpdatedAt = DateTime.Now;
            await _userManager.UpdateAsync(user);
            return new ApiOkResponse<string>(ResponseMessages.PhotoDeletionSuccessful);
        }

        #region Private Methods
        //private ApiBaseResponse ValidateImageFile(IFormFile photo)
        //{
        //    var fileFormats = new string[] { ".png", ".jpg", ".jpeg" };
        //    var isCorrectFormat = false;
        //    foreach (var f in fileFormats)
        //    {
        //        if (photo.FileName.EndsWith(f))
        //        {
        //            isCorrectFormat = true;
        //            break;
        //        }
        //    }
        //    if (!isCorrectFormat)
        //        return new BadRequestResponse(ResponseMessages.InvalidImageFormat);

        //    if (photo.Length > PHOTO_MAX_ALLOWABLE_SIZE)
        //        return new BadRequestResponse(ResponseMessages.FileTooLarge);

        //    return new ApiOkResponse<bool>(true);
        //}
        #endregion
    }
}
