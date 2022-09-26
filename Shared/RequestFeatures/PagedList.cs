using Shared.DataTransferObjects;

namespace Shared.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public static MetaData CreatePageMetaData(int page, int perPage, int total)
        {
            var total_pages = total % perPage == 0 ? total / perPage : total / perPage + 1;
            return new MetaData
            {
                CurrentPage = page,
                PageSize = perPage,
                TotalCount = total,
                TotalPages = total_pages
            };
        }

        public static PaginatedListDto<T> Paginate(IEnumerable<T> source, int page, int perPage)
        {
            page = page < 1 ? 1 : page;
            var paginatedList = source.Skip((page - 1) * perPage).Take(perPage).ToList();
            var pageMeta = CreatePageMetaData(page, perPage, source.ToList().Count);
            return new PaginatedListDto<T>
            {
                MetaData = pageMeta,
                Data = paginatedList
            };
        }
    }
}
