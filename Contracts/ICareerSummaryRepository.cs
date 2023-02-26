using Entities.Models;

namespace Contracts
{
    public interface ICareerSummaryRepository
    {
        Task CreateCareerSummaryAsync(CareerSummary entity);
        void DeleteCareerSummary(CareerSummary entity);
        void UpdateCareerSummary(CareerSummary entity);
        IQueryable<CareerSummary> FindCareerSummary(string userId, bool trackChanges);
        IQueryable<CareerSummary> FindCareerSummary(Guid id, string userId, bool trackChanges);
        Task<bool> Exists(string userId);
    }
}
