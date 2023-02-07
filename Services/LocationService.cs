using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Entities.Response;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;
using System.Net;

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
            await _repository.Country.CreateCountryAsync(entity);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> CreateState(StateForCreationDto state)
        {
            if (!state.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var entity = _mapper.Map<State>(state);
            await _repository.State.CreateStateAsync(entity);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> DeleteCountry(Guid id, bool trackChanges)
        {
            var entity = await _repository.Country.GetCountryAsync(id, trackChanges);
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.CountryNotFound);

            _repository.Country.DeleteCountry(entity);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> DeleteState(Guid id, bool trackChanges)
        {
            var entity = await _repository.State.GetStateAsync(id, trackChanges);
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.StateNotFound);

            _repository.State.DeleteState(entity);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> GetCountries(SearchParameters searchParameters)
        {
            var countries = await _repository.Country.GetCountriesAsync(searchParameters, false);
            var countriesToReturn = _mapper.Map<IEnumerable<CountryDto>>(countries);
            var pagedData = PagedList<CountryDto>.Paginate(countriesToReturn, searchParameters.PageNumber, searchParameters.PageSize);
            return new ApiOkResponse<PaginatedListDto<CountryDto>>(pagedData);
        }

        public async Task<ApiBaseResponse> GetCountry(Guid id, bool trackChanges)
        {
            var entity = await _repository.Country.GetCountryAsync(id, trackChanges);
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.CountryNotFound);

            var countryToReturn = _mapper.Map<CountryDto>(entity);

            return new ApiOkResponse<CountryDto>(countryToReturn);
        }

        public async Task<ApiBaseResponse> GetStates(StateSearchParameters searchParameters, bool trackChanges)
        {
            IEnumerable<State> states;
            if (searchParameters.CountryId.Equals(Guid.Empty))
                states = await _repository.State.GetStates(searchParameters, trackChanges).ToListAsync();
            else
                states = await _repository.State.GetStatesByCountry(searchParameters, trackChanges).ToListAsync();

            var statesToReturn = _mapper.Map<IEnumerable<StateDto>>(states);
            var pagedData = PagedList<StateDto>.Paginate(statesToReturn, searchParameters.PageNumber, searchParameters.PageSize);
            return new ApiOkResponse<PaginatedListDto<StateDto>>(pagedData);
        }

        public async Task<ApiBaseResponse> GetState(Guid id, bool trackChanges)
        {
            var entity = await _repository.State.GetState(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.StateNotFound);

            var stateToReturn = _mapper.Map<StateDto>(entity);

            return new ApiOkResponse<StateDto>(stateToReturn);
        }

        public async Task<ApiBaseResponse> UpdateCountry(Guid id, CountryForUpdateDto countryForUpdate, bool trackChanges)
        {
            if (!countryForUpdate.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var entity = await _repository.Country.GetCountryAsync(id, trackChanges);
            if (entity == null)
                return new NotFoundResponse(ResponseMessages.CountryNotFound);

            _mapper.Map(countryForUpdate, entity);
            entity.UpdatedAt = DateTime.Now;
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> UpdateState(Guid id, StateForUpdateDto stateForUpdate, bool trackChanges)
        {
            if(!stateForUpdate.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var entity = await _repository.State.GetStateAsync(id, trackChanges);
            if(entity == null) 
                return new NotFoundResponse(ResponseMessages.StateNotFound);

            _mapper.Map(stateForUpdate, entity);
            entity.UpdatedAt = DateTime.Now;
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }
    }
}
