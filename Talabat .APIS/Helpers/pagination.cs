namespace Talabat_.APIS.Helpers
{
    public class pagination<T>
    {
        public pagination(int pageSize, int count, int pageIndex, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            Count = count;
            PageIndex = pageIndex;
            Data = data;
        }

        public int PageSize { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
