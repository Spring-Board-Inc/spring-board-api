using Entities.Models;

namespace Contracts
{
    public interface IEducationRepository
    {
        Task<IEnumerable<Education>> GetEducationsAsync(Guid id, bool trackChanges);
        Task<Education?> GetEducationAsync(Guid id, bool trackChanges);
        IQueryable<Education> GetEducations(Guid userInfoId, bool trackChanges);
        void DeleteEducation(Education education);
        void UpdateEducation(Education education);
        Task CreateEducationAsync(Education education);
    }
}
