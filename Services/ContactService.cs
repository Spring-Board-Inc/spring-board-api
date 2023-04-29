using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using Shared.DataTransferObjects;

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

        public async Task<ApiBaseResponse> Create(ContactForCreationDto request)
        {
            if (await manager.Contact.Exists()) return new ContactExistsResponse();

            var contact = mapper.Map<Contact>(request);

            await manager.Contact.CreateAsync(contact);
            await manager.SaveAsync();

            var data = mapper.Map<ContactToReturnDto>(contact);
            return new ApiOkResponse<ContactToReturnDto>(data);
        }
    }
}
