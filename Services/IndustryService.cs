using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Services
{
    public class IndustryService : IIndustryService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public IndustryService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(IndustryRequestObject request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Industry))
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var industry = _mapper.Map<Industry>(request);

            await _repository.Industry.CreateIndustryAsync(industry);
            await _repository.SaveAsync();

            var industryToReturn = _mapper.Map<IndustryToReturnDto>(industry);
            return new ApiOkResponse<IndustryToReturnDto>(industryToReturn);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var industry = await _repository.Industry.FindIndustryAsync(id, true);
            if (industry == null)
                return new NotFoundResponse(ResponseMessages.IndustryNotFound);

            _repository.Industry.DeleteIndustry(industry);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.IndustryDeleted);
        }

        public async Task<ApiBaseResponse> Update(Guid id, IndustryRequestObject request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Industry))
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var industry = await _repository.Industry.FindIndustryAsync(id, true);
            if (industry == null)
                return new NotFoundResponse(ResponseMessages.IndustryNotFound);

            industry.Name = request.Industry;
            industry.UpdatedAt = DateTime.Now;

            _repository.Industry.UpdateIndustry(industry);
            await _repository.SaveAsync();

            var industryToReturn = _mapper.Map<IndustryToReturnDto>(industry);
            return new ApiOkResponse<IndustryToReturnDto>(industryToReturn);
        }

        public async Task<IEnumerable<IndustryToReturnDto>> Get()
        {
            var industries = await _repository.Industry.FindIndustriesAsync(false);
            return _mapper.Map<IEnumerable<IndustryToReturnDto>>(industries);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var industry = await _repository.Industry.FindIndustryAsync(id, false);
            if (industry == null)
                return new NotFoundResponse(ResponseMessages.IndustryNotFound);

            var industryToReturn = _mapper.Map<IndustryToReturnDto>(industry);
            return new ApiOkResponse<IndustryToReturnDto>(industryToReturn);
        }
    }
}
