using System.Net;

namespace YogaApi.Core.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data, HttpStatusCode status, bool isOk)
        {
            this.data = data;
            this.httpStatusCode = (int)status;
            this.isOk = isOk;
        }

        public T data { get; set; }
        public int httpStatusCode { get; set; }
        public bool isOk { get; set; }
    }
}
