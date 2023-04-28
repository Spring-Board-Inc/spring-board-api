using AutoMapper;
using Contracts;
using Services.Contracts;

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
    }
}
