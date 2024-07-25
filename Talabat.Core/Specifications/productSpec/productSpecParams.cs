namespace Talabat.Core.Specifications.productSpec
{
    public class productSpecParams
    {
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


        public string? Sort { get; set; }

        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public int PageIndex { get; set; } = 1;

        private int pageSize { get; set; } = 5;

        private const int MaxPageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
