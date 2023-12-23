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
            await repository.Faq.AddAsync(faq);

            return new ApiOkResponse<FaqToReturnDto>(mapper.Map<FaqToReturnDto>(faq));
        }

        public async Task<ApiBaseResponse> Update(Guid id, FaqForUpdateDto request)
        {
            var faq = await repository.Faq.FindAsync(id);
            if (faq == null) return new FaqNotFoundResponse(id);

            mapper.Map(request, faq);
            faq.UpdatedAt = DateTime.UtcNow;

            await repository.Faq.EditAsync(x => x.Id.Equals(id), faq);
            return new ApiOkResponse<FaqToReturnDto>(mapper.Map<FaqToReturnDto>(faq));
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var faq = await repository.Faq.FindAsync(id);
            if (faq == null) return new FaqNotFoundResponse(id);

            return new ApiOkResponse<FaqToReturnDto>(mapper.Map<FaqToReturnDto>(faq));
        }

        public PaginatedListDto<FaqToReturnDto> Get(SearchParameters parameters)
        {
            var faqs = repository.Faq.FindAsync(parameters);

            var data = mapper.Map<IEnumerable<FaqToReturnDto>>(faqs);
            return PaginatedListDto<FaqToReturnDto>.Paginate(data, faqs.MetaData);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var faq = await repository.Faq.FindAsync(id);
            if(faq == null) return new FaqNotFoundResponse(id);

            await repository.Faq.DeleteAsync(x => x.Id.Equals(id));
            return new ApiOkResponse<bool>(true);
        }
    }
}
