using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Entities.Response;
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

        public async Task<ApiBaseResponse> Create(LocationForCreationDto location)
        {
            var locationEntity = _mapper.Map<Location>(location);
            await _repository.Location.CreateLocationAsync(locationEntity);
            await _repository.SaveAsync();

            var locationToReturn = _mapper.Map<LocationDto>(locationEntity);
            return new ApiOkResponse<LocationDto>(locationToReturn);
        }

        public async Task<ApiBaseResponse> Get(SearchParameters locationParameters, bool trackChanges)
        {
            if (!locationParameters.ValidDateRange)
                return new BadRequestResponse(ResponseMessages.InvalidDateRange);

            var locations = await _repository.Location.GetLocationsAsync(locationParameters, trackChanges);

            var locationsDto = _mapper.Map<IEnumerable<LocationDto>>(locations);
            var pagedDataList = PagedList<LocationDto>.Paginate(locationsDto, locationParameters.PageNumber, locationParameters.PageSize);

            return new ApiOkResponse<PaginatedListDto<LocationDto>>(pagedDataList);
        }

        public async Task<ApiBaseResponse> Get(Guid locationId, bool trackChanges)
        {
            var location = await _repository.Location.GetLocationAsync(locationId, trackChanges);
            if (location is null)
                return new NotFoundResponse(ResponseMessages.NoLocationFound);

            var locationDto = _mapper.Map<LocationDto>(location);
            return new ApiOkResponse<LocationDto>(locationDto);
        }

        public async Task<ApiBaseResponse> Update(Guid locationId, LocationForUpdateDto locationForUpdate, bool trackChanges)
        {
            var locationEntity = await _repository.Location.GetLocationAsync(locationId, trackChanges);
            if (locationEntity is null)
                return new NotFoundResponse(ResponseMessages.NoLocationFound);

            _mapper.Map(locationForUpdate, locationEntity);
            locationEntity.UpdatedAt = DateTime.Now;
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.UpdateSuccessful);
        }

        public async Task<ApiBaseResponse> Delete(Guid locationId, bool trackChanges)
        {
            var location = await _repository.Location.GetLocationAsync(locationId, trackChanges);
            if (location is null)
                return new NotFoundResponse(ResponseMessages.NoLocationFound);
            
            _repository.Location.DeleteLocation(location);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.DeleteSuccessful);
        }
    }
}
