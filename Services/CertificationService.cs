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
            if(request == null)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            Certification certification = _mapper.Map<Certification>(request);
            certification.UserInformationId = userInfoId;

            await _repository.Certification.CreateCertificationAsync(certification);
            await _repository.SaveAsync();

            var certificationToReturn = _mapper.Map<CertificationDto>(certification);
            return new ApiOkResponse<CertificationDto>(certificationToReturn);
        }

        public async Task<ApiBaseResponse> Update(Guid id, CertificationRequest request)
        {
            if (request == null)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var certificationForUpdate = await _repository.Certification.FindCertification(id, true);
            if (certificationForUpdate == null)
                return new NotFoundResponse(ResponseMessages.CertificationNotFound);

            _mapper.Map(request, certificationForUpdate);
            certificationForUpdate.UpdatedAt = DateTime.Now;

            _repository.Certification.UpdateCertification(certificationForUpdate);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.CertificationUpdated);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var certificationForDelete = await _repository.Certification.FindCertification(id, true);
            if (certificationForDelete == null)
                return new NotFoundResponse(ResponseMessages.CertificationNotFound);

            _repository.Certification.DeleteCertification(certificationForDelete);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.CertificationDeleted);
        }
    }
}
