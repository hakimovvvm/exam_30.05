using System.Net;

namespace Domain.ApiResponse;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public string Massage { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    public Response(string massage, T? data)
    {
        IsSuccess = true;
        Massage = massage;
        Data = data;
        StatusCode = (int)HttpStatusCode.OK;
    } 
    public Response(string massage, HttpStatusCode code)
    {
        IsSuccess = true;
        Massage = massage;
        Data = default;
        StatusCode = (int)code;
    } 
}