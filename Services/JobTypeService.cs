using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Services
{
    public class JobTypeService : IJobTypeService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public JobTypeService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(JobTypeRequestObject request)
        {
            if(!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var jobType = _mapper.Map<JobType>(request);

            await _repository.JobType.CreateJobTypeAsync(jobType);
            await _repository.SaveAsync();

            var jobTypeToReturn = _mapper.Map<JobTypeToReturnDto>(jobType);
            return new ApiOkResponse<JobTypeToReturnDto>(jobTypeToReturn);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var jobType = await _repository.JobType.FindJobTypeAsync(id, true);
            if (jobType == null)
                return new NotFoundResponse(ResponseMessages.JobTypeNotFound);

            _repository.JobType.DeleteJobType(jobType);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Update(Guid id, JobTypeRequestObject request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var jobType = await _repository.JobType.FindJobTypeAsync(id, true);
            if (jobType == null)
                return new NotFoundResponse(ResponseMessages.JobTypeNotFound);

            jobType.Name = request.JobType;
            jobType.UpdatedAt = DateTime.Now;

            _repository.JobType.UpdateJobType(jobType);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<IEnumerable<JobTypeToReturnDto>> Get()
        {
            var jobTypes = await _repository.JobType.FindJobTypesAsync(false);
            return _mapper.Map<IEnumerable<JobTypeToReturnDto>>(jobTypes);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var jobType = await _repository.JobType.FindJobTypeAsync(id, false);
            if (jobType == null)
                return new NotFoundResponse(ResponseMessages.JobTypeNotFound);

            var jobTypeToReturn = _mapper.Map<JobTypeToReturnDto>(jobType);
            return new ApiOkResponse<JobTypeToReturnDto>(jobTypeToReturn);
        }
    }
}
