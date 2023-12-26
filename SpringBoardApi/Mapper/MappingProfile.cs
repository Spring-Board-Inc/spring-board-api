using AutoMapper;
using Entities.Models;
using Shared;
using Shared.DataTransferObjects;

namespace SpringBoardApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<State, StateDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name));
            CreateMap<Skill, SkillDto>();
            CreateMap<StateForCreationDto, State>();
            CreateMap<StateForUpdateDto, State>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryForCreationDto, Country>();
            CreateMap<CountryForUpdateDto, Country>();
            CreateMap<UserForRegistrationDto, AppUser>();
            CreateMap<AppUser, DetailedUserToReturnDto>();
            CreateMap<UserInformationDto, UserInformation>();
            CreateMap<UserInformation, UserInformationToReturnDto>();
            CreateMap<UserNamesForUpdateDto, AppUser>();
            CreateMap<UserAddressForUpdateDto, AppUser>();
            CreateMap<EducationForCreationDto, Education>()
                .ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.Course));
            CreateMap<Education, EducationToReturnDto>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Major));
            CreateMap<EducationForUpdateDto, Education>();
            CreateMap<WorkExperienceRequest, WorkExperience>();
            CreateMap<WorkExperience, WorkExperienceToReturnDto>();
            CreateMap<SkillRequest, Skill>();
            CreateMap<Skill, SkillToReturnDto>();
            CreateMap<UserSkillRequest, UserSkill>();
            CreateMap<CertificationRequest, Certification>();
            CreateMap<Certification, CertificationDto>();
            CreateMap<JobTypeRequestObject, JobType>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.JobType));
            CreateMap<JobType, JobTypeToReturnDto>()
                .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.Name));
            CreateMap<IndustryRequestObject, Industry>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Industry));
            CreateMap<Industry, IndustryToReturnDto>()
                .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Name));
            CreateMap<CompanyRequestObject, Company>();
            CreateMap<Company, CompanyToReturnDto>();
            CreateMap<JobRequestObject, Job>();
            CreateMap<Job, JobMinimumInfoDto>();
            CreateMap<Job, JobToReturnDto>();
            CreateMap<AppUser, ApplicantInformation>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom((src) => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
            CreateMap<UserInformation, UserInformationToReturn>()
                .ForMember(dest => dest.Educations, opt => opt.MapFrom(src => src.Educations))
                .ForMember(dest => dest.WorkExperiences, opt => opt.MapFrom(src => src.WorkExperiences))
                .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications))
                .ForMember(dest => dest.UserSkills, opt => opt.MapFrom(src => src.UserSkills));
            CreateMap<Education, EducationMinInfo>();
            CreateMap<WorkExperience, WorkExperienceMinInfo>();
            CreateMap<Certification, CertificationMinInfo>();
            CreateMap<UserSkill, UserSkillMinInfo>();
            CreateMap<CareerSummary, CareerSummaryReturnDto>()
                .ForMember(dest => dest.CareerSummary, opt => opt.MapFrom(src => src.Summary));
            CreateMap<CareerSummaryDto, CareerSummary>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.CareerSummary));
            CreateMap<CareerSummaryUpdateDto, CareerSummary>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.CareerSummary));
            CreateMap<AboutUsForCreateDto, AboutUs>();
            CreateMap<AboutUsForUpdateDto, AboutUs>();
            CreateMap<AboutUs, AboutUsToReturnDto>();
            CreateMap<ContactForCreationDto, Contact>();
            CreateMap<ContactForUpdateDto, Contact>();
            CreateMap<Contact, ContactToReturnDto>();
            CreateMap<AddressForCreationDto, Address>();
            CreateMap<Address, AddressToReturnDto>();
            CreateMap<AddressForUpdateDto, Address>();
            CreateMap<PhoneForCreationDto, Phone>();
            CreateMap<Phone, PhoneToReturnDto>();
            CreateMap<PhoneForUpdateDto, Phone>();
            CreateMap<EmailForCreationDto, Email>();
            CreateMap<Email, EmailToReturnDto>();
            CreateMap<EmailForUpdateDto, Email>();
            CreateMap<FaqForCreationDto, Faq>();
            CreateMap<FaqForUpdateDto, Faq>();
            CreateMap<Faq, FaqToReturnDto>();
        }
    }
}