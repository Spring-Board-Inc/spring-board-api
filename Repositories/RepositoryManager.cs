using Contracts;

namespace Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ILocationRepository> _locationRepository;
        private readonly Lazy<ITokenRepository> _tokenRepository;
        private readonly Lazy<IUserInformationRepository> _userInformationRepository;
        private readonly Lazy<IEducationRepository> _educationRepository;
        private readonly Lazy<IWorkExperienceRepository> _workExperienceRepository;
        private readonly Lazy<ISkillsRepository> _skillsRepository;
        private readonly Lazy<ICertificationRepository> _certificationRepository;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IIndustryRepository> _industryRepository;
        private readonly Lazy<IJobRepository> _jobRepository;
        private readonly Lazy<IJobTypeRepository> _jobTypeRepository;
        private readonly Lazy<IUserSkillRepository> _userSkillRepository;
        private readonly Lazy<IUserJobRepository> _userJobRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _locationRepository = new Lazy<ILocationRepository>(() => new
                LocationRepository(repositoryContext));
            _tokenRepository = new Lazy<ITokenRepository>(() => new 
                TokenRepository(repositoryContext));
            _userInformationRepository = new Lazy<IUserInformationRepository>(() => new
                UserInformationRepository(repositoryContext));
            _educationRepository = new Lazy<IEducationRepository>(() => new
                EducationRepository(repositoryContext));
            _workExperienceRepository = new Lazy<IWorkExperienceRepository>(() => new
                WorkExperienceRepository(repositoryContext));
            _skillsRepository = new Lazy<ISkillsRepository>(() => new
                SkillsRepository(repositoryContext));
            _companyRepository = new Lazy<ICompanyRepository>(() => new
                CompanyRepository(repositoryContext));
            _certificationRepository = new Lazy<ICertificationRepository>(() => new
                CertificationRepository(repositoryContext));
            _industryRepository = new Lazy<IIndustryRepository>(() => new
                IndustryRepository(repositoryContext));
            _jobTypeRepository = new Lazy<IJobTypeRepository>(() => new
                JobTypeRepository(repositoryContext));
            _jobRepository = new Lazy<IJobRepository>(() => new
                JobRepository(repositoryContext));
            _userSkillRepository = new Lazy<IUserSkillRepository>(() => new
                UserSkillRepository(repositoryContext));
            _userJobRepository = new Lazy<IUserJobRepository>(() => new
                UserJobRepository(repositoryContext));
        }
       
        public ILocationRepository Location => _locationRepository.Value;
        public ITokenRepository Token => _tokenRepository.Value;
        public IUserInformationRepository UserInformation => _userInformationRepository.Value;
        public IEducationRepository Education => _educationRepository.Value;
        public IWorkExperienceRepository WorkExperience => _workExperienceRepository.Value;
        public ISkillsRepository Skills => _skillsRepository.Value;
        public ICertificationRepository Certification => _certificationRepository.Value;
        public ICompanyRepository Company => _companyRepository.Value;
        public IIndustryRepository Industry => _industryRepository.Value;
        public IJobRepository Job => _jobRepository.Value;
        public IJobTypeRepository JobType => _jobTypeRepository.Value;
        public IUserSkillRepository UserSkill => _userSkillRepository.Value;

        public IUserJobRepository UserJob => _userJobRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
