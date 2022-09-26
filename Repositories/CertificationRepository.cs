using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CertificationRepository : RepositoryBase<Certification>, ICertificationRepository
    {
        public CertificationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateCertificationAsync(Certification certification) => await Create(certification);

        public void DeleteCertification(Certification certification) => Delete(certification);

        public void UpdateCertification(Certification certification) => Update(certification);

        public async Task<Certification?> FindCertification(Guid id, bool trackChanges) =>
            await FindByCondition(ct => ct.Id.Equals(id), trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<Certification>> FindCertificationsAsync(Guid userInfoId, bool trackChanges) =>
            await FindByCondition(ct => ct.UserInformationId.Equals(userInfoId), trackChanges)
                     .ToListAsync();

        public IQueryable<Certification?> FindCertifications(Guid userInfoId, bool trackChanges) =>
            FindByCondition(ct => ct.UserInformationId.Equals(userInfoId), trackChanges)
                .OrderByDescending(ct => ct.IssuingDate);
    }
}