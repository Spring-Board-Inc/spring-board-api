using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IAboutUsService
    {
        Task<ApiBaseResponse> Create(AboutUsForCreateDto request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Deprecate(Guid id);
        Task<AboutUsToReturnDto> Get();
        Task<ApiBaseResponse> Get(Guid id);
        Task<IEnumerable<AboutUsToReturnDto>> GetAll();
        Task<ApiBaseResponse> Update(Guid id, AboutUsForUpdateDto request);
    }
}
