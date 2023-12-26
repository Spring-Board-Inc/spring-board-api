using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using Services.Extensions;
using Shared;
using Shared.Helpers;

namespace Services
{
    public class CareerSummaryService : ICareerSummaryService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;
        private readonly UserManager<AppUser> userManager;

        public CareerSummaryService(IRepositoryManager repository, IMapper mapper, ILoggerManager logger, UserManager<AppUser> userManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.userManager = userManager;
        }

        public async Task<ApiBaseResponse> Create(string userId, CareerSummaryDto request)
        {
            if(!request.IsValid)
            {
                logger.LogError($"{nameof(CareerSummaryDto)} {nameof(CareerSummaryDto.CareerSummary)} value is invalid");
                return new BadRequestResponse(ResponseMessages.InvalidRequest);
            }

            bool exists = await repository.CareerSummary.Exists(userId.StringToGuid());
            if(exists) return new BadRequestResponse(ResponseMessages.CareerSummaryExists);

            var user = await userManager.FindByIdAsync(userId);
            if(user == null) return new BadRequestResponse(ResponseMessages.UserNotFound);

            CareerSummary entity = mapper.Map<CareerSummary>(request);

            entity.UserId = userId.StringToGuid();
            await repository.CareerSummary.AddAsync(entity);

            user.CareerSummaryId = entity.Id;
            await userManager.UpdateAsync(user);

            CareerSummaryReturnDto data = mapper.Map<CareerSummaryReturnDto>(entity);
            return new ApiOkResponse<CareerSummaryReturnDto>(data);
        }

        public ApiBaseResponse GetMany(string userId)
        {
            List<CareerSummary> entity = repository.CareerSummary
                .FindAsQueryable(userId.StringToGuid())
                .ToList();

            if (entity == null) return new NotFoundResponse(ResponseMessages.CareerSummaryNotFound);

            List<CareerSummaryReturnDto> data = mapper.Map<List<CareerSummaryReturnDto>>(entity);
            return new ApiOkResponse<List<CareerSummaryReturnDto>>(data);
        }

        public ApiBaseResponse Get(string userId)
        {
            CareerSummary entity = repository.CareerSummary
                .FindAsQueryable(userId.StringToGuid())
                .FirstOrDefault();

            if (entity == null) return new NotFoundResponse(ResponseMessages.CareerSummaryNotFound);

            CareerSummaryReturnDto data = mapper.Map<CareerSummaryReturnDto>(entity);
            return new ApiOkResponse<CareerSummaryReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Update(Guid id, string userId, CareerSummaryUpdateDto request)
        {
            if (!request.IsValid)
            {
                logger.LogError($"{nameof(CareerSummaryUpdateDto)} {nameof(CareerSummaryUpdateDto.CareerSummary)} value is invalid");
                return new BadRequestResponse(ResponseMessages.InvalidRequest);
            }

            CareerSummary entity = repository.CareerSummary
                .FindByIdAsQueryable(id, userId.StringToGuid())
                .FirstOrDefault();

            if (entity == null) return new NotFoundResponse(ResponseMessages.CareerSummaryNotFound);

            mapper.Map(request, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            await repository.CareerSummary.EditAsync(x => x.Id.Equals(id), entity);
            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Delete(Guid id, string userId)
        {
            CareerSummary entity = repository.CareerSummary
                .FindByIdAsQueryable(id, userId.StringToGuid())
                .FirstOrDefault();

            if (entity == null) return new NotFoundResponse(ResponseMessages.CareerSummaryNotFound);

            await repository.CareerSummary
                .DeleteAsync(x => x.Id.Equals(entity.Id));

            return new ApiOkResponse<bool>(true);
        }
    }
}
