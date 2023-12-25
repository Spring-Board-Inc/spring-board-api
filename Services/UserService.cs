using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Extensions;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;
using System.Security.Claims;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositoryManager repositoryManager;

        public UserService
        (
            IMapper mapper,
            UserManager<AppUser> userManager,
            ICloudinaryService cloudinaryService,
            IHttpContextAccessor httpContextAccessor,
            IRepositoryManager repositoryManager
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
            _httpContextAccessor = httpContextAccessor;
            this.repositoryManager = repositoryManager;
        }

        public async Task<ApiBaseResponse> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var userToReturn = _mapper.Map<DetailedUserToReturnDto>(user);
            return new ApiOkResponse<DetailedUserToReturnDto>(userToReturn);
        }

        public async Task<ApiBaseResponse> GetDetails(Guid id)
        {
            var user = _userManager.Users
                .Where(u => u.Id.Equals(id))
                .FirstOrDefault();

            var userInfo = await repositoryManager.UserInformation.GetByUserIdAsync(id);
            if (user == null || userInfo == null)
                return new BadRequestResponse(ResponseMessages.UserNotFound);

            var careerSummary = await repositoryManager.CareerSummary.FindAsync(x => x.UserId.Equals(id));
            var education = repositoryManager.Education.FindByUserInfoId(userInfo.Id);
            var experience = repositoryManager.WorkExperience.FindByUserInfoId(userInfo.Id);
            var skills = repositoryManager.UserSkill.FindAsList(userInfo.Id);
            var certifications = repositoryManager.Certification
                .FindByUserInfoIdAsQueryable(userInfo.Id)
                .ToList();

            var data = _mapper.Map<ApplicantInformation>(user);
            data.CareerSummary = careerSummary.Summary;
            data.Educations = _mapper.Map<ICollection<EducationMinInfo>>(education);
            data.WorkExperiences = _mapper.Map<ICollection<WorkExperienceMinInfo>>(experience);
            data.Certifications = _mapper.Map<ICollection<CertificationMinInfo>>(certifications);
            data.UserSkills = _mapper.Map<ICollection<UserSkillMinInfo>>(skills);

            return new ApiOkResponse<ApplicantInformation>(data);
        }

        public ApiBaseResponse Get(SearchParameters searchParameters)
        {
            var endDate = searchParameters.EndDate == DateTime.MaxValue ? searchParameters.EndDate : searchParameters.EndDate.AddDays(1);
            var users = _userManager.Users.AsQueryable()
                                    .Where(u => u.CreatedOn >= searchParameters.StartDate && u.CreatedOn <= endDate)
                                    .Search(searchParameters.SearchBy)
                                    .OrderByDescending(u => u.CreatedOn)
                                    .ThenBy(u => u.FirstName)
                                    .ToList();

            var pagedList = PagedList<AppUser>.ToPagedList(users, searchParameters.PageNumber, searchParameters.PageSize);

            var usersDto = _mapper.Map<IEnumerable<DetailedUserToReturnDto>>(pagedList);
            var pagedDataList = PaginatedListDto<DetailedUserToReturnDto>.Paginate(usersDto, pagedList.MetaData);

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

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Reactivate(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            user.IsActive = true;
            user.IsDeprecated = false;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new BadRequestResponse(ResponseMessages.UserUpdateFailed);

            return new ApiOkResponse<bool>(true);
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

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Suspend(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            user.IsActive = false;
            user.IsDeprecated = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new BadRequestResponse(ResponseMessages.UserUpdateFailed);

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> UploadUserPhoto(PhotoToUploadDto photo, string userId)
        {
            if (!photo.IsValidParams)
                return new BadRequestResponse(ResponseMessages.NoFileChosen);

            var validationResult = Commons.ValidateImageFile(photo.Photo);
            if (!validationResult.Successful)
                return new BadRequestResponse(validationResult.Message);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var uploadResult = await _cloudinaryService.UploadPhoto(photo.Photo);
            if(uploadResult == null)
                return new BadRequestResponse(ResponseMessages.PhotoUploadFailed);

            user.PhotoUrl = uploadResult.Url;
            user.PublicId = uploadResult.PublicId;
            user.UpdatedAt = DateTime.Now;
            await _userManager.UpdateAsync(user);

            return new ApiOkResponse<string>(ResponseMessages.PhotoUploadSuccessful);
        }

        public async Task<ApiBaseResponse> UpdateUserPhoto(PhotoToUploadDto photo, string userId)
        {
            if (!photo.IsValidParams)
                return new BadRequestResponse(ResponseMessages.NoFileChosen);

            var validationResult = Commons.ValidateImageFile(photo.Photo);
            if (!validationResult.Successful)
                return new BadRequestResponse(validationResult.Message);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var uploadResult = await _cloudinaryService.UploadPhoto(photo.Photo);
            if (uploadResult == null)
                return new BadRequestResponse(ResponseMessages.PhotoUploadFailed);

            if (!string.IsNullOrEmpty(user.PublicId))
            {
                await _cloudinaryService.DeleteFile(user.PublicId);
            }

            user.PhotoUrl = uploadResult.Url;
            user.PublicId = uploadResult.PublicId;
            user.UpdatedAt = DateTime.UtcNow;
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
            userForUpdate.UpdatedAt = DateTime.Now;
            await _userManager.UpdateAsync(userForUpdate);

            var userToReturn = _mapper.Map<DetailedUserToReturnDto>(userForUpdate);
            return new ApiOkResponse<DetailedUserToReturnDto>(userToReturn);
        }

        public async Task<ApiBaseResponse> UpdateUserAddress(string userId, UserAddressForUpdateDto request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new NotFoundResponse(ResponseMessages.UserNotFound);

            var userForUpdate = _mapper.Map(request, user);
            userForUpdate.UpdatedAt = DateTime.Now;
            await _userManager.UpdateAsync(userForUpdate);

            var userToReturn = _mapper.Map<DetailedUserToReturnDto>(userForUpdate);
            return new ApiOkResponse<DetailedUserToReturnDto>(userToReturn);
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

        public async Task<UserClaimsDto> GetUserClaims()
        {
            var userId = GetUserId();

            return new UserClaimsDto
            {
                UserId = userId,
                Email = GetUserEmail(),
                Roles = await GetUserRoles(userId)
            };
        }
        public string? GetUserId()
        {
            ClaimsPrincipal? userClaim = _httpContextAccessor.HttpContext?.User;
            return userClaim?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string? GetUserEmail()
        {
            ClaimsPrincipal? userClaim = _httpContextAccessor.HttpContext?.User;
            return userClaim?.FindFirstValue(ClaimTypes.Name);
        }

        public async Task<IList<string>>? GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null ? await _userManager.GetRolesAsync(user) : null;
        }

        public async Task<bool> IsInRole(string userId, string role)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
