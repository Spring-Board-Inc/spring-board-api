using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services
{
    public class FaqService : IFaqService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public FaqService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(FaqForCreationDto request)
        {
            var faq = mapper.Map<Faq>(request);
            await repository.Faq.CreateAsync(faq);
            await repository.SaveAsync();

            return new ApiOkResponse<FaqToReturnDto>(mapper.Map<FaqToReturnDto>(faq));
        }

        public async Task<ApiBaseResponse> Update(Guid id, FaqForUpdateDto request)
        {
            var faq = await repository.Faq.GetAsync(id, true);
            if (faq == null) return new FaqNotFoundResponse(id);

            mapper.Map(request, faq);
            faq.UpdatedAt = DateTime.Now;

            repository.Faq.UpdateFaq(faq);
            await repository.SaveAsync();

            return new ApiOkResponse<FaqToReturnDto>(mapper.Map<FaqToReturnDto>(faq));
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var faq = await repository.Faq.GetAsync(id, false);
            if (faq == null) return new FaqNotFoundResponse(id);

            return new ApiOkResponse<FaqToReturnDto>(mapper.Map<FaqToReturnDto>(faq));
        }

        public async Task<PaginatedListDto<FaqToReturnDto>> Get(SearchParameters parameters)
        {
            var faqs = await repository.Faq.GetAsync(parameters);

            var data = mapper.Map<IEnumerable<FaqToReturnDto>>(faqs);
            return PaginatedListDto<FaqToReturnDto>.Paginate(data, faqs.MetaData);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var faq = await repository.Faq.GetAsync(id, true);
            if(faq == null) return new FaqNotFoundResponse(id);

            repository.Faq.DeleteFaq(faq);
            await repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }
    }
}
