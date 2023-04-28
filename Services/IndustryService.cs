using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

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
            if (!request.IsValidParams)
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

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Update(Guid id, IndustryRequestObject request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var industry = await _repository.Industry.FindIndustryAsync(id, true);
            if (industry == null)
                return new NotFoundResponse(ResponseMessages.IndustryNotFound);

            industry.Name = request.Industry;
            industry.UpdatedAt = DateTime.Now;

            _repository.Industry.UpdateIndustry(industry);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<PaginatedListDto<IndustryToReturnDto>> Get(SearchParameters parameters)
        {
            var industries = await _repository.Industry.FindIndustriesAsync(parameters, false);
            var data = _mapper.Map<IEnumerable<IndustryToReturnDto>>(industries);

            var paged = PaginatedListDto<IndustryToReturnDto>.Paginate(data, industries.MetaData);
            return paged;
        }

        public async Task<IEnumerable<IndustryToReturnDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<IndustryToReturnDto>>( await _repository.Industry.FindIndustries(false).ToListAsync());
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
