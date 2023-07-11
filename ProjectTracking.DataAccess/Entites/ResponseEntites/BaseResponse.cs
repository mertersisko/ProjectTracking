using ProjectTracking.DataAccess.Entites.Enums;

namespace ProjectTracking.DataAccess.Entites.ResponseEntites
{
    public class BaseResponse<T>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public T Data { get; set; }
        public ICollection<T> DataList { get; set; }
        public ResultStatus Status { get; set; }

    }
}
