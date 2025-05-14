namespace Core.Application.Interfaces.Helpers
{
    public interface IMapper
    {
        public TResult? Handler<TResult, TSource>(TSource source);
    }
}
