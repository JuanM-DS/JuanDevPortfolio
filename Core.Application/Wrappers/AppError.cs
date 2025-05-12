namespace Core.Application.Wrappers
{
    public class AppError
    {
        public AppError(string message, string? property)
        {
            Message = message;
            Property = property;
        }

        public string Message { get; set; } = string.Empty;
        public string? Property { get; set; }
        
        public static AppError Create(string Message, string? property = null)
        {
            return new AppError(Message, property);
        }
    }
}
