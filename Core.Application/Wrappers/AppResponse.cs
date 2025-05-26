using System.Net;

namespace Core.Application.Wrappers
{
	public class AppResponse
    {
        public string? Message { get; set; } 
        public bool Succeded { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public List<AppError>? Errors { get; set; }
    }

    public class AppResponse<T> : AppResponse
    {
        public T? Data { get; set; }

        public AppResponse(T? data, HttpStatusCode httpStatusCode, string? message = null)
        {
            Data = data;
            Message = message;
            Succeded = true;
            HttpStatusCode = httpStatusCode;
        }
        public AppResponse(HttpStatusCode httpStatusCode, string? message = null)
        {
            Message = message;
            Succeded = true;
            HttpStatusCode = httpStatusCode;
        }

        public AppResponse(AppError error, HttpStatusCode httpStatusCode, string? message = null)
        {
            Message = message;
            Succeded = false;
            HttpStatusCode = httpStatusCode;
            Errors = Errors ?? [];
            Errors.Add(error);
        }
        public AppResponse(List<AppError> error, HttpStatusCode httpStatusCode, string? message = null)
        {
            Message = message;
            Succeded = false;
            HttpStatusCode = httpStatusCode;
            Errors = Errors ?? [];
            Errors.AddRange(error);
        }
    }

    public static class AppResponseExt
    {
        public static AppResponse<T> BuildResponse<T>(T data, HttpStatusCode code, string? message = null)
        {
            return new AppResponse<T>(data, code, message);
        }

        public static AppResponse<T> BuildResponse<T>(this AppError error, HttpStatusCode code, string? message = null)
        {
            return new AppResponse<T>(error, code, message);
        }

        public static AppResponse<T> BuildResponse<T>(this List<AppError> errors, HttpStatusCode code, string? message = null)
        {
            return new AppResponse<T>(errors, code, message);
        }

		public static AppResponse<T> AddError<T>(this AppResponse<T> response, AppError error)
		{
			response.Errors = response.Errors ?? [];
			response.Errors.Add(error);
            return response;
		}
	}
}
