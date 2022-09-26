using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Services
{
    public class JobService: IJobService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public JobService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(JobRequestObject request)
        {
            if (!request.IsValidSalaryRange)
                return new BadRequestResponse(ResponseMessages.InvalidSalaryRange);

            if (!request.IsValidClosingDate)
                return new BadRequestResponse(ResponseMessages.InvalidClosingDate);

            var job = _mapper.Map<Job>(request);

            await _repository.Job.CreateJobAsync(job);
            await _repository.SaveAsync();

            var jobToReturn = new JobToReturnDto(job.Id, job.Title, job.Descriptions, (job.SalaryLowerRange != null && job.SalaryLowerRange != null) ? "" : $"{job.SalaryLowerRange} - {job.SalaryUpperRange}", job.ClosingDate, job.CreatedAt, job.UpdatedAt);
            return new ApiOkResponse<JobToReturnDto>(jobToReturn);

        }
    }
}
