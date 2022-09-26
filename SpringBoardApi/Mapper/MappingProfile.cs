using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace SpringBoardApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Location, LocationDto>();
            CreateMap<LocationForCreationDto, Location>();
            CreateMap<LocationForUpdateDto, Location>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, DetailedUserToReturnDto>();
            CreateMap<UserInformationDto, UserInformation>();
            CreateMap<UserInformation, UserInformationToReturnDto>();
            CreateMap<UserNamesForUpdateDto, User>();
            CreateMap<EducationForCreationDto, Education>();
            CreateMap<Education, EducationToReturnDto>();
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
            CreateMap<Job, JobToReturnDto>();
        }
    }
}