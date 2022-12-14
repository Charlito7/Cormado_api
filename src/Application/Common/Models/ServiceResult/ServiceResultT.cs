using System.Net;


namespace Application.Models;

public class ServiceResult<T> : ServiceResult
{
    public T Result { get; protected set; }

    public ServiceResult(HttpStatusCode status) : base(status, false)
    {

    }

    public ServiceResult(HttpStatusCode status, T result) : base(status, true)
    {
        Result = result;
    }

    public ServiceResult(T result) : base(HttpStatusCode.OK, true)
    {
        Result = result;
    }

}
