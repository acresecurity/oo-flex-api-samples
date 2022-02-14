
namespace Common
{
    public class PagedJSendResponse : JSendResponse
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalNumberOfPages { get; set; }

        public int TotalNumberOfRecords { get; set; }

        public string NextPageUrl { get; set; }
    }
}
