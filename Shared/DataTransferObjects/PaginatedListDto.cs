using Shared.RequestFeatures;

namespace Shared.DataTransferObjects
{
    public class PaginatedListDto<T>
    {
        public MetaData? MetaData { get; set; }
        public IEnumerable<T> Data { get; set; }
        public PaginatedListDto()
        {
            Data = new List<T>();
        }
    }
}
