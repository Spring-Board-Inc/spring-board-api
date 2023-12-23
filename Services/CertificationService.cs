using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Services
{
    public class CertificationService : ICertificationService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CertificationService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(Guid userInfoId, CertificationRequest request)
        {
            var userInfo = await _repository.UserInformation.GetByIdAsync(userInfoId);
            if (userInfo == null) return new NotFoundResponse(ResponseMessages.UserInformationNotFound);

            if(!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            Certification certification = _mapper.Map<Certification>(request);
            certification.UserInformationId = userInfoId;

            await _repository.Certification.AddAsync(certification);
            userInfo.Certifications.Add(certification);
            await _repository.UserInformation.EditAsync(x => x.Id.Equals(userInfo), userInfo);
            var certificationToReturn = _mapper.Map<CertificationDto>(certification);
            return new ApiOkResponse<CertificationDto>(certificationToReturn);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var cert = await _repository.Certification.FindByIdAsync(id);
            if (cert == null)
                return new NotFoundResponse(ResponseMessages.EducationNotFound);

            var certForReturn = _mapper.Map<CertificationMinInfo>(cert);
            return new ApiOkResponse<CertificationMinInfo>(certForReturn);
        }

        public async Task<IEnumerable<CertificationMinInfo>> Get(Guid id, bool track)
        {
            var certs = await _repository.Certification.FindByUserInfoIdAsync(id);
            return _mapper.Map<IEnumerable<CertificationMinInfo>>(certs);
        }

        public async Task<ApiBaseResponse> Update(Guid id, CertificationRequest request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var certificationForUpdate = await _repository.Certification.FindByIdAsync(id);
            if (certificationForUpdate == null)
                return new NotFoundResponse(ResponseMessages.CertificationNotFound);

            _mapper.Map(request, certificationForUpdate);
            certificationForUpdate.UpdatedAt = DateTime.UtcNow;

            await _repository.Certification.EditAsync(x => x.Id.Equals(id), certificationForUpdate);
            return new ApiOkResponse<string>(ResponseMessages.CertificationUpdated);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var certificationForDelete = await _repository.Certification.FindByIdAsync(id);
            if (certificationForDelete == null)
                return new NotFoundResponse(ResponseMessages.CertificationNotFound);

            var userInfo = await _repository.UserInformation.GetByIdAsync(certificationForDelete.UserInformationId);

            if (userInfo != null)
            {
                userInfo.Certifications.Remove(certificationForDelete);
            }

            await _repository.Certification.DeleteAsync(x => x.Id.Equals(id));
            await _repository.UserInformation.EditAsync(x => x.Id.Equals(userInfo.Id), userInfo);
            return new ApiOkResponse<string>(ResponseMessages.CertificationDeleted);
        }
    }
}
