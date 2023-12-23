using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Services.Extensions;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinary;


        public CompanyService(IRepositoryManager repository, IMapper mapper, ICloudinaryService cloudinary)
        {
            _repository = repository;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<ApiBaseResponse> Create(string userId, CompanyRequestObject request)
        {
            if(!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var uploadResult = new PhotoUploadResultDto(string.Empty, string.Empty);

            if(request.IsValidFile)
            {
                var validationResult = Commons.ValidateImageFile(request.Logo);
                if (!validationResult.Successful)
                    return new BadRequestResponse(validationResult.Message);
                try
                {
                    uploadResult = await _cloudinary.UploadPhoto(request.Logo);
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
                if (uploadResult == null)
                    return new BadRequestResponse(ResponseMessages.PhotoUploadFailed);
            }

            var company = _mapper.Map<Company>(request);
            company.LogoUrl = uploadResult.Url;
            company.PublicId = uploadResult.PublicId;
            company.UserId = userId.StringToGuid();

            await _repository.Company.AddAsync(company);

            var companyToReturn = _mapper.Map<CompanyToReturnDto>(company);
            return new ApiOkResponse<CompanyToReturnDto>(companyToReturn);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var company = await _repository.Company.FindAsync(id);
            if (company == null)
                return new NotFoundResponse(ResponseMessages.CompanyNotFound);

            var publicId = company.PublicId;
            await _repository.Company.DeleteAsync(x => x.Id.Equals(id));

            if(!string.IsNullOrEmpty(publicId))
                await _cloudinary.DeleteFile(publicId);

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Update(Guid id, CompanyRequestObject request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var companyForUpdate = await _repository.Company.FindAsync(id);
            if (companyForUpdate == null)
                return new NotFoundResponse(ResponseMessages.CompanyNotFound);

            _mapper.Map(request, companyForUpdate);
            companyForUpdate.UpdatedAt = DateTime.UtcNow;

            if(request.IsValidFile)
            {
                var validationResult = Commons.ValidateImageFile(request.Logo);
                if (!validationResult.Successful)
                    return new BadRequestResponse(validationResult.Message);

                var uploadResult = new PhotoUploadResultDto(string.Empty, string.Empty);
                try
                {
                    uploadResult = await _cloudinary.UploadPhoto(request.Logo);
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
                
                if(uploadResult == null)
                    return new BadRequestResponse(ResponseMessages.CompanyUpdateFailed);

                try
                {
                    await _cloudinary.DeleteFile(companyForUpdate.PublicId);
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }

                companyForUpdate.LogoUrl = uploadResult.Url;
                companyForUpdate.PublicId = uploadResult.PublicId;
            }

            await _repository.Company.EditAsync(x => x.Id.Equals(id), companyForUpdate);

            var companyToReturn = _mapper.Map<CompanyToReturnDto>(companyForUpdate);
            return new ApiOkResponse<CompanyToReturnDto>(companyToReturn);
        }

        public ApiBaseResponse Get(string userId, bool isEmployer, SearchParameters parameters)
        {
            var companies = _repository.Company.FindAsync(parameters, userId.StringToGuid(), isEmployer);

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyToReturnDto>>(companies);
            var pagedData = PaginatedListDto<CompanyToReturnDto>.Paginate(companiesToReturn, companies.MetaData);
            return new ApiOkResponse<PaginatedListDto<CompanyToReturnDto>>(pagedData);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var company = await _repository.Company.FindAsync(id);
            if (company == null)
                return new NotFoundResponse(ResponseMessages.CompanyNotFound);

            var companyToReturn = _mapper.Map<CompanyToReturnDto>(company);
            return new ApiOkResponse<CompanyToReturnDto>(companyToReturn);
        }
    }
}
