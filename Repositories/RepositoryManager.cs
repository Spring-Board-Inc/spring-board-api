using Contracts;
using Microsoft.Extensions.Options;
using Repositories.Configurations;

namespace Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
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
        private readonly Lazy<IStateRepository> _stateRepository;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<ICareerSummaryRepository> _careerSummaryRepository;
        private readonly Lazy<IAboutUsRepository> _aboutUsRepository;
        private readonly Lazy<IContactRepository> _contactRepository;
        private readonly Lazy<IFaqRepository> _faqRepository;

        public RepositoryManager(IOptions<MongoDbSettings> mongoDbSettings)
        {
            _tokenRepository = new Lazy<ITokenRepository>(() => new 
                TokenRepository(mongoDbSettings));
            _userInformationRepository = new Lazy<IUserInformationRepository>(() => new
                UserInformationRepository(mongoDbSettings));
            _educationRepository = new Lazy<IEducationRepository>(() => new
                EducationRepository(mongoDbSettings));
            _workExperienceRepository = new Lazy<IWorkExperienceRepository>(() => new
                WorkExperienceRepository(mongoDbSettings));
            _skillsRepository = new Lazy<ISkillsRepository>(() => new
                SkillsRepository(mongoDbSettings));
            _companyRepository = new Lazy<ICompanyRepository>(() => new
                CompanyRepository(mongoDbSettings));
            _certificationRepository = new Lazy<ICertificationRepository>(() => new
                CertificationRepository(mongoDbSettings));
            _industryRepository = new Lazy<IIndustryRepository>(() => new
                IndustryRepository(mongoDbSettings));
            _jobTypeRepository = new Lazy<IJobTypeRepository>(() => new
                JobTypeRepository(mongoDbSettings));
            _jobRepository = new Lazy<IJobRepository>(() => new
                JobRepository(mongoDbSettings));
            _userSkillRepository = new Lazy<IUserSkillRepository>(() => new
                UserSkillRepository(mongoDbSettings));
            _userJobRepository = new Lazy<IUserJobRepository>(() => new
                UserJobRepository(mongoDbSettings));
            _stateRepository = new Lazy<IStateRepository>(() => new 
                StateRepository(mongoDbSettings));
            _countryRepository = new Lazy<ICountryRepository>(() => new
                CountryRepository(mongoDbSettings));
            _careerSummaryRepository = new Lazy<ICareerSummaryRepository>(() => new
                CareerSummaryRepository(mongoDbSettings));
            _aboutUsRepository = new Lazy<IAboutUsRepository>(() => new
                AboutUsRepository(mongoDbSettings));
            _contactRepository = new Lazy<IContactRepository>(() => new
                ContactRepository(mongoDbSettings));
            _faqRepository = new Lazy<IFaqRepository>(() => new
                FaqRepository(mongoDbSettings));
        }
       
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
        public IStateRepository State => _stateRepository.Value;
        public ICountryRepository Country => _countryRepository.Value;
        public ICareerSummaryRepository CareerSummary => _careerSummaryRepository.Value;
        public IContactRepository Contact => _contactRepository.Value;
        public IAboutUsRepository AboutUs => _aboutUsRepository.Value;
        public IFaqRepository Faq => _faqRepository.Value;
    }
}
