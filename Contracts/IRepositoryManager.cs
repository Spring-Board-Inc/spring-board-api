namespace Contracts
{
    public interface IRepositoryManager
    {
        ILocationRepository Location { get; }
        ITokenRepository Token { get; }
        IUserInformationRepository UserInformation { get; }
        IEducationRepository Education { get; }
        IWorkExperienceRepository WorkExperience { get; }
        ISkillsRepository Skills { get; }
        ICertificationRepository Certification { get; }
        ICompanyRepository Company { get; }
        IIndustryRepository Industry { get; }
        IJobRepository Job { get; }
        IJobTypeRepository JobType { get; }
        IUserSkillRepository UserSkill { get; }
        IUserJobRepository UserJob { get; }
        Task SaveAsync();
    }
}
