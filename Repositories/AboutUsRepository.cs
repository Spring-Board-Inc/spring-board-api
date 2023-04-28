using Contracts;
using Entities.Models;

namespace Repositories
{
    public class AboutUsRepository: RepositoryBase<AboutUs>, IAboutUsRepository
    {
        public AboutUsRepository(RepositoryContext context)
            : base(context)
        {}
    }
}
