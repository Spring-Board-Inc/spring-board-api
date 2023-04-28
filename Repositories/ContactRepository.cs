using Contracts;
using Entities.Models;

namespace Repositories
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(RepositoryContext context)
            : base(context)
        {}
    }
}
