namespace Core.Application.Wrappers
{
    public class AppException : Exception
    {
        public AppResponse AppResponse { get; set; } = null!;

        public AppException(AppResponse AppResponse)
        {
            this.AppResponse = AppResponse;
        }
    }

    public static class AppExceptionExt
    {
        public static void Throw(this AppResponse AppResponse)
        {
            throw new AppException(AppResponse);
        }
    }
}
