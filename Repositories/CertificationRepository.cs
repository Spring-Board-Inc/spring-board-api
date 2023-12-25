using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using System.Linq.Expressions;

namespace Repositories
{
    public class CertificationRepository : MongoRepositoryBase<Certification>, ICertificationRepository
    {
        public CertificationRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {}

        public async Task AddAsync(Certification certification) => 
            await CreateAsync(certification);

        public async Task DeleteAsync(Expression<Func<Certification, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task EditAsync(Expression<Func<Certification, bool>> expression, Certification certification) => 
            await UpdateAsync(expression, certification);

        public async Task<Certification?> FindByIdAsync(Guid id) =>
            await GetAsync(ct => ct.Id.Equals(id));

        public List<Certification> FindByUserInfoIdAsync(Guid userInfoId) =>
            GetAsQueryable(ct => ct.UserInformationId.Equals(userInfoId)).ToList();

        public IQueryable<Certification> FindByUserInfoIdAsQueryable(Guid userInfoId) =>
            GetAsQueryable(ct => ct.UserInformationId.Equals(userInfoId))
                .OrderByDescending(ct => ct.IssuingDate);
    }
}