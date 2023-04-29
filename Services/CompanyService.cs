using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
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
            company.UserId = userId;

            await _repository.Company.CreateCompanyAsync(company);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyToReturnDto>(company);
            return new ApiOkResponse<CompanyToReturnDto>(companyToReturn);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var company = await _repository.Company.FindCompanyAsync(id, true);
            var publicId = company.PublicId;

            if (company == null)
                return new NotFoundResponse(ResponseMessages.CompanyNotFound);

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();

            if(!string.IsNullOrEmpty(publicId))
                await _cloudinary.DeleteFile(publicId);

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Update(Guid id, CompanyRequestObject request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var companyForUpdate = await _repository.Company.FindCompanyAsync(id, true);
            if (companyForUpdate == null)
                return new NotFoundResponse(ResponseMessages.CompanyNotFound);

            _mapper.Map(request, companyForUpdate);
            companyForUpdate.UpdatedAt = DateTime.Now;

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

            _repository.Company.UpdateCompany(companyForUpdate);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyToReturnDto>(companyForUpdate);
            return new ApiOkResponse<CompanyToReturnDto>(companyToReturn);
        }

        public async Task<ApiBaseResponse> Get(string userId, bool isEmployer, SearchParameters parameters)
        {
            var companies = await _repository.Company.FindCompaniesAsync(parameters, false, userId, isEmployer);

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyToReturnDto>>(companies);
            var pagedData = PaginatedListDto<CompanyToReturnDto>.Paginate(companiesToReturn, companies.MetaData);
            return new ApiOkResponse<PaginatedListDto<CompanyToReturnDto>>(pagedData);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var company = await _repository.Company.FindCompanyAsync(id, false);
            if (company == null)
                return new NotFoundResponse(ResponseMessages.CompanyNotFound);

            var companyToReturn = _mapper.Map<CompanyToReturnDto>(company);
            return new ApiOkResponse<CompanyToReturnDto>(companyToReturn);
        }
    }
}
