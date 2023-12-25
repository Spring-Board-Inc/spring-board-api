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

            await _repository.JobType.AddAsync(jobType);
            var jobTypeToReturn = _mapper.Map<JobTypeToReturnDto>(jobType);
            return new ApiOkResponse<JobTypeToReturnDto>(jobTypeToReturn);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var jobType = await _repository.JobType.FindAsync(id);
            if (jobType == null)
                return new NotFoundResponse(ResponseMessages.JobTypeNotFound);

            await _repository.JobType.DeleteAsync(x => x.Id.Equals(id));
            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Update(Guid id, JobTypeRequestObject request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var jobType = await _repository.JobType.FindAsync(id);
            if (jobType == null)
                return new NotFoundResponse(ResponseMessages.JobTypeNotFound);

            jobType.Name = request.JobType;
            jobType.UpdatedAt = DateTime.UtcNow;

            await _repository.JobType.EditAsync(x => x.Id.Equals(id), jobType);
            return new ApiOkResponse<bool>(true);
        }

        public IEnumerable<JobTypeToReturnDto> Get()
        {
            var jobTypes = _repository.JobType.FindAsList();
            return _mapper.Map<IEnumerable<JobTypeToReturnDto>>(jobTypes);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var jobType = await _repository.JobType.FindAsync(id);
            if (jobType == null)
                return new NotFoundResponse(ResponseMessages.JobTypeNotFound);

            var jobTypeToReturn = _mapper.Map<JobTypeToReturnDto>(jobType);
            return new ApiOkResponse<JobTypeToReturnDto>(jobTypeToReturn);
        }
    }
}
