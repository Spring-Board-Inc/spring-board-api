using Entities.Models;
using Entities.Response;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface ILocationService
    {
        ApiBaseResponse GetState(Guid id);
        ApiBaseResponse GetStates(StateSearchParameters searchParameters);
        Task<ApiBaseResponse> CreateState(StateForCreationDto state);
        Task<ApiBaseResponse> DeleteState(Guid id);
        Task<ApiBaseResponse> UpdateState(Guid id, StateForUpdateDto stateForUpdate);
        Task<ApiBaseResponse> GetCountry(Guid id);
        ApiBaseResponse GetCountries(SearchParameters searchParameters);
        Task<ApiBaseResponse> CreateCountry(CountryForCreationDto state);
        Task<ApiBaseResponse> DeleteCountry(Guid id);
        Task<ApiBaseResponse> UpdateCountry(Guid id, CountryForUpdateDto stateForUpdate);
        IEnumerable<StateDto> GetAll(Guid countryId);
        IEnumerable<CountryDto> GetAll();
    }
}
