namespace Services.Contracts
{
    public interface IServiceManager
    {
        ILocationService Location { get; }
        IAuthenticationService Authentication { get; }
        IUserService User { get; }
        IUserInformationService UserInformation { get; }
        IEducationService Education { get; }
        IWorkExperienceService WorkExperience { get; }
        ISkillService Skill { get; }
        IUserSkillService UserSkill { get; }
        ICertificationService Certification { get; }
        IJobService Job { get; }
        IJobTypeService JobType { get; }
        ICompanyService Company { get; }
        IIndustryService Industry { get; }
        ICareerSummaryService CareerSummary { get; }
    }
}