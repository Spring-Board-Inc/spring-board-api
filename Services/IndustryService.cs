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

            await _repository.Industry.AddAsync(industry);
            var industryToReturn = _mapper.Map<IndustryToReturnDto>(industry);
            return new ApiOkResponse<IndustryToReturnDto>(industryToReturn);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var industry = await _repository.Industry.FindAsync(id);
            if (industry == null)
                return new NotFoundResponse(ResponseMessages.IndustryNotFound);

            await _repository.Industry.DeleteAsync(x => x.Id.Equals(id));
            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Update(Guid id, IndustryRequestObject request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var industry = await _repository.Industry.FindAsync(id);
            if (industry == null)
                return new NotFoundResponse(ResponseMessages.IndustryNotFound);

            industry.Name = request.Industry;
            industry.UpdatedAt = DateTime.UtcNow;

            await _repository.Industry.EditAsync(x => x.Id.Equals(id), industry);

            return new ApiOkResponse<bool>(true);
        }

        public PaginatedListDto<IndustryToReturnDto> Get(SearchParameters parameters)
        {
            var industries = _repository.Industry.Find(parameters);
            var data = _mapper.Map<IEnumerable<IndustryToReturnDto>>(industries);

            var paged = PaginatedListDto<IndustryToReturnDto>.Paginate(data, industries.MetaData);
            return paged;
        }

        public IEnumerable<IndustryToReturnDto> GetAll()
        {
            return _mapper.Map<IEnumerable<IndustryToReturnDto>>( 
                _repository.Industry.FindAsQueryable().ToList());
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var industry = await _repository.Industry.FindAsync(id);
            if (industry == null)
                return new NotFoundResponse(ResponseMessages.IndustryNotFound);

            var industryToReturn = _mapper.Map<IndustryToReturnDto>(industry);
            return new ApiOkResponse<IndustryToReturnDto>(industryToReturn);
        }
    }
}
