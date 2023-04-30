namespace Contracts
{
    public interface IRepositoryManager
    {
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
        IStateRepository State { get; }
        ICountryRepository Country { get; }
        ICareerSummaryRepository CareerSummary { get; }
        IContactRepository Contact { get; }
        IAboutUsRepository AboutUs { get; }
        IFaqRepository Faq { get; }
        Task SaveAsync();
    }
}
