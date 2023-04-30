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
            await manager.AboutUs.CreateAsync(about);
            await manager.SaveAsync();

            var data = mapper.Map<AboutUsToReturnDto>(about);

            return new ApiOkResponse<AboutUsToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Update(Guid id, AboutUsForUpdateDto request)
        {
            var about = await manager.AboutUs.Get(id, true);
            if(about == null) return new AboutUsNotFoundResponse(id);

            mapper.Map(request, about);
            about.UpdatedAt = DateTime.Now;

            manager.AboutUs.UpdateAbout(about);
            await manager.SaveAsync();

            var data = mapper.Map<AboutUsToReturnDto>(about);
            return new ApiOkResponse<AboutUsToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Get()
        {
            var about = await manager.AboutUs.Get(false);
            if (about == null) return new AboutUsNotFoundResponse();

            var data = mapper.Map<AboutUsToReturnDto>(about);
            return new ApiOkResponse<AboutUsToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var about = await manager.AboutUs.Get(id, false);
            if (about == null) return new AboutUsNotFoundResponse(id);

            var data = mapper.Map<AboutUsToReturnDto>(about);
            return new ApiOkResponse<AboutUsToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var about = await manager.AboutUs.Get(id, true);
            if (about == null) return new AboutUsNotFoundResponse(id);

            manager.AboutUs.DeleteAbout(about);
            await manager.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Deprecate(Guid id)
        {
            var about = await manager.AboutUs.Get(id, true);
            if (about == null) return new AboutUsNotFoundResponse(id);

            about.IsDeprecated = true;
            about.UpdatedAt = DateTime.Now;

            manager.AboutUs.UpdateAbout(about);
            await manager.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }
    }
}
