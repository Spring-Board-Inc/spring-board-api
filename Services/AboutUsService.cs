using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace Services
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IRepositoryManager manager;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public AboutUsService(IRepositoryManager manager, IMapper mapper, ILoggerManager logger)
        {
            this.manager = manager;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ApiBaseResponse> Create(AboutUsForCreateDto request)
        {
            if (await manager.AboutUs.Exists()) return new AboutUsExistsReponse();

            var about = mapper.Map<AboutUs>(request);
            await manager.AboutUs.AddAsync(about);
            var data = mapper.Map<AboutUsToReturnDto>(about);

            return new ApiOkResponse<AboutUsToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Update(Guid id, AboutUsForUpdateDto request)
        {
            var about = await manager.AboutUs.FindByIdAsync(id);
            if(about == null) return new AboutUsNotFoundResponse(id);

            mapper.Map(request, about);
            about.UpdatedAt = DateTime.UtcNow;

            await manager.AboutUs.EditAsync(x => x.Id.Equals(id), about);

            var data = mapper.Map<AboutUsToReturnDto>(about);
            return new ApiOkResponse<AboutUsToReturnDto>(data);
        }

        public async Task<AboutUsToReturnDto> Get()
        {
            var about = await manager.AboutUs.FindFirstAsync(_=> true);
            return mapper.Map<AboutUsToReturnDto>(about);
        }

        public async Task<IEnumerable<AboutUsToReturnDto>> GetAll()
        {
            var abouts = await manager.AboutUs.FindAllAsync();
            return mapper.Map<IEnumerable<AboutUsToReturnDto>>(abouts);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var about = await manager.AboutUs.FindByIdAsync(id);
            if (about == null) return new AboutUsNotFoundResponse(id);

            var data = mapper.Map<AboutUsToReturnDto>(about);
            return new ApiOkResponse<AboutUsToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var about = await manager.AboutUs.FindByIdAsync(id);
            if (about == null) return new AboutUsNotFoundResponse(id);

            await manager.AboutUs.DeleteAsync(x => x.Id.Equals(about.Id));

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Deprecate(Guid id)
        {
            var about = await manager.AboutUs.FindByIdAsync(id);
            if (about == null) return new AboutUsNotFoundResponse(id);

            about.IsDeprecated = true;
            about.UpdatedAt = DateTime.UtcNow;

            await manager.AboutUs.EditAsync(x => x.Id.Equals(id), about);
            return new ApiOkResponse<bool>(true);
        }
    }
}
