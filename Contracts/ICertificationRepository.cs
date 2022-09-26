using Entities.Models;

namespace Contracts
{
    public interface ICertificationRepository
    {
        IQueryable<Certification?> FindCertifications(Guid userInfoId, bool trackChanges);
        Task<IEnumerable<Certification>> FindCertificationsAsync(Guid userInfoId, bool trackChanges);
        Task<Certification?> FindCertification(Guid id, bool trackChanges);
        void UpdateCertification(Certification certification);
        void DeleteCertification(Certification certification);
        Task CreateCertificationAsync(Certification certification);
    }
}
