using Entities.Models;
using Entities.Response;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface ILocationService
    {
        Task<ApiBaseResponse> GetState(Guid id, bool trackChanges);
        Task<ApiBaseResponse> GetStates(StateSearchParameters searchParameters, bool trackChanges);
        Task<ApiBaseResponse> CreateState(StateForCreationDto state);
        Task<ApiBaseResponse> DeleteState(Guid id, bool trackChanges);
        Task<ApiBaseResponse> UpdateState(Guid id, StateForUpdateDto stateForUpdate, bool trackChanges);
        Task<ApiBaseResponse> GetCountry(Guid id, bool trackChanges);
        Task<ApiBaseResponse> GetCountries(SearchParameters searchParameters);
        Task<ApiBaseResponse> CreateCountry(CountryForCreationDto state);
        Task<ApiBaseResponse> DeleteCountry(Guid id, bool trackChanges);
        Task<ApiBaseResponse> UpdateCountry(Guid id, CountryForUpdateDto stateForUpdate, bool trackChanges);
        Task<IEnumerable<StateDto>> GetAll(Guid countryId);
        Task<IEnumerable<CountryDto>> GetAll();
    }
}
