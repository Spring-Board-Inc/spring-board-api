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

        public async Task<ApiBaseResponse> Get()
        {
            var contact = await manager.Contact.GetAsync(false);
            if(contact == null) return new ContactNotFoundResponse();

            var data = mapper.Map<ContactToReturnDto>(contact);
            return new ApiOkResponse<ContactToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var contact = await manager.Contact.GetAsync(id, false);
            if (contact == null) return new ContactNotFoundResponse(id);

            var data = mapper.Map<ContactToReturnDto>(contact);
            return new ApiOkResponse<ContactToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Update(Guid id, ContactForUpdateDto request)
        {
            var contact = await manager.Contact.GetAsync(id, true);
            if (contact == null) return new ContactNotFoundResponse(id);

            mapper.Map(request, contact);
            contact.UpdatedAt = DateTime.Now;
            manager.Contact.UpdateContact(contact);
            await manager.SaveAsync();

            return new ApiOkResponse<ContactToReturnDto>(mapper.Map<ContactToReturnDto>(contact));
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var contact = await manager.Contact.GetAsync(id, true);
            if (contact == null) return new ContactNotFoundResponse(id);

            manager.Contact.DeleteContact(contact);
            await manager.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Deprecate(Guid id)
        {
            var contact = await manager.Contact.GetAsync(id, true);
            if (contact == null) return new ContactNotFoundResponse(id);

            contact.IsDeprecated = true;
            contact.UpdatedAt = DateTime.Now;
            manager.Contact.UpdateContact(contact);
            await manager.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }
    }
}
