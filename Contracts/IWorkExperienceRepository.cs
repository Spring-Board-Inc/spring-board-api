using Entities.Models;

namespace Contracts
{
    public interface IWorkExperienceRepository
    {
        IQueryable<WorkExperience> FindExperiences(Guid userInfoId, bool trackChanges);
        Task<IEnumerable<WorkExperience>> FindWorkExperiencesAsync(Guid userInfoId, bool trackChanges);
        Task<WorkExperience?> FindWorkExperienceAsync(Guid id, bool trackChanges);
        void UpdateWorkExperience(WorkExperience workExperience);
        void DeleteWorkExperience(WorkExperience workExperience);
        Task CreateWorkExperienceAsync(WorkExperience workExperience);
        IQueryable<WorkExperience> FindExperiences(Guid id);
    }
}