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
    internal sealed class LocationService : ILocationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public LocationService(
            IRepositoryManager repository, 
            ILoggerManager logger,
            IMapper mapper
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> CreateCountry(CountryForCreationDto country)
        {
            if(!country.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var entity = _mapper.Map<Country>(country);
            await _repository.Country.AddAsync(entity);

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> CreateState(StateForCreationDto state)
        {
            if (!state.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var entity = _mapper.Map<State>(state);
            await _repository.State.AddAsync(entity);
            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> DeleteCountry(Guid id)
        {
            var entity = await _repository.Country.FindAsync(id);
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.CountryNotFound);

            await _repository.Country.DeleteAsync(x => x.Id.Equals(id));
            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> DeleteState(Guid id)
        {
            var entity = await _repository.State.FindAsync(id);
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.StateNotFound);

            await _repository.State.DeleteAsync(x => x.Id.Equals(id));

            return new ApiOkResponse<bool>(true);
        }

        public ApiBaseResponse GetCountries(SearchParameters searchParameters)
        {
            var countries = _repository.Country.FindAsync(searchParameters);
            var countriesToReturn = _mapper.Map<IEnumerable<CountryDto>>(countries);
            var pagedData = PaginatedListDto<CountryDto>.Paginate(countriesToReturn, countries.MetaData);
            return new ApiOkResponse<PaginatedListDto<CountryDto>>(pagedData);
        }

        public async Task<IEnumerable<CountryDto>> GetAll() =>
            _mapper.Map<IEnumerable<CountryDto>>(await _repository.Country.FindAsQueryable().ToListAsync());

        public async Task<ApiBaseResponse> GetCountry(Guid id)
        {
            var entity = await _repository.Country.FindAsync(id);
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.CountryNotFound);

            var countryToReturn = _mapper.Map<CountryDto>(entity);

            return new ApiOkResponse<CountryDto>(countryToReturn);
        }

        public ApiBaseResponse GetStates(StateSearchParameters searchParameters)
        {
            PagedList<State> states;
            if (searchParameters.CountryId.Equals(Guid.Empty))
                states = _repository.State.FindStates(searchParameters);

            else
                states = _repository.State.FindByCountryIdAsync(searchParameters);

            var statesToReturn = _mapper.Map<IEnumerable<StateDto>>(states);
            var pagedData = PaginatedListDto<StateDto>.Paginate(statesToReturn, states.MetaData);
            return new ApiOkResponse<PaginatedListDto<StateDto>>(pagedData);
        }

        public async Task<IEnumerable<StateDto>> GetAll(Guid countryId)
        {
            if(!countryId.Equals(Guid.Empty))
                return _mapper.Map<IEnumerable<StateDto>>(await _repository.State.FindByCountryAsQueryable(countryId)
                    .ToListAsync());

            return new List<StateDto>();
        }

        public async Task<ApiBaseResponse> GetState(Guid id)
        {
            var entity = await _repository.State.FindByIdAsQueryable(id)
                .FirstOrDefaultAsync();
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.StateNotFound);

            var stateToReturn = _mapper.Map<StateDto>(entity);

            return new ApiOkResponse<StateDto>(stateToReturn);
        }

        public async Task<ApiBaseResponse> UpdateCountry(Guid id, CountryForUpdateDto countryForUpdate)
        {
            if (!countryForUpdate.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var entity = await _repository.Country.FindAsync(id);
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.CountryNotFound);

            _mapper.Map(countryForUpdate, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            await _repository.Country.EditAsync(x => x.Id.Equals(id), entity);
            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> UpdateState(Guid id, StateForUpdateDto stateForUpdate)
        {
            if(!stateForUpdate.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var entity = await _repository.State.FindAsync(id);
            if(entity == null) 
                return new NotFoundResponse(ResponseMessages.StateNotFound);

            _mapper.Map(stateForUpdate, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            await _repository.State.EditAsync(x => x.Id.Equals(id), entity);
            return new ApiOkResponse<bool>(true);
        }
    }
}
