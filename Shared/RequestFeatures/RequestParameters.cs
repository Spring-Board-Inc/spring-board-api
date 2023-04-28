namespace Shared.RequestFeatures
{
    public abstract class RequestParameters
    {

        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        public string SearchBy { get; set; } = string.Empty;
        public bool ValidDateRange => EndDate > StartDate;
    }
}