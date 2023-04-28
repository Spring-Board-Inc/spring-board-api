using AutoMapper;
using Contracts;
using Microsoft.Extensions.Logging;
using Services.Contracts;

namespace Services
{
    public class ContactService : IContactService
    {
        private readonly IRepositoryManager manager;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public ContactService(IRepositoryManager manager, IMapper mapper, ILoggerManager logger)
        {
            this.manager = manager;
            this.mapper = mapper;
            this.logger = logger;
        }
    }
}
